using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PetminderApp.Files
{
    public interface IDeviceExternalStorage
    {
        bool SaveFile(string FileName, byte[] Data, string Type, string MimeType);
        Task<bool> CheckStoragePermission();
    }
}
