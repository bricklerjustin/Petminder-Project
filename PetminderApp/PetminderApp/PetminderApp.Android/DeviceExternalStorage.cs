using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PetminderApp.Files;
using Plugin.FilePicker.Abstractions;

namespace PetminderApp.Droid
{
    public class DeviceExternalStorage : IDeviceExternalStorage
    {
        public FileData ReadFile()
        {
            throw new NotImplementedException();
        }

        public bool SaveFile(FileData file)
        {        
            throw new NotImplementedException();
        }
    }
}