using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Models.ListViewGroups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderList : ContentPage
    {
        private AddReminder _AddReminderModal;
        public IList<ReminderReadModel> Reminders { get; set; }
        private ObservableCollection<ReminderGrouped> grouped { get; set; }
        public ReminderList()
        {
            InitializeComponent();

            PetminderApp.Reminders.Reminders.CalendarSyncProcess();

            BindingContext = this;

        }

        public async void On_ReminderDelete(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var reminderModel = contextItem as ReminderReadModel;

            try
            {
                client.Delete("api/reminders", UserInfo.Token, reminderModel.Id);
                await PetminderApp.Reminders.Reminders.DeleteReminder(reminderModel);

                //Refresh List
                RefreshListView();
            }
            catch
            {
                //ERROR
            }
        }

        private List<ReminderReadModel> GetReminderList()
        {
            RestClient client = new RestClient();
            List<ReminderReadModel> reminders = new List<ReminderReadModel>();

            var responseMessage = client.Get("api/reminders", UserInfo.Token);

            if (responseMessage.IsSuccessStatusCode)
            {
                reminders = JsonConvert.DeserializeObject<List<ReminderReadModel>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            else
            {
                //ERROR
            }

            return reminders;
        }

        public void On_ReminderAdd(object sender, EventArgs e)
        {
            ShowReminderModalPage(null);
        }

        public void On_ReminderEdit(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var reminderModel = contextItem as ReminderReadModel;
            ShowReminderModalPage(reminderModel);
        }

        private void RefreshListView()
        {
            var reminderList = GetReminderList();
            if (reminderList != null)
            {
                grouped = new ObservableCollection<ReminderGrouped>();
                ReminderGrouped group = new ReminderGrouped();

                foreach (string type in reminderList.Select(x => x.Type).Distinct())
                {
                    group = new ReminderGrouped() { LongName = type, ShortName = type };

                    foreach (ReminderReadModel reminder in reminderList.Where(p => p.Type == type))
                    {
                        group.Add(reminder);
                    }

                    grouped.Add(group);
                }

                Reminders = reminderList;
                ReminderListview.ItemsSource = null;
                ReminderListview.ItemsSource = grouped;
            }
        }

        private async void ShowReminderModalPage(ReminderReadModel Reminder)
        {
            //Add handler for modal return
            Application.Current.ModalPopping += HandleModalPopping;
            _AddReminderModal = new AddReminder(Reminder);
            await Navigation.PushModalAsync(_AddReminderModal);
        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _AddReminderModal)
            {
                RefreshListView();
            }
        }

        private void ActivityIndicatorToggle(bool toggle)
        {
            activityIndicator.IsVisible = toggle;
            activityIndicator.IsRunning = toggle;
        }

        public class GroupedReminderList
        {
            public string Title { get; set; }
            public string ShortName { get; set; } //will be used for jump lists
            public string Subtitle { get; set; }
            private GroupedReminderList(string title, string shortName)
            {
                Title = title;
                ShortName = shortName;
            }

            public static IList<GroupedReminderList> All { private set; get; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshListView();
        }
    }
}