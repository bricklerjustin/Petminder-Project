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
    public partial class AddPet : ContentPage
    {
        public AddPet()
        {
            InitializeComponent();

            submitPet.Clicked += SubmitPet_Clicked;

            
        }

        private async void SubmitPet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeScreen());
        }

        private async void AddAnotherPet_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPet());
        }
    }
}