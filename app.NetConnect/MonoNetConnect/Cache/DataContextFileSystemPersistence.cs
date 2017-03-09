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
using Newtonsoft.Json;
using System.IO;

namespace MonoNetConnect.Cache
{
    public partial class DataContext
    {
        public static void SaveDataContext()
        {
            DataContext copy = DataContext.GetDataContext();

            try
            {
                var json = JsonConvert.SerializeObject(copy);
                File.WriteAllText(DataContextFilePath, json);
            }
            catch(Exception ex)
            { }
        }
        public static void LoadDataContextFromFile()
        {
            try
            {
                current = JsonConvert.DeserializeObject<DataContext>(File.ReadAllText(DataContextFilePath));
            }
            catch(Exception ex)
            { }
        }
    }
}