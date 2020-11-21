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

namespace PetminderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPet : ContentPage
    {   
        public IList<PetReadModel> Pets { get; set; }
        public EditPet()
        {
            InitializeComponent();

            var petsReturn = GetPetList();
            if (petsReturn != null)
            {
                Pets = petsReturn;
            }

            BindingContext = this;
            
        }

        // Class that brings up pets added and their corresponding image downloaded
        //public class Pet
        //{
        //    public string Name { get; set; }
        //    public Image Image { get; set; }
        //    public object ImageSource { get; internal set; }
        //}

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
    }
}