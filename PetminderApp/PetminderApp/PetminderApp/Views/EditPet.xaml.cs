using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using PetminderApp.Views;

namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPet : ContentPage
    {
        private AddPet _AddPetModal;

        public IList<PetReadModel> Pets { get; set; }
        public EditPet()
        {
            //Add handler for modal return
            Application.Current.ModalPopping += HandleModalPopping;

            InitializeComponent();

            RefreshListView();

            BindingContext = this;        
        }

        private List<PetReadModel> GetPetList()
        {
            RestClient client = new RestClient();

            var responseMessage = client.Get("api/pets", UserInfo.Token);
            
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = responseMessage.Content.ReadAsStringAsync();
                List<PetReadModel> pets = JsonConvert.DeserializeObject<List<PetReadModel>>(content.Result);

                return pets;
            }

            return null;
        }

        private void PetAdd_Clicked(object sender, EventArgs e)
        {
            ShowAddPetModalPage(null);
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var petModel = contextItem as PetReadModel;

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    client.Delete("api/pets", UserInfo.Token, petModel.Id);
                    RefreshListView();
                });
            }
            catch
            {
                //ERROR
            }
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var petModel = contextItem as PetReadModel;
            ShowAddPetModalPage(petModel);
        }

        private async void ShowAddPetModalPage(PetReadModel Pet)
        {
            _AddPetModal = new AddPet(Pet, this);
            await Navigation.PushModalAsync(_AddPetModal);
        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _AddPetModal)
            {
                RefreshListView();
            }
        }

        private void RefreshListView()
        {
            var petList = GetPetList();
            Pets = petList;
            PetsListview.ItemsSource = null;
            PetsListview.ItemsSource = Pets;
        }

        private async void Files_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var petModel = contextItem as PetReadModel;
            await Navigation.PushAsync(new FileList(petModel.Id));
        }
    }
}