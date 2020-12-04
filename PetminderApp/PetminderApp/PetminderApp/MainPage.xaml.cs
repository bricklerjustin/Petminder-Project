 using PetminderApp.Api;
using PetminderApp.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetminderApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            // Constructor
            InitializeComponent();

            if (UserInfo.LoggedIn)
            {
                Navigation.PushAsync(new HomeScreen());
                
            }

            signUp.Clicked += SignUp_Clicked;
            signIn.Clicked += SignIn_Clicked;

            NavigationPage.SetHasNavigationBar(this, false);
           // ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#2196f3");
           // ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#ffffff");
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }

        private async void SignUp_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }



    }
}
