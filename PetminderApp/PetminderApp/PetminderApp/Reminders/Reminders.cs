using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Files;
using PetminderApp.Models;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Essentials;

namespace PetminderApp.Reminders
{
    public static class Reminders 
    {
        static public async void CalendarSyncProcess()
        {
            var granted = await CheckPermissions();
            if (!granted)
            {
                return;
            }

            var reminders = GetRemindersList();
            await CleanCalendarAndLocalStorage(reminders);
            CleanUpFinishedReminders(reminders);
            await AddMissingReminders(reminders);

        }


        private static List<ReminderReadModel> GetRemindersList()
        {
            RestClient client = new RestClient();
            List<ReminderReadModel> reminders = null;
            try
            {
                var response = client.Get("api/reminders", UserInfo.Token);

                if (response.IsSuccessStatusCode)
                {
                    reminders = new List<ReminderReadModel>();
                    reminders = JsonConvert.DeserializeObject<List<ReminderReadModel>>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch
            {
                //ERROR
            }

            return reminders;
        }

        //This code is going to be insane pls no
        public static async Task<bool> AddMissingReminders(List<ReminderReadModel> reminders)
        {
            var calendar = await CheckCalendarExistanceAndCreation("Petminder", "Petminder");

            AppDataReadWrite dataWriter = new AppDataReadWrite("Reminders.json");
            var localReminders = JsonConvert.DeserializeObject<List<LocalReminderModel>>(dataWriter.ReadStringFromFile());

            var calendarEvents = await CrossCalendars.Current.GetEventsAsync(calendar, DateTime.MinValue, DateTime.MaxValue);

            foreach (var reminder in reminders)
            {
                var exists = localReminders.FirstOrDefault(p => p.databaseId == reminder.Id);
                if (exists == null)
                {
                    if (reminder.Repeat)
                    {
                        await AddRepeatingReminderToCalendar(reminder);
                    }
                    else
                    {
                        await AddReminderToCalendar(reminder);
                    }
                }
            }

            return true;
        }

        public static async Task<bool> AddRepeatingReminderToCalendar(ReminderReadModel reminderModel)
        {
            try
            {

                var calendar = await CheckCalendarExistanceAndCreation("Petminder", "Petminder");
                var frequency = reminderModel.Frequency;
                DateTime dateTime;
                List<string> externalIds = new List<string>();
                string externalId;
                LocalReminderModel localReminderModel = new LocalReminderModel();

                dateTime = reminderModel.StartDate + reminderModel.TimeLocal.TimeOfDay;

                if (frequency == "Daily")
                {
                    var loopcount = DateTime.IsLeapYear(reminderModel.StartDate.Year) ? 366 : 365;

                    for (int i = 0; i < loopcount; i++)
                    {
                        externalId = await AddReminderToCalendar(reminderModel, calendar, dateTime, dateTime + TimeSpan.FromMinutes(15));
                        dateTime = dateTime.AddDays(1);
                        externalIds.Add(externalId);
                    }
                }
                else if (frequency == "Weekly")
                {
                    var loopcount = 52;

                    for (int i = 0; i < loopcount; i++)
                    {
                        externalId = await AddReminderToCalendar(reminderModel, calendar, dateTime, dateTime + TimeSpan.FromMinutes(15));
                        dateTime = dateTime.AddDays(7);
                        externalIds.Add(externalId);
                    }
                }
                else if (frequency == "Monthly")
                {
                    var loopcount = 12;

                    for (int i = 0; i < loopcount; i++)
                    {
                        externalId = await AddReminderToCalendar(reminderModel, calendar, dateTime, dateTime + TimeSpan.FromMinutes(15));
                        dateTime = dateTime.AddMonths(1);
                        externalIds.Add(externalId);
                    }
                }

                localReminderModel.databaseId = reminderModel.Id;
                localReminderModel.calendarIds = externalIds;

                AppendToReminderToLocal(localReminderModel);

            }
            catch
            {
                return false;
            }

            return true;
        }

        private static void AppendToReminderToLocal(LocalReminderModel localReminderModel)
        {
            AppDataReadWrite dataWriter = new AppDataReadWrite("Reminders.json");
            var localReminders = JsonConvert.DeserializeObject<List<LocalReminderModel>>(dataWriter.ReadStringFromFile());

            if (localReminders == null)
            {
                localReminders = new List<LocalReminderModel>();
            }

            localReminders.Add(localReminderModel);

            dataWriter.WriteStringToFile(JsonConvert.SerializeObject(localReminders));
        }

        //For use elsewhere
        public static async Task<string> AddReminderToCalendar(ReminderReadModel reminderModel)
        {

            var calendar = await CheckCalendarExistanceAndCreation("Petminder", "Petminder");
            var start = reminderModel.StartDate + reminderModel.TimeLocal.TimeOfDay;
            var end = reminderModel.StartDate + reminderModel.TimeLocal.TimeOfDay + TimeSpan.FromMinutes(15);
            var id = await AddReminderToCalendar(reminderModel, calendar, start, end);

            LocalReminderModel localReminderModel = new LocalReminderModel();

            localReminderModel.calendarIds.Add(id);
            localReminderModel.databaseId = reminderModel.Id;

            AppendToReminderToLocal(localReminderModel);
            return id;
        }

        //Interal Class method, does that actual work of adding to calendar
        private static async Task<string> AddReminderToCalendar(ReminderReadModel reminderModel, Calendar calendar, DateTime start, DateTime end)
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            CalendarEventReminder reminder = new CalendarEventReminder();
            calendarEvent.Name = reminderModel.Name;
            calendarEvent.Description = reminderModel.Message;
            calendarEvent.Start = start;
            calendarEvent.End = end;
            reminder.Method = CalendarReminderMethod.Alert;
            reminder.TimeBefore = TimeSpan.FromMinutes(15);
            IList<CalendarEventReminder> reminderList = new List<CalendarEventReminder>();
            reminderList.Add(reminder);
            calendarEvent.Reminders = reminderList;

            await CrossCalendars.Current.AddOrUpdateEventAsync(calendar, calendarEvent);
            var returnList = await CrossCalendars.Current.GetEventsAsync(calendar, start, end);

            return returnList[0].ExternalID.ToString();
        }

        private static async Task<Calendar> CheckCalendarExistanceAndCreation(string CalendarName, string AccountOwner)
        {
            var list = await CrossCalendars.Current.GetCalendarsAsync();

            foreach (Calendar calendarFromList in list)
            {
                if (calendarFromList.AccountName == AccountOwner && calendarFromList.Name == CalendarName)
                {
                    return calendarFromList;
                }
            }

            var calendar = await CrossCalendars.Current.CreateCalendarAsync("Petminder");
            return calendar;
        }

        private static async Task<bool> CheckPermissions()
        {
            var readPermissions = await Xamarin.Essentials.Permissions.RequestAsync<Permissions.CalendarRead>();
            var writePermissions = await Xamarin.Essentials.Permissions.RequestAsync<Permissions.CalendarWrite>(); 

            if (readPermissions == PermissionStatus.Granted && writePermissions == PermissionStatus.Granted)
            {
                return true;
            }

            return false;
        }

        private static void CleanUpFinishedReminders(List<ReminderReadModel> reminders)
        {
            RestClient client = new RestClient();
            AppDataReadWrite dataWriter = new AppDataReadWrite("Reminders.json");
            var strLocalReminders = dataWriter.ReadStringFromFile();

            var localReminders = JsonConvert.DeserializeObject<List<LocalReminderModel>>(strLocalReminders);

            foreach (ReminderReadModel reminder in  reminders)
            {
                if (reminder.StartDate < DateTime.Now && reminder.Repeat == false)
                {
                    client.Delete("api/reminders", UserInfo.Token, reminder.Id);
                    reminder.Complete = true;

                    if (localReminders != null)
                    {
                        localReminders.RemoveAll(p => p.databaseId == reminder.Id);
                    }
                }
            }

            if (reminders != null)
            {
                reminders.RemoveAll(p => p.Complete == true);
            }

            dataWriter.WriteStringToFile(JsonConvert.SerializeObject(localReminders));
        }

        //General Delete Reminder Event
        public static async Task<bool> DeleteReminder(ReminderReadModel reminderModel)
        {
            CalendarEvent calendarEvent;
            var calendar = await CheckCalendarExistanceAndCreation("Petminder", "Petminder");

            AppDataReadWrite dataWriter = new AppDataReadWrite("Reminders.json");
            var strLocalReminders = dataWriter.ReadStringFromFile();

            var localReminders = JsonConvert.DeserializeObject<List<LocalReminderModel>>(strLocalReminders);

            if (localReminders != null)
            {
                var reminderToDelete = localReminders.FirstOrDefault(p => p.databaseId == reminderModel.Id);

                if (reminderToDelete != null)
                {
                    //Remove from local storage json file first since sync process will clean up if the delete process is interrupted
                    localReminders.RemoveAll(p => p.databaseId == reminderModel.Id);
                    dataWriter.WriteStringToFile(JsonConvert.SerializeObject(localReminders));

                    foreach (string externalId in reminderToDelete.calendarIds)
                    {
                        calendarEvent = await CrossCalendars.Current.GetEventByIdAsync(externalId);
                        if (calendarEvent != null)
                        {
                            await CrossCalendars.Current.DeleteEventAsync(calendar, calendarEvent);
                        }
                    }
                }
            }

            return true;
        }

        //This cleans up any events that get missed by other delete processes.
        //Needed in case other delete processes are intreruppted and dont restard. ie app is closed
        public static async Task<bool> CleanCalendarAndLocalStorage(List<ReminderReadModel> reminders)
        {
            if (reminders != null)
            {
                //Get Calendar and all events on it
                var calendar = await CheckCalendarExistanceAndCreation("Petminder", "Petminder");
                var calendarEvents = await CrossCalendars.Current.GetEventsAsync(calendar, DateTime.MinValue, DateTime.MaxValue);
                var listCalendarEvents = calendarEvents.ToList<CalendarEvent>();

                AppDataReadWrite dataWriter = new AppDataReadWrite("Reminders.json");
                var strLocalReminders = dataWriter.ReadStringFromFile();

                var localReminders = JsonConvert.DeserializeObject<List<LocalReminderModel>>(strLocalReminders);

                // Cleans Local Storage
                if (localReminders != null)
                {
                    foreach (LocalReminderModel localReminder in localReminders)
                    {
                        var exists = await CrossCalendars.Current.GetEventByIdAsync(localReminder.calendarIds.First());

                        if (exists == null)
                        {
                            localReminder.toBeRemoved = true;
                        }
                        else
                        {
                            var reminder = reminders.FirstOrDefault(p => p.Id == localReminder.databaseId);

                            if (reminder == null)
                            {
                                localReminder.toBeRemoved = true;
                            }
                        }
                    }

                    //Remove bad data and write bad to local storage
                    localReminders.RemoveAll(p => p.toBeRemoved == true);
                    dataWriter.WriteStringToFile(JsonConvert.SerializeObject(localReminders));



                    List<string> validExternalIds = new List<string>();

                    //Combine all valid calendar events into one list
                    foreach (var localreminder in localReminders)
                    {
                        validExternalIds.InsertRange(0, localreminder.calendarIds);
                    }

                    //Loop through list
                    foreach (var validId in validExternalIds)
                    {
                        //Remove if valid
                        listCalendarEvents.RemoveAll(p => p.ExternalID == validId);
                    }

                    //Clean up calendar
                    foreach (var listCalendarEvent in listCalendarEvents)
                    {
                        await CrossCalendars.Current.DeleteEventAsync(calendar, listCalendarEvent);
                    }
                }
                else
                {
                    if (listCalendarEvents.Count > 0)
                    {
                        foreach (CalendarEvent calendarEvent in listCalendarEvents)
                        {
                            await CrossCalendars.Current.DeleteEventAsync(calendar, calendarEvent);
                        }
                    }
                }
            }

            return true;
        }
    }

}

