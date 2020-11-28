using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetminderApp.Files
{
    public class AppDataReadWrite
    {
        private string _fileName;
        public AppDataReadWrite(string FileName)
        {
            _fileName = FileName;
            _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _fileName);
        }

        public string ReadStringFromFile()
        {
            string readString = "";

            if (File.Exists(_fileName))
            {

                readString = File.ReadAllText(_fileName);
            }

            return readString;
        }

        public bool WriteStringToFile(string Text)
        {
            try
            {
                File.WriteAllText(_fileName, Text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
