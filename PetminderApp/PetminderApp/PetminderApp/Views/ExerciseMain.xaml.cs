using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
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
    public partial class ExerciseMain : ContentPage
    {
        //private List<Guid> _selectedPets;
        private List<PetReadModel> _pets;

        public ExerciseMain()
        {
            //_selectedPets = new List<Guid>();

            List<ExerciseReadModel> exercises = new List<ExerciseReadModel>();
            List<StackLayout> layouts = new List<StackLayout>();
            _pets = new List<PetReadModel>();

            InitializeComponent();

            exercises = RefreshExerciseData();
            _pets = RefreshPetData();

            if (_pets.Count > 0)
            {
                double time = 0;
                double distance = 0;

                foreach (var pet in _pets)
                {
                    time = 0;
                    distance = 0;
                    var timeEntries = exercises.Where(p => p.PetId == pet.Id);

                    if (timeEntries.Count() > 0)
                    {
                        time = timeEntries.Select(x => x.Time.TotalMinutes).Sum();
                    }

                    var distanceEntries = exercises.Where(p => p.PetId == pet.Id);
                    
                    if (distanceEntries.Count() > 0)
                    {
                        distance = distanceEntries.Select(x => x.Distance).Sum();
                    }

                    var name = pet.Name;

                    if (name.Length < 15)
                    {
                        name = name.PadRight(17, 'a');
                    }
                    else if (name.Length > 15)
                    {
                        name = name.Remove(11) + "...";
                    }

                    var stackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };

                    stackLayout.Children.Add(new Label()
                    { 
                        Text = name,
                        FontSize = 16,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                    });

                    stackLayout.Children.Add(new Label()
                    {
                        Text = (distance.ToString("0.00") + " Mi").PadLeft(15),
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = 16,                     
                    });

                    stackLayout.Children.Add(new Label()
                    {
                        Text = (time.ToString("0.00") + " Min").PadLeft(15),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 16
                    });

                    Switch selectedSwitch = new Switch { };

                    stackLayout.Children.Add(selectedSwitch);

                    layouts.Add(stackLayout);
                }
            }

            foreach (var layout in layouts)
            {
                PetSection.Add(new ViewCell() { View = layout });
            }
        }

        private List<ExerciseReadModel> RefreshExerciseData()
        {
            RestClient client = new RestClient();
            List<ExerciseReadModel> exercises = new List<ExerciseReadModel>();

            var response = client.Get("api/exercise", UserInfo.Token);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                exercises = JsonConvert.DeserializeObject<List<ExerciseReadModel>>(responseString);
            }
            else
            {
                //ERROR
            }

            return exercises;
        }

        private List<PetReadModel> RefreshPetData()
        {
            RestClient client = new RestClient();
            List<PetReadModel> pets = new List<PetReadModel>();

            var response = client.Get("api/pets", UserInfo.Token);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                pets = JsonConvert.DeserializeObject<List<PetReadModel>>(responseString);
            }
            else
            {
                //ERROR
            }

            return pets;
        }

        private async void gotoExerciseTimer_Clicked(object sender, EventArgs e)
        {
            int i = 0;
            List<Guid> selectedPets = new List<Guid>();
            foreach (ViewCell petRow in PetSection)
            {
                //Data starts on second row
                if (i > 0)
                {
                    StackLayout layout = petRow.View as StackLayout;

                    var children = layout.Children;

                    var element = children.ElementAt(3);
                    
                    if (element.GetType() == typeof(Switch))
                    {
                        Switch elementSwitch = element as Switch;

                        if (elementSwitch.IsToggled)
                        {
                            selectedPets.Add(_pets[i - 1].Id);
                        }
                    }
                }
                i++;
            }

            if (selectedPets.Count == 0)
            {
                await DisplayAlert("Error", "Please select a pet to start exercising", "Ok");
            }
            else
            {
                Navigation.InsertPageBefore(new Exercise(selectedPets), this);
                await Navigation.PopAsync();
            }
        }
    }
}