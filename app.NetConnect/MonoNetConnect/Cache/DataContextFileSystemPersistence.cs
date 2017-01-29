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
        
        private void SaveDataContext()
        {
            DataContext copy = this;
            var json = JsonConvert.SerializeObject(copy);
            using (FileStream fs = new FileStream(DataContextFilePath, FileMode.CreateNew))
            {
                var data = new UTF8Encoding(true).GetBytes(json);
                fs.WriteAsync(data, 0, data.Length);
            }            
        }
        private void LoadDataContextFromFile()
        {
            current = JsonConvert.DeserializeObject<DataContext>(DataContextFilePath);
        }

    }
}