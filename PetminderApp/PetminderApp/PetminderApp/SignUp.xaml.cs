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
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
            signUp.Clicked += SignUp_Clicked;
        }

        private async void SignUp_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddPet());
        }

        private async void GotoLogIn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }
    }
}