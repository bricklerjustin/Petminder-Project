using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPet : ContentPage
    {
        public AddPet()
        {
            InitializeComponent();

            submitPet.Clicked += SubmitPet_Clicked;
            uploadPet.Clicked += UploadPet_Clicked;

        }

        private async void SubmitPet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeScreen());
        }
        private async void UploadPet_Clicked(object sender, EventArgs e)
        {

            try
            {

                FileData filedata = await CrossFilePicker.Current.PickFile();
                // the data Array of the file will be found in filedata.DataArray 
                // file name will be found in filedata.FileName;
                // etc etc.

            }
            catch (Exception)
            {
            }

        }
        private async void AddAnotherPet_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPet());
        }
    }
}