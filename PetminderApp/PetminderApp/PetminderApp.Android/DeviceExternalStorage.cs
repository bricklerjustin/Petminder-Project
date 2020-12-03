using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PetminderApp.Files;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PetminderApp.Droid.DeviceExternalStorage))]
namespace PetminderApp.Droid
{ 
    public class DeviceExternalStorage : IDeviceExternalStorage
    {
        public async Task<bool> CheckStoragePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (status != PermissionStatus.Granted)
            {
                return false;
            }

            return true;
        }

        public bool SaveFile(string FileName, byte[] Data, string Type, string MimeType)
        {

            try
            {
                // Determine where to save your file
                var downloadDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
                var filePath = Path.Combine(downloadDirectory, FileName + Type);

                // Create and save your file to the Android device
                var streamWriter = File.Create(filePath);
                streamWriter.Close();
                File.WriteAllBytes(filePath, Data);

                var downloadManager = DownloadManager.FromContext(Android.App.Application.Context);
                downloadManager.AddCompletedDownload(FileName, "Petminder download", true, MimeType, filePath, File.ReadAllBytes(filePath).Length, true);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}