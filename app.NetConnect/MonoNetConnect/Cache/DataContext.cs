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
    public partial class DataContext
    {
        private Boolean _isInitialLoad { get; set; } = true;

        private static DataContext current = null;
        
        public Tournament Tournament { get; set; }
        public User User { get; set; }
        public Settings Settings { get; set; }
        public Data<Sponsor> Sponsors { get; set; }
        public Data<Product> Products { get; set; }

        private DataContext()
        {            
        }
        public static DataContext GetDataContext()
        {
            return current;
        }
        public void UpdateDataContext()
        {
            UpdateAsyncIfNeeded<User>();
            UpdateAsyncIfNeeded<Settings>();
            UpdateAsyncIfNeeded<Tournament>();
            if (!_isInitialLoad)
            {
                foreach (var sponsor in Sponsors)
                    UpdateAsyncIfNeeded<Sponsor>(sponsor);
                foreach (var product in Products)
                    UpdateAsyncIfNeeded<Product>(product);
            }
            else
            {
                UpdateAsyncIfNeeded<Data<Sponsor>>();
                UpdateAsyncIfNeeded<Data<Product>>();
            }
        }
    }
}