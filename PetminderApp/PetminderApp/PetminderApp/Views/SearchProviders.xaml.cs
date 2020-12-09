using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Models.ListViewGroups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchProviders : ContentPage
    {
        private AddFavorite _addFavorite;
        private ObservableCollection<FavoriteGrouped> _grouped { get; set; }
        public SearchProviders()
        {
            InitializeComponent();
            //Add handler for modal return
            Application.Current.ModalPopping += HandleModalPopping;
        }
        private void lblSearch_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {

        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var favoriteModel = contextItem as FavoriteReadModel;
            ShowEditModalPage(favoriteModel);
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var favoriteModel = contextItem as FavoriteReadModel;

            ActivityIndicatorToggle(true);
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var response = client.Delete("api/favorite", UserInfo.Token, favoriteModel.Id);

                    //Refresh List
                    RefreshListView();
                }
                catch
                {
                    //ERROR
                }
                ActivityIndicatorToggle(false);
            });
        }
        private void RefreshListView()
        {
            var favoriteList = GetFavoriteList();
            if (favoriteList != null)
            {
                _grouped = new ObservableCollection<FavoriteGrouped>();
                FavoriteGrouped group = new FavoriteGrouped();

                foreach (string type in favoriteList.Select(x => x.Type).Distinct())
                {
                    group = new FavoriteGrouped() { LongName = type, ShortName = type };

                    foreach (FavoriteReadModel favorite in favoriteList.Where(p => p.Type == type))
                    {
                        group.Add(favorite);
                    }

                    _grouped.Add(group);
                }

                FavoritesListview.ItemsSource = null;
                FavoritesListview.ItemsSource = _grouped;
            }
        }

        private List<FavoriteReadModel> GetFavoriteList()
        {
            RestClient client = new RestClient();
            List<FavoriteReadModel> favorites = new List<FavoriteReadModel>();

            var responseMessage = client.Get("api/favorite", UserInfo.Token);

            if (responseMessage.IsSuccessStatusCode)
            {
                favorites = JsonConvert.DeserializeObject<List<FavoriteReadModel>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            else
            {
                //ERROR
            }

            return favorites;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshListView();
        }

        private void ActivityIndicatorToggle(bool toggle)
        {
            activityIndicator.IsVisible = toggle;
            activityIndicator.IsRunning = toggle;
        }

        private async void ShowEditModalPage(FavoriteReadModel Favorite)
        {
            _addFavorite = new AddFavorite(Favorite);
            await Navigation.PushModalAsync(_addFavorite);
        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _addFavorite)
            {
                RefreshListView();
            }
        }

        private void AddFavorite_Clicked(object sender, EventArgs e)
        {
            _addFavorite = new AddFavorite(null);
            Navigation.PushModalAsync(_addFavorite);
        }

        private async Task OpenBrowser(Uri uri)
        {
            try
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No browser may be installed on this device", "Ok");
            }
        }

        private async Task OpenMapsApp(string location)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                await Launcher.OpenAsync("http://maps.google.com/?daddr=" + location);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stack = sender as StackLayout;
            FavoritesListview.SelectedItem = stack.BindingContext;

            var favorite = FavoritesListview.SelectedItem as FavoriteReadModel;
            string action = "";
            string navigateAction = "Navigate";
            string callAction = "Call";
            string openWebsiteAction = "Open Website";

            if (favorite.Type.ToLower() == "link")
            {
                action = await DisplayActionSheet("Open", "Cancel", null, openWebsiteAction);
            }
            else
            {

                if (!string.IsNullOrEmpty(favorite.Phone) && !string.IsNullOrEmpty(favorite.Url))
                {
                    action = await DisplayActionSheet("Open", "Cancel", null, navigateAction, callAction, openWebsiteAction);
                }
                else if (!string.IsNullOrEmpty(favorite.Phone))
                {
                    action = await DisplayActionSheet("Open", "Cancel", null, navigateAction, callAction);
                }
                else if (!string.IsNullOrEmpty(favorite.Url))
                {
                    action = await DisplayActionSheet("Open", "Cancel", null, navigateAction, openWebsiteAction);
                }
                else
                {
                    action = await DisplayActionSheet("Open", "Cancel", null, navigateAction);
                }
            }

            if (action == navigateAction)
            {
                await OpenMapsApp(favorite.Location);
            }
            else if (action == callAction)
            {
                PhoneDialer.Open(favorite.Phone);
            }
            else if (action == openWebsiteAction)
            {
                await OpenBrowser(new Uri(favorite.Url, UriKind.Absolute));
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            StackLayout stack = sender as StackLayout;
            FavoritesListview.SelectedItem = stack.BindingContext;
        }
    }
}