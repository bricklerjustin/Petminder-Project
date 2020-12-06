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
    public partial class AddFavorite : ContentPage
    {
        private bool _update = false;
        public AddFavorite(FavoriteReadModel favoriteModel)
        {
            InitializeComponent();

            TypePicker.Items.Add("Location");
            TypePicker.Items.Add("Link");
            TypePicker.SelectedIndex = 0;

            if (favoriteModel != null)
            {
                _update = true;
                Name.Text = favoriteModel.Name;
                Address.Text = favoriteModel.Location;
                Phone.Text = favoriteModel.Phone;
                URL.Text = favoriteModel.Url;
                TypePicker.SelectedItem = favoriteModel.Type;
            }
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = sender as Picker;
            var index = obj.SelectedIndex;

            if (index == 0)
            {
                PhoneFrame.IsVisible = true;
                PhoneLabel.IsVisible = true;
                AddressFrame.IsVisible = true;
                AddressLabel.IsVisible = true;

            }
            else
            {
                PhoneFrame.IsVisible = false;
                PhoneLabel.IsVisible = false;
                AddressFrame.IsVisible = false;
                AddressLabel.IsVisible = false;
            }
        }

        private async void submitFavorite_Clicked(object sender, EventArgs e)
        {
            var valid = await ValidateData(TypePicker.SelectedIndex);

            if (valid)
            {
                RestClient client = new RestClient();
                FavoriteCreateUpdateModel favoriteCreate = new FavoriteCreateUpdateModel();

                favoriteCreate.AccountId = UserInfo.AccountId;
                favoriteCreate.Url = URL.Text;
                favoriteCreate.Type = TypePicker.SelectedItem.ToString();
                favoriteCreate.Name = Name.Text;

                if (TypePicker.SelectedIndex == 0)
                {
                    favoriteCreate.Location = Address.Text;
                    favoriteCreate.Phone = Phone.Text;
                }
                
                
                ActivityIndicatorToggle(true);
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (_update)
                    { }
                    else
                    {
                        var body = JsonConvert.SerializeObject(favoriteCreate);
                        var response = client.Post("api/favorite", "", UserInfo.Token, body);

                        if (response.StatusCode != System.Net.HttpStatusCode.Created)
                        {
                            await DisplayAlert("Error", "There was an error uploading bookmark", "Ok");
                        }
                    }

                    ActivityIndicatorToggle(false);
                });
            }
        }

        private async Task<bool> ValidateData(int Index)
        {

            if (string.IsNullOrEmpty(Name.Text))
            {
                return await ValidationAlert("Name");
            }

            if (Index == 1)
            {
                if (string.IsNullOrEmpty(URL.Text))
                {
                    return await ValidationAlert("URL");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Address.Text))
                {
                    return await ValidationAlert("Address");
                }
            }

            return true;
        }

        private async Task<bool> ValidationAlert(string field)
        {
            await DisplayAlert("Entry Issue", $"The field '{field}' cannot be empty", "Ok");
            return false;
        }

        private void ActivityIndicatorToggle(bool toggle)
        {
            activityIndicator.IsVisible = toggle;
            activityIndicator.IsRunning = toggle;
        }
    }
}