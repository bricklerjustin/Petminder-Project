using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Files;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderList : ContentPage
    {
        public IList<ReminderReadModel> Reminders { get; set; }
        public ReminderList()
        {
            InitializeComponent();

            ReminderFile file = new ReminderFile(DefaultFileNames.ReminderFileName);
            var ReminderReturn = GetReminderList();

            if (ReminderReturn != null)
            {
                Reminders = ReminderReturn;          
            }

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
                var reminderList = GetReminderList();
                Reminders = reminderList;
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
            Navigation.PushModalAsync(new AddReminder());
        }

        public void On_ReminderEdit(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddReminder());
        }

    }
}