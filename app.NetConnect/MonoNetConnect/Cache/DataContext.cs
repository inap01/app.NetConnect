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
using MonoNetConnect.ApiModel;
using Java.Security;
using System.Reflection;

namespace MonoNetConnect.Cache
{

    public interface ISubscriber
    {
        void update();
    }
    [Serializable]
    public partial class DataContext
    {
        List<ISubscriber> subscriber = new List<ISubscriber>();
        private readonly string Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("984825f8-8ac0-4748-8fe4-502f462fdf85"));
        private Dictionary<Activity, Action> ActivityCallBackFunctions { get; set; }
        private DateTime? lastUpdated = DateTime.MinValue;
        private Boolean _isInitialLoad { get; set; } = true;
        private String ApiImagesPath = "images";
        private String ApiImageUpdateRequest = "api.php/app/ImageUpdates";
        private String DataContextFilePath { get; set; }
        private static DataContext current = null;

        internal Order CurrentOrder { get; set; } = new Order();

        
        public ChangesRequestModel Changes { get; set; }
        public Data<Tournament> Tournaments { get; set; } = new Data<Tournament>();
        public User User { get; set; } = new User();
        public Settings Settings { get; set; } = new Settings();
        public Data<Sponsor> Sponsors { get; set; } = new Data<Sponsor>();
        public Data<Product> Products { get; set; } = new Data<Product>();

        private DataContext()
        {
            
        }
        private void Notify()
        {
            foreach (var sub in subscriber)
            {
                System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} Notifying {sub}");
                sub.update();                
            }
        }
        public void Attach(ISubscriber sub)
        {
            subscriber.Add(sub);
        }
        public void Detach(ISubscriber sub)
        {
            subscriber.Remove(sub);
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
            if (current?.lastUpdated?.AddMinutes(2) <= DateTime.Now)
            {
                current.MyDirPath = path;
                current.UpdateDataContext();
            }
            current._isInitialLoad = false;
            //current.SaveDataContext();
        }
        public Boolean UpdateSingleProperty<T>(String PropertyName, Type propType)
            where T : IApiModels
        {
            return UpdateAsyncIfNeeded<T>(PropertyName, propType);
        }
        public void UpdateDataContext(Action CallBackDelegate)
        {
            GenerateAndRunUpdateTasks();
        }
        public async Task UpdateDataContext()
        {
            var task = Task.Factory.StartNew(() => GenerateAndRunUpdateTasks());
            await task;
            Notify();
        }

        /// <summary>
        /// Updates a Group or Single Properties Images i.e. Data<Product> or a single Product use in Controller
        /// </summary>
        /// <typeparam name="T">Type of The Model to be updated</typeparam>
        /// <param name="ModelToPost">The Property that has to be Updated</param>
        public void UpdateImageOfSingleProperty<T>(T ModelToPost)
            where T : IApiImageModel
        {
            var images = UpdateImagesPost<T, List<String>>(ModelToPost, ApiImageUpdateRequest);
            DownloadImagesAsync<T>(images);
        }

        public void callBackTest()
        {

        }
    }
}