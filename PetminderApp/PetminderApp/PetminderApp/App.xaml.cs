﻿using PetminderApp.Api;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new CustomMaster());
             MainPage = new NavigationPage(new MainPage());
            // MainPage = new CustomMaster();

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
