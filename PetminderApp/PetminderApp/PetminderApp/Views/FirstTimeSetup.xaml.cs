using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstTimeSetup : ContentPage
    {
        public FirstTimeSetup()
        {
            InitializeComponent();
        }

        private void start_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddPet(null, new CustomMaster()));
        }
    }
}