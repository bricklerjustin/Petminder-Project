using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetminderApp.Files
{
    public interface IDeviceExternalStorage
    {
        bool SaveFile(FileData file);
        FileData ReadFile();
    }
}
