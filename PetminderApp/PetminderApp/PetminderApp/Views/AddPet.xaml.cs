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
        private PetReadModel _petModel;
        private Page _returnPage;

        public AddPet(PetReadModel PetModel, Page ReturnPage)
        {
            InitializeComponent();

            submitPet.Clicked += SubmitPet_Clicked;
            this._petModel = PetModel;
            this._returnPage = ReturnPage;

            if (_petModel != null)
            {
                PetName.Text = _petModel.Name;
                Weight.Text = _petModel.Weight;
                Type.Text = _petModel.Type;
                Breed.Text = _petModel.Breed;
                Age.Text = _petModel.Age.ToString();
            }

            if (_returnPage.GetType() == typeof(HomeScreen))
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            else
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
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

            //If update pet
            if (_petModel != null)
            {
                var response = client.Put("api/pets", _petModel.Id, UserInfo.Token, JsonConvert.SerializeObject(pet));

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    await Navigation.PopAsync();
                    return;
                }

                await DisplayAlert("Update Error", "", "Ok");
            }
            else
            {
                var response = client.Post("api/pets", "", UserInfo.Token, JsonConvert.SerializeObject(pet));

                if (response.StatusCode == System.Net.HttpStatusCode.Created && _returnPage.GetType() == typeof(HomeScreen))
                {
                    var another = await DisplayAlert("", "Would you like to add another pet", "Yes", "No");
                    if (another)
                    {
                        Navigation.InsertPageBefore(new AddPet(null, _returnPage), this);
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
                else if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await Navigation.PopModalAsync();
                    return;
                }

                await DisplayAlert("Creation Error", "", "Ok");
            }         
        }
    }
}