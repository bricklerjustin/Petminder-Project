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
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();

            signIn.Clicked += SignIn_Clicked;
        }

        private async void SignIn_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HomeScreen());
        }

        private async void GoToSignUp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}