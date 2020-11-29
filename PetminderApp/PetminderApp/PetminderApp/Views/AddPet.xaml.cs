using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PetminderApp.Api.Api_Models;
using PetminderApp.Api;
using Newtonsoft.Json;

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
            RestClient client = new RestClient();
            PetCreateModel pet = new PetCreateModel();
            pet.Name = PetName.Text;
            pet.Weight = Weight.Text;
            pet.Type = Type.Text;
            pet.Breed = Breed.Text;
            pet.AccountId = UserInfo.AccountId;
            
            if (!int.TryParse(Age.Text, out int _age))
            {
                await DisplayAlert("Input Error", "Age must be a whole number", "Ok");
                return;
            }

            pet.Age = _age;
            var response = client.Post("api/pets", "", UserInfo.Token, JsonConvert.SerializeObject(pet));

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var another = await DisplayAlert("", "Would you like to add another pet", "Yes", "No");
                if (another)
                {
                    Navigation.InsertPageBefore(new AddPet(), this);
                    await Navigation.PopAsync();
                    return;
                }
                else
                {
                    Navigation.InsertPageBefore(new HomeScreen(), this);
                    await Navigation.PopAsync();
                    return;
                }
            }

            await DisplayAlert("Creation Error", "", "Ok");
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