using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        Stopwatch stopwatch;
        Boolean stopGeo = false;
        double totalDistance = 0;
        public Exercise()
        {
            InitializeComponent();
            Task<PermissionStatus> task = CheckAndRequestLocationPermission();
            stopwatch = new Stopwatch();
            lblStopwatch.Text = "00:00:00";
            lblDistance.Text = "0.00 miles walked";
        }

        public async void btnStart_Clicked(object sender, EventArgs e)
        {
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\\:mm\\:ss");
                return true;
            }
            );
            
            stopGeo = false;
            await RetrieveLocation();           
        }

        public void btnStop_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            stopGeo = true;
            totalDistance = 0;
            lblDistance.Text = "0.00 miles walked";
        }

        public void btnPause_Clicked(object sender, EventArgs e)
        {
            stopwatch.Stop();
            stopGeo = true;
        }

        public async Task RetrieveLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            //initialize point A
            var positionA = await locator.GetPositionAsync();
            do {
                if (stopGeo == false)
                //wait 10 sec for point B
                await Task.Delay(10000);
                //get new position
                var positionB = await locator.GetPositionAsync();
                Xamarin.Essentials.Location FromLocA = new Xamarin.Essentials.Location(positionA.Longitude, positionA.Latitude);
                Xamarin.Essentials.Location ToLocB = new Xamarin.Essentials.Location(positionB.Longitude, positionB.Latitude);
                //running total of gps distances
                totalDistance += LocationExtensions.CalculateDistance(FromLocA, ToLocB, DistanceUnits.Miles);
                //set point A to point B to collect new distance
                positionA = positionB;
                if (totalDistance == 0)
                {
                    lblDistance.Text = "0.00 miles walked";
                }
                else
                {
                    lblDistance.Text = totalDistance.ToString("#.##") + " miles walked";
                }
                
            } while (stopGeo == false);            
        }

        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;


            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }
    }
}