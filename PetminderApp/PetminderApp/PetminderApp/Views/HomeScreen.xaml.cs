using PetminderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeScreen : ContentPage
    {
        public HomeScreen()
        {
            InitializeComponent();

            editPet.Clicked += EditPet_Clicked;

            reminderEvents.Clicked += ReminderEvents_Clicked;

            exerciseFitBark.Clicked += ExerciseFitBark_Clicked;

            searchProviders.Clicked += SearchProviders_Clicked;


            // NavigationPage.SetHasNavigationBar(this, false);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#ffffff");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#0778B6");


        }

        private async void EditPet_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditPet());
        }

        private async void ReminderEvents_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FileList(new Guid("D1BAEBBD-2199-440B-9727-08D8942B76A7")));
        }

        private async void ExerciseFitBark_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Exercise());
        }

        private async void SearchProviders_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchProviders());
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Logout", "Would you like to contiune with logout?", "Yes", "No");

                if (result)
                {
                    await this.Navigation.PopAsync();
                }
            });

            return true;
        }
    }
}