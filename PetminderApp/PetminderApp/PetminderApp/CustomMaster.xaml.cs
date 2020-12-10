using PetminderApp.Api;
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
    public partial class CustomMaster : MasterDetailPage
    {
        // Make object for Menu Items
        List<MenuItems> MenuItems;
        public CustomMaster()
        {
            InitializeComponent();
            // Object
            MenuItems = new List<MenuItems>();
            MenuItems.Add(new MenuItems { OptionName = "HomeScreen" });
            MenuItems.Add(new MenuItems { OptionName = "Logout" });
           // MenuItems.Add(new MenuItems { OptionName = "Settings" });
            navigationList.ItemsSource = MenuItems;
            Detail = new NavigationPage(new HomeScreen());

            if (Application.Current.MainPage != null)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#2196f3");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#ffffff");
            }
        }

        private async void Item_Tapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as MenuItems;

                switch (item.OptionName)
                {
                    // These are the pages we will need to add
                    case "HomeScreen":
                        {
                            Detail = new NavigationPage(new HomeScreen());
                            IsPresented = false;
                        }
                        break;
                    case "Logout":
                        {
                            var logout = await DisplayAlert("Logout", "Confirm Logout?", "Yes", "No");
                            if (logout)
                            {
                                UserInfo.LoggedIn = false;
                                await this.Navigation.PopToRootAsync();
                            }
                        }
                        break;
                   // case "Settings":
                    //    {
                            //Detail = new NavigationPage(new Settings());
                     //       IsPresented = false;
                     //   }
                     //   break;
                }
            }
            catch (Exception ex)
            {

            }
        }
       
    }
    public class MenuItems
    {
        public string OptionName { get; set; }
    }
}