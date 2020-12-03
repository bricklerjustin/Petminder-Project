using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Files;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PetminderApp.Reminders;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReminder : ContentPage
    {
        private bool _update;
        private Guid id;

        public AddReminder(ReminderReadModel reminderModel)
        {
            InitializeComponent();

            //List<Types> types = new List<Types>();
            //types.Add(new Types(0, "Appointment"));
            //types.Add(new Types(1, "Medicine"));
            //types.Add(new Types(2, "Exercise"));

            //IList<Types> source = types;

            submitReminder.Clicked += SubmitReminder_Clicked;

            TypePicker.Items.Add("Appointment");
            TypePicker.Items.Add("Medicine");
            TypePicker.Items.Add("Exercise");
            TypePicker.Items.Add("Other");
            TypePicker.SelectedIndex = 0;

            Frequency.Items.Add("Daily");
            Frequency.Items.Add("Weekly");
            Frequency.Items.Add("Monthly");

            //PetId = pet.Id;

            SetFrequencyVisible();

            if (reminderModel != null)
            {
                _update = true;
                Name.Text = reminderModel.Name;
                Message.Text = reminderModel.Message;
                Repeat.IsToggled = reminderModel.Repeat;
                TypePicker.SelectedItem = reminderModel.Type;
                StartDate.Date = reminderModel.StartDate;
                Time.Time = reminderModel.TimeLocal.TimeOfDay;
                id = reminderModel.Id;
            }
        }

        private void Repeat_OnToggled(object sender, ToggledEventArgs e)
        {
            SetFrequencyVisible();
        }

        private async void SubmitReminder_Clicked(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            HttpResponseMessage response = new HttpResponseMessage();

            ReminderCreateModel reminderModel = new ReminderCreateModel();
            reminderModel.Name = Name.Text;
            reminderModel.Message = Message.Text;
            reminderModel.Repeat = Repeat.IsToggled;
            reminderModel.AccountId = UserInfo.AccountId;
            reminderModel.Complete = false;
            reminderModel.Type = TypePicker.SelectedItem.ToString(); //Since this object is just a string this is ok
            reminderModel.StartDate = StartDate.Date;
            reminderModel.Time = DateTime.Today.ToLocalTime() + Time.Time; //Converting to local to add time offset

            if (string.IsNullOrEmpty(reminderModel.Name))
            {
                await ValidationAlert(nameof(Name));
                return;
            }
            if (string.IsNullOrEmpty(reminderModel.Message))
            {
                await ValidationAlert(nameof(Message));
                return;
            }

            if (reminderModel.Repeat)
            {
                reminderModel.Frequency = Frequency.SelectedItem.ToString();
            }

            ActivityIndicatorToggle(true);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var body = JsonConvert.SerializeObject(reminderModel);

                if (_update)
                {
                    response = client.Put("api/reminders", id, UserInfo.Token, body);
                }
                else
                {
                    response = client.Post("api/reminders", "", UserInfo.Token, body);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    ReminderReadModel createdReminder = JsonConvert.DeserializeObject<ReminderReadModel>(response.Content.ReadAsStringAsync().Result);
                    if (reminderModel.Repeat == false)
                    {
                        await PetminderApp.Reminders.Reminders.AddReminderToCalendar(createdReminder);
                    }
                    else
                    {
                        await PetminderApp.Reminders.Reminders.AddRepeatingReminderToCalendar(createdReminder);
                    }
                    await Navigation.PopModalAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    var readResponse = client.Get($"api/reminders/{id}", UserInfo.Token);

                    if (readResponse.IsSuccessStatusCode)
                    {
                        var updatedReminder = JsonConvert.DeserializeObject<ReminderReadModel>(readResponse.Content.ReadAsStringAsync().Result);

                        if (updatedReminder.Repeat == false)
                        {
                            await PetminderApp.Reminders.Reminders.DeleteReminder(updatedReminder);
                            await PetminderApp.Reminders.Reminders.AddReminderToCalendar(updatedReminder);
                        }
                        else
                        {
                            await PetminderApp.Reminders.Reminders.DeleteReminder(updatedReminder);
                            await PetminderApp.Reminders.Reminders.AddRepeatingReminderToCalendar(updatedReminder);
                        }
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    if (_update)
                    {
                        await DisplayAlert("Update Error", "Please try again", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Creation Error", "Please try again", "Ok");
                    }
                }
            });
        }

        private void SetFrequencyVisible()
        {
            if (Repeat.IsToggled == true)
            {
                Frequency.IsEnabled = true;
                Frequency.IsVisible = true;
                Frequency.SelectedIndex = 0;
            }
            else
            {
                Frequency.IsVisible = false;
                Frequency.IsEnabled = false;
            }
        }

        private async Task<bool> ValidationAlert(string field)
        {
            await DisplayAlert("Entry Issue", $"The field '{field}' cannot be empty", "Ok");
            return true;
        }

        private void ActivityIndicatorToggle(bool toggle)
        {
            activityIndicator.IsVisible = toggle;
            activityIndicator.IsRunning = toggle;
        }
    }
}