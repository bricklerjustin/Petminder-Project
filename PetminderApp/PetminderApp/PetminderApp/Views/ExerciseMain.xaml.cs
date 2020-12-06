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
            List<Grid> layouts = new List<Grid>();
            _pets = new List<PetReadModel>();
            int i = 0;

            InitializeComponent();

            exercises = RefreshExerciseData();
            _pets = RefreshPetData();

            if (_pets.Count > 0)
            {
                double time = 0;
                double distance = 0;
                double timeHours = 0;
                string timeUnit = "";

                foreach (var pet in _pets)
                {
                    time = 0;
                    distance = 0;
                    timeHours = 0;
                    timeUnit = "Min"; ;
                    var timeEntries = exercises.Where(p => p.PetId == pet.Id);

                    if (timeEntries.Count() > 0)
                    {
                        time = timeEntries.Select(x => x.Time.TotalMinutes).Sum();
                        timeHours = timeEntries.Select(x => x.Time.TotalHours).Sum();
                    }

                    var distanceEntries = exercises.Where(p => p.PetId == pet.Id);
                    
                    if (distanceEntries.Count() > 0)
                    {
                        distance = distanceEntries.Select(x => x.Distance).Sum();
                    }

                    var name = pet.Name;

                    //if (name.Length < 15)
                    //{
                    //    name = name.PadRight(17, 'a');
                    //}
                    //else if (name.Length > 15)
                    //{
                    //    name = name.Remove(11) + "...";
                    //}

                    if (time >= 60)
                    {
                        time = timeHours;
                        timeUnit = "Hr";
                    }

                    //var stackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    var grid = new Grid() {};
                    ColumnDefinition columnDefinition = new ColumnDefinition() { Width = GridLength.Star};
                    ColumnDefinition columnDefinition2 = new ColumnDefinition() { Width = 50 };
                    RowDefinition rowDefinition = new RowDefinition() { Height = 50 };
                    grid.ColumnDefinitions.Add(columnDefinition);
                    grid.ColumnDefinitions.Add(columnDefinition);
                    grid.ColumnDefinitions.Add(columnDefinition);
                    grid.ColumnDefinitions.Add(columnDefinition2);
                    grid.RowDefinitions.Add(rowDefinition);

                    grid.Children.Add(new Label()
                    {
                        Text = name,
                        FontSize = 16,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center
                    }, 0, 0);

                    grid.Children.Add(new Label()
                    {
                        Text = (distance.ToString("0.00") + " Mi"),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 16,                    
                    }, 1, 0);

                    grid.Children.Add(new Label()
                    {
                        Text = (time.ToString("0.00") + $" {timeUnit}"),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 16
                    }, 2, 0);

                    Switch selectedSwitch = new Switch { HorizontalOptions = LayoutOptions.End};

                    grid.Children.Add(selectedSwitch, 3, 0);

                    layouts.Add(grid);

                    i++;
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
                    Grid layout = petRow.View as Grid;

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