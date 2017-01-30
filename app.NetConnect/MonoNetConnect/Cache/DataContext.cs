using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoNetConnect.InternalModels;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MonoNetConnect.Cache
{
    [Serializable]
    public partial class DataContext
    {
        private Dictionary<String, Action> ActivityCallBackFunctions { get; set; }
        private DateTime? lastUpdated = DateTime.MinValue;
        private Boolean _isInitialLoad { get; set; } = true;
        private String ApiImagesPath = "images";
        private String DataContextFilePath { get; set; }
        private static DataContext current = null;

        public Data<Tournament> Tournaments { get; set; } = new Data<Tournament>();
        public User User { get; set; } = new User();
        public Settings Settings { get; set; } = new Settings();
        public Data<Sponsor> Sponsors { get; set; } = new Data<Sponsor>();
        public Data<Product> Products { get; set; } = new Data<Product>();

        private DataContext()
        {

        }
        public static DataContext GetDataContext()
        {            
            return current;
        }
        public static void InitializeDataContext(String path)
        {
            if (current != null)
            {
                if (current?.DataContextFilePath != null)
                {
                    if (File.Exists(current.DataContextFilePath))
                    {
                        current.LoadDataContextFromFile();
                    }
                }
            }
            else
            {
                current = new DataContext();
            }
            if (DateTime.Now > current?.lastUpdated?.AddMinutes(2))
            {
                current.MyDirPath = path;
                current.UpdateDataContext();
            }
        }
        public async Task<Task> UpdateSingleProperty<T>(String PropertyName, Type propType)
            where T : IApiModels
        {
            return UpdateAsyncIfNeeded<T>(PropertyName, propType);
        }
        public async Task<Task> UpdateSingleProperty<T>(String PropertyName, Type propType, string[] Conditions)
            where T : IApiModels
        {
            throw new NotImplementedException();
        }
        public async void UpdateDataContext()
        {
            lastUpdated = DateTime.Now;
            List<Task> ImageTasks = new List<Task>();
            Task[] UpdateTasks = new Task[]
            {
                Task.Run(() =>
                {
                    Task Image = UpdateSingleProperty<Settings>("Settings",typeof(Settings));
                    Image?.Start();

                }),
                Task.Run(() =>
                {
                    Task Image = UpdateSingleProperty<Data<Tournament>>("Tournaments",typeof(Tournament));
                    Image?.Start();                    
                }),
                Task.Run(() =>
                {
                    Task Image = UpdateSingleProperty<Data<Product>>("Products",typeof(Product));
                    Image?.Start();
                }),
                Task.Run(() =>
                {
                    Task Image = UpdateSingleProperty<Data<Sponsor>>("Sponsors", typeof(Sponsor));
                    Image?.Start();
                })
            };
            Task.WhenAll(UpdateTasks).ContinueWith((a) =>
            {
                callBackTest();
            });      
        }
        public void callBackTest()
        {

        }
    }
}