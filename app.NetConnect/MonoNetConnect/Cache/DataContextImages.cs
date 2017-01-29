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
using System.Net;
using System.Net.Http;
using System.IO;
using System.Drawing;
using Android.Graphics;
using Java.IO;
using System.Reflection;

namespace MonoNetConnect.Cache
{
    public partial class DataContext
    {
        private List<MethodInfo> WrappedDownloadImageTasks = new List<MethodInfo>();
        private String MyDirPath { get; set; }
        private IEnumerable<String> SponsorImages
        {
            get
            {
                return Sponsors.Select(x => String.Join("/", MyDirPath, new Sponsor().ImageDirectoryPath(), x.Image));
            }
        }
        private IEnumerable<String> ProductImages
        {
            get
            {
                return Products.Select(x => String.Join("/", MyDirPath, new Product().ImageDirectoryPath(), x.ImageName));
            }
        }
        private void DownloadImagesAsyncSponsorWrapper(Data<Sponsor> model)
        {
            foreach(var sp in model)
                DownloadImageAsync<Sponsor>(sp.Image);
        }

        private void DownloadImagesAsyncProductWrapper(Data<Product> model)
        {
            foreach (var prod in model)
                DownloadImageAsync<Product>(prod.ImageName);
        }
        private async Task DownloadImageAsync<T>(string image)
            where T : IApiModels
        {
            Task.Run(() =>
            {
                DownloadImage<T>(image);
            });
        }
        private void DownloadImagesAsync<T>(Data<T> model) 
            where T : IApiModels           
        {
            model.ForEach(x =>
            {
                Task.Run(() =>
                {
                    foreach(var prop in x.GetType().GetProperties())
                    {
                        if (ApiPropNameValue(prop) == "Image")
                        {
                            DownloadImage<T>(prop.GetValue(x, null).ToString());
                            break;
                        }
                    }
                });
            });
        }
        private async Task DownloadImage<T>(string fileName)
            where T : IApiModels
        {
            var model = ((T)Activator.CreateInstance(typeof(T)));
            String _url = String.Join("/", BasicAPIPath, model.ApiPath(),fileName);
            Uri url = new Uri(_url);
            using (WebClient client = new WebClient())
            {
                byte[] result = null;
                await Task.Run(() =>
                {
                    result = client.DownloadData(url.AbsolutePath);
                }).ContinueWith((Task) =>
                {
                    SaveImage(result, model.ImageDirectoryPath(),fileName);
                });
            }
        }
        private void SaveImage(byte[] data, string path, string fileName)
        {
            try
            {
                String fullPath = System.IO.Path.Combine(MyDirPath, path, fileName);
                using (Stream outStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    Bitmap bm = BitmapFactory.DecodeByteArray(data, 0, data.Length);
                    bm.Compress(Bitmap.CompressFormat.Jpeg, 100, outStream);
                }
            }
            catch(Exception ex)
            {
                MethodBase.GetCurrentMethod();
                System.Diagnostics.Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
            
        }

    }
}