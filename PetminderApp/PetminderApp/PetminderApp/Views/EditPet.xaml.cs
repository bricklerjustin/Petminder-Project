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
    public partial class EditPet : ContentPage
    {
       
        public IList<Pet> Pets { get; set; }
        public EditPet()
        {
            InitializeComponent();
            Pets = new List<Pet>();
            Pets.Add(new Pet
            {
                Name = "Oliver",
                // Will come from a URL or file Stream

               // Image = new Image { Source = "PetminderApp/Images/Logo.png" }
                Image = new Image { Source = "{local:ImageResource PetminderApp.Images.PetButton.png}" }

            });

            BindingContext = this;
            
        }

        // Class that brings up pets added and their corresponding image downloaded
        public class Pet
        {
            public string Name { get; set; }
            public Image Image { get; set; }
            public object ImageSource { get; internal set; }
        }
    }
}