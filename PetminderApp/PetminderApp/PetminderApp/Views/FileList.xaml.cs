
using Newtonsoft.Json;
using PetminderApp.Api;
using PetminderApp.Api.Api_Models;
using PetminderApp.Files;
using PetminderApp.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetminderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileList : ContentPage
    {
        private Guid petId;
        public IList<FileReadModel> Files { get; set; }
        public FileList(Guid petId)
        {
            InitializeComponent();

            this.petId = petId;
            BindingContext = this;
            RunRefreshWork();
        }

        public async void On_FileAdd(object sender, EventArgs e)
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();

            if (fileData != null)
            {
                ActivityIndicatorToggle(true);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await UploadFile(fileData, petId);
                    if (string.IsNullOrEmpty(response))
                    {
                        RunRefreshWork();
                    }
                    else
                    {
                        await DisplayAlert("Error", response, "Ok");
                    }

                    ActivityIndicatorToggle(false);
                });
            }      
        }
        public void On_FileDelete(object sender, EventArgs e)
        {
            RestClient client = new RestClient();
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var fileModel = contextItem as FileReadModel;

            try
            {
                ActivityIndicatorToggle(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    client.Delete("api/files", UserInfo.Token, fileModel.Id);
                    RunRefreshWork();
                    ActivityIndicatorToggle(false);
                });
            }
            catch
            {
                //ERROR
            }

            ActivityIndicatorToggle(false);
        }
        public void On_FileRename(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var fileModel = contextItem as FileReadModel;

            Device.BeginInvokeOnMainThread(async () =>
            {
                var response = await RenameFile(fileModel.Id);
                if (!string.IsNullOrEmpty(response))
                {
                    await DisplayAlert("Error", response, "Ok");
                }
                RunRefreshWork();
                ActivityIndicatorToggle(false);
            });
        }

        private void RefreshListViewDataSource()
        {
            FileListview.ItemsSource = null;
            FileListview.ItemsSource = Files;
        }

        private void RunRefreshWork()
        {
            ActivityIndicatorToggle(true);

            Device.BeginInvokeOnMainThread(async () =>
            {
                Files = GetFileListByPet();
                RefreshListViewDataSource();
                ActivityIndicatorToggle(false);
            });
        }

        private async Task<string> RenameFile(Guid id)
        {
            FileUpdateModel fileUpdateModel = new FileUpdateModel();
            RestClient client = new RestClient();

            ActivityIndicatorToggle(false);
            var newName = await DisplayPromptAsync("Rename", "Enter new name");

            if (newName.IndexOf('.') != -1)
            {
                return "Please do not include file type in name";
            }

            ActivityIndicatorToggle(true);
            if (!string.IsNullOrEmpty(newName))
            {
                fileUpdateModel.Name = newName;
                var responseMessage = client.Put("api/files", id, UserInfo.Token, JsonConvert.SerializeObject(fileUpdateModel));
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return "";
                }
                else
                {
                    return "There was an error updating file";
                }
            }

            return "";
        }

        private List<FileReadModel> GetFileListByPet()
        {
            RestClient client = new RestClient();
            List<FileReadModel> files = new List<FileReadModel>();

            var responseMessage = client.Get("api/files", UserInfo.Token);

            if (responseMessage.IsSuccessStatusCode)
            {
                files = JsonConvert.DeserializeObject<List<FileReadModel>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            else
            {
                //ERROR
            }

            files.RemoveAll(p => p.PetId != petId);

            return files;
        }

        private async Task<string> UploadFile(FileData fileData, Guid petId)
        {
            FileCreateModel fileCreateModel = new FileCreateModel();
            RestClient client = new RestClient();

            var dataToUpload = fileData.DataArray;
            var fileName = fileData.FileName;

            if (dataToUpload.Length > 5e+7)
            {
                return "File to large. File must be under 50MB";
            }

            string name = "";
            string type = "";

            var valid = SeperateTypeAndName(fileName, ref name, ref type);
            if (valid)
            {
                fileCreateModel.Type = type;
                fileCreateModel.AccountId = UserInfo.AccountId;
                fileCreateModel.Data = Convert.ToBase64String(dataToUpload);
                fileCreateModel.Name = name;
                fileCreateModel.PetId = petId;

                var response = await client.PostAsync("api/files", "", UserInfo.Token, JsonConvert.SerializeObject(fileCreateModel));

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return "";
                }
                else
                {
                    return "File creation error";
                }
            }

            return "File does not contain type or file name is invlaid";
        }

        private bool SeperateTypeAndName(string Input, ref string Name, ref string Type)
        {
            var typeIndex = Input.LastIndexOf('.');
            var typeIndexFirst = Input.IndexOf('.');

            if (typeIndex != -1 && typeIndex == typeIndexFirst)
            {
                Type = Input.Substring(typeIndex);
                Name = Input.Remove(typeIndex);
                return true;
            }

            return false;
        }

        private void ActivityIndicatorToggle(bool toggle)
        {
            activityIndicator.IsVisible = toggle;
            activityIndicator.IsRunning = toggle;
        }

        private string FileDataDownload(Guid id)
        {
            RestClient client = new RestClient();

            var data = client.Get($"api/files/data/{id}", UserInfo.Token);

            return data.Content.ReadAsStringAsync().Result;
        }

        private async void FileSaveProcess(FileReadModel fileModel)
        {    
            var data = FileDataDownload(fileModel.Id);
            var bytesData = Convert.FromBase64String(data);

            if (Device.RuntimePlatform == Device.Android)
            {
                IDeviceExternalStorage service = DependencyService.Get<IDeviceExternalStorage>();
                var granted = await service.CheckStoragePermission();
                if (granted)
                {
                    service.SaveFile(fileModel.Name, bytesData, fileModel.Type, GetMimeType(fileModel.Type));
                }
            }
        }

        public void On_FileDownload(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext;
            var fileModel = contextItem as FileReadModel;

            try
            {
                ActivityIndicatorToggle(true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    FileSaveProcess(fileModel);
                    ActivityIndicatorToggle(false);
                });
            }
            catch
            {
                //ERROR
            }

            ActivityIndicatorToggle(false);
        }

        private string GetMimeType(string Type)
        {
            switch(Type)
            {
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".jpeg"
                    return "image/jpeg";
                default:
                    return "application/pdf";
            }
        }

    }
}