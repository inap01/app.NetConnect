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

namespace MonoNetConnect.Cache
{
    [Serializable]
    public partial class DataContext
    {
        private Boolean _isInitialLoad { get; set; } = true;
        
        private String DataContextFilePath { get; set; }
        public static DataContext current = null;

        public Tournament Tournament { get; set; } = new Tournament();
        public User User { get; set; } = new User();
        public Settings Settings { get; set; } = new Settings();
        public Data<Sponsor> Sponsors { get; set; } = new Data<Sponsor>();
        public Data<Product> Products { get; set; } = new Data<Product>();

        private DataContext()
        {            
        }
        public static DataContext GetDataContext()
        {
            if (current == null)
            {
                current = new DataContext();
                current.UpdateDataContext();
            }
            return current;
        }
        public async void UpdateDataContext()
        {
            List<Task> ImageTasks = new List<Task>();
            Task[] UpdateTasks = new Task[]
            {
                new Task(async() =>
                {
                    await UpdateAsyncIfNeeded<User>()
                    .ContinueWith((Task imageTask) =>
                    {
                        imageTask?.Start();
                    });
                }),
                new Task(async() =>
                {
                    await UpdateAsyncIfNeeded<Settings>()
                    .ContinueWith((Task imageTask) =>
                    {
                        imageTask?.Start();
                    });
                }),
                new Task(async() =>
                {
                    await UpdateAsyncIfNeeded<Tournament>()
                    .ContinueWith((Task imageTask) =>
                    {
                        imageTask?.Start();
                    });
                }),
                new Task(async() =>
                {
                    await UpdateAsyncIfNeeded<Data<Sponsor>>()
                    .ContinueWith((Task imageTask) =>
                    {
                        imageTask?.Start();
                    });
                }),
                new Task(async() =>
                {
                    await UpdateAsyncIfNeeded<Data<Sponsor>>()
                    .ContinueWith((Task imageTask) =>
                    {
                        imageTask?.Start();
                    });
                })
            };
            Task.WhenAll(UpdateTasks).ContinueWith(
                (Task a) => {
                    
            });
            
        }
    }
}