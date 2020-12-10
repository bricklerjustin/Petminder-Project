using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();

            signUp.Clicked += SignUp_Clicked;

            // NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SignUp_Clicked(object sender, System.EventArgs e)
        {
            this.IsBusy = true;

            RestClient client = new RestClient();

            var response = client.Post("api/accounts", "", "", "{}");

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var responsestring = response.Content.ReadAsStringAsync().Result;
                var AccountReturn = JsonConvert.DeserializeObject<AccountPostReturnModel>(responsestring);

                UserCreateModel userModel = new UserCreateModel();

                userModel.accountId = AccountReturn.id;
                userModel.email = Email.Text.ToLower();
                userModel.username = Email.Text;
                userModel.passwordHash = Password.Text;
                userModel.googleAuth = "";

                response = client.Post("api/users", "", AccountReturn.apiKey, JsonConvert.SerializeObject(userModel));

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Email.Text = "";
                    Password.Text = "";
                    FullName.Text = "";

                    UserInfo.AccountId = userModel.accountId;
                    UserInfo.Username = userModel.email.ToLower();
                    UserInfo.Token = AccountReturn.apiKey;

                    await DisplayAlert("Success", "Account Created", "Ok");

                    this.IsBusy = false;

                    UserInfo.LoggedIn = true;
                    Navigation.InsertPageBefore(new FirstTimeSetup(), this);
                   // Navigation.InsertPageBefore(new AddPet(null, new HomeScreen()), this);
                    await Navigation.PopAsync();
                    return;
                }
            }

            this.IsBusy = false;
            await DisplayAlert("Error", "There was an issue creating your account, please try again.", "Ok");
        }

        private async void GotoLogIn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }
    }
}