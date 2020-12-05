using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PetminderApp.Api;
using System.Net;
using Newtonsoft.Json;
using PetminderApp.Api.Api_Models;

namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();

            signIn.Clicked += SignIn_Clicked;

           // NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;

            RestClient restClient = new RestClient();

            UserInfo.Username = (!string.IsNullOrEmpty(Email.Text)) ? Email.Text.ToLower() : "";

            var response = restClient.Login("api/authenticate", UserInfo.Username, Password.Text);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                AuthenticateReturnModel authenticateReturnModel = JsonConvert.DeserializeObject<AuthenticateReturnModel>(content.Result);

                UserInfo.Token = authenticateReturnModel.ApiKey;
                UserInfo.AccountId = authenticateReturnModel.AccountId;
                Password.Text = "";

                this.IsBusy = false;

                UserInfo.LoggedIn = true;
                Navigation.InsertPageBefore(new CustomMaster(), this);
               // Navigation.InsertPageBefore(new HomeScreen(), this);
                await Navigation.PopAsync();
            }
            else
            {
                this.IsBusy = false;
                await DisplayAlert("Invalid", "The information entered is invalid, please try again", "OK");
            }
        }

        private async void GoToSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new SignUp(), this);

            await Navigation.PopAsync();            
        }
    }
}