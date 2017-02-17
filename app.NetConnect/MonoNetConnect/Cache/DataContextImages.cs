using System;
using System.Collections.Generic;
using Android.OS;
using MonoNetConnect.InternalModels;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Android.Graphics;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace MonoNetConnect.Cache
{
    public partial class DataContext
    {
        private String MyDirPath { get; set; }
        private void DownloadImagesAsync<T>(Data<T> model) 
            where T : IApiImageModel
        {

            System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
            Action<T> downloadAction =
            (T x) =>
            {
                DownloadImage<T>(((IHasImage)x).GetImage());                        
            };
            model.ForEach(downloadAction);         
        }
        private void DownloadImagesAsync<T>(List<String> images)
            where T : IApiImageModel
        {

            System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
            Action<String> downloadAction = (x) =>
            {
                DownloadImage<T>(x);
            };
            images.ForEach(downloadAction);
        }
        private void DownloadImage<T>(string fileName)
            where T : IApiModels
        {

            System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {fileName}");
            var model = ((T)Activator.CreateInstance(typeof(T)));
            String _url = fileName;
            Uri url = new Uri(_url);
            using (WebClient client = new WebClient())
            {
                byte[] result = null;
                result = client.DownloadData(url.ToString());
                SaveImage(result, model.GetImageDirectoryPath(), fileName);
                result = null;
            }
        }
        private void SaveImage(byte[] data, string path, string fileName)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {fileName}");
                if (!Directory.Exists(System.IO.Path.Combine(MyDirPath, path)))
                    Directory.CreateDirectory(System.IO.Path.Combine(MyDirPath, path));                    
                String fullPath = System.IO.Path.Combine(MyDirPath, path, fileName.Split('/').Last());
                using (Stream outStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (Bitmap bm = BitmapFactory.DecodeByteArray(data, 0, data.Length))
                    {
                        bm.Compress(Bitmap.CompressFormat.Png, 70, outStream);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
        }
        private async Task ResolveUnreferencedImages<T>(T model)
            where T : IApiModels
        {

            if (model == null)
                return;
            
            if ((typeof(IApiImageModel)).IsAssignableFrom(typeof(T)))
            {
                try
                {
                    var images = ((IApiImageModel)model).GetImages();
                    var files = Directory.GetFiles(String.Join("/", MyDirPath, model.GetImageDirectoryPath()));
                    var filesToDelete = files.Where(x => !images.Any(y => x.Split('/').Last() == y.Split('/').Last())).ToList();
                    Action<string> delete = (filename) =>
                    {
                        string delPath = String.Join("/", MyDirPath, model.GetImageDirectoryPath(), filename.Split('/').Last());
                        System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {delPath}");
                        File.Delete(delPath);
                    };
                    filesToDelete.ForEach(delete);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
                }                
            }
            return;
        }
    }
}