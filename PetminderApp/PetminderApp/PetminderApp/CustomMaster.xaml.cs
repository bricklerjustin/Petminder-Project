﻿using System;
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
            MenuItems.Add(new MenuItems { OptionName = "SwitchUser" });
            navigationList.ItemsSource = MenuItems;
            Detail = new NavigationPage(new HomeScreen());

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#2196f3");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#ffffff");
        }

        private  void Item_Tapped(object sender, ItemTappedEventArgs e)
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
                            //Justin please fix this so this will go to the sign in screen
                            //Settings.AccessToken = string.Empty;
                            //Settings.Username = string.Empty;
                            //Settings.Password = string.Empty;
                            //Detail.Navigation.PushAsync(new MainPage());
                            //IsPresented = false;
                        }
                        break;
                    case "SwitchUser":
                        {
                            Detail = new NavigationPage(new SwitchUser());
                            IsPresented = false;
                        }
                        break;
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