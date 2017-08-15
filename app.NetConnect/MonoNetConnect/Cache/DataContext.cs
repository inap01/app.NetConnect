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
using Newtonsoft.Json;

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
        internal readonly string Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("984825f8-8ac0-4748-8fe4-502f462fdf85"));
        private Dictionary<Activity, Action> ActivityCallBackFunctions { get; set; }
        [JsonProperty]
        private DateTime? lastUpdated = DateTime.MinValue;
        private Boolean _isInitialLoad { get; set; } = true;
        private String ApiImagesPath = "images";
        private String ApiImageUpdateRequest = "api.php/app/ImageUpdates";
        private static String DataContextFilePath { get; set; }
        private static DataContext current = null;
        [JsonProperty]
        public Boolean isLoggedIn { get; set; }
        [JsonProperty]
        public DateTime LatestLoginDate { get; set; }
        [JsonIgnore]
        internal Order CurrentOrder { get; set; } = new Order();
        public ChangesRequestModel Changes { get; set; }
        public User User { get; set; } = new User();
        public Settings Settings { get; set; } = new Settings();
        public Data<Tournament> Tournaments { get; set; } = new Data<Tournament>();
        public Data<Sponsor> Sponsors { get; set; } = new Data<Sponsor>();
        public Data<Product> Products { get; set; } = new Data<Product>();
        public Data<Seat> Seating { get; set; } = new Data<Seat>();

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
            DataContext.DataContextFilePath = Path.Combine(path, "datacontext.json");
            if (false)//File.Exists(DataContext.DataContextFilePath))
            {
                DataContext.LoadDataContextFromFile();
                if ((DateTime.Now - DataContext.GetDataContext().LatestLoginDate).Duration().TotalHours > 72)
                    DataContext.GetDataContext().isLoggedIn = false;
            }
            else
            {
                current = new DataContext();
            }
            if (current?.lastUpdated?.AddMinutes(2) <= DateTime.Now)
            {
                current.MyDirPath = path;
                current.UpdateDataContext().ContinueWith((Task t) =>
                {
                    current._isInitialLoad = false;
                });
            }
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
            lock (current)
            {
                //DataContext.SaveDataContext();
            }
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
        //public void UpdateImageOfSingleProperty2<T>(Data<T> ModelToPost)
        //    where T : IApiModels
        //{
        //    var requiredUpdates = UpdateImagesPost2<T, Data<T>>(ModelToPost, ApiImageUpdateRequest);
        //}
    }
}