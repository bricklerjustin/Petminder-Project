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
    public partial class SearchProviders : ContentPage
    {
        public SearchProviders()
        {
            InitializeComponent();
        }

        public class GroupedReminderList
        {
            public string Title { get; set; }
            public string ShortName { get; set; } //will be used for jump lists
            public string Subtitle { get; set; }
            private GroupedReminderList(string title, string shortName)
            {
                Title = title;
                ShortName = shortName;
            }

            public static IList<GroupedReminderList> All { private set; get; }
        }

        private void lblSearch_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {

        }

        private void Edit_Clicked(object sender, EventArgs e)
        {

        }

        private void Remove_Clicked(object sender, EventArgs e)
        {

        }

    }
}