using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Files;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderList : ContentPage
    {
        private AddReminder _AddReminderModal;
        public IList<ReminderReadModel> Reminders { get; set; }
        public ReminderList()
        {
            InitializeComponent();

            AppDataReadWrite file = new AppDataReadWrite(DefaultFileNames.ReminderFileName);
            
            RefreshListView();

            BindingContext = this;
        }

        public void On_ReminderDelete(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var reminderModel = contextItem as ReminderReadModel;

            try
            {
                client.Delete("api/reminders", UserInfo.Token, reminderModel.Id);

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
                Reminders = reminderList;
                ReminderListview.ItemsSource = null;
                ReminderListview.ItemsSource = Reminders;
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
    }
}