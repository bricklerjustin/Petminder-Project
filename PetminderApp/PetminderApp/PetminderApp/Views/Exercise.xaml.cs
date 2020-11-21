using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        Stopwatch stopwatch;
        public Exercise()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            lblStopwatch.Text = "00:00:00";
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\\:mm\\:ss");
                return true;
            }
            );


        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
        }

        private void btnPause_Clicked(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        
    }

}