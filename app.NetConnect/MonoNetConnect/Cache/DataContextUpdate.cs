using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Android.Content;
using MonoNetConnect.InternalModels;
using MonoNetConnect.ApiModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MonoNetConnect.Cache
{

    public partial class DataContext
    {
        private static String BasicAPIPath = @"http://lan-netconnect.de/_api/public";
        private String ExMessage(Exception ex) => String.Join("\n", ex.Message, ex.InnerException, ex.StackTrace);
        private String MethodName () => System.Reflection.MethodBase.GetCurrentMethod().Name;
        private static String MyDirPath(Context c) => c.FilesDir.AbsolutePath;

        
        
        private async Task UpdateAsyncIfNeeded<T>(T model)
           where T : IApiPath
        {
            try
            {
                ApiUpdated api = new ApiUpdated()
                {
                    DateTime = DateTime.Now,
                    ObjectName = typeof(T).Name
                };
                if (await RequestIfUpdateNeeded(api))
                {
                    T t = (T)Activator.CreateInstance(typeof(T));
                    await UpdateAsync<T>(t.ApiPath(),model);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodName()} with {ExMessage(ex)}");
            }
        }
        private async Task UpdateAsyncIfNeeded<T>()
            where T : IApiPath
        {
            try
            {
                ApiUpdated api = new ApiUpdated()
                {
                    DateTime = DateTime.Now,
                    ObjectName = typeof(T).Name
                };
                if (await RequestIfUpdateNeeded(api))
                {
                    T t = (T)Activator.CreateInstance(typeof(T));
                    await UpdateAsync<T>(t.ApiPath(), GetPropertyOfType<T>(typeof(T)));
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodName()} with {ExMessage(ex)}");
            }
        }
        private T GetPropertyOfType<T>(Type t)
        {
            var props = GetType().GetProperties(System.Reflection.BindingFlags.Public);
            var propertyOfTypeT = props.SingleOrDefault(x => x.GetType() == t);
            T value = (T)Activator.CreateInstance(t);
            value = (T)propertyOfTypeT.GetValue(this, null);
            return value;
        }
        private async Task<Boolean> RequestIfUpdateNeeded(ApiUpdated up)
        {
            try
            {
                HttpClient client = new HttpClient();
                var values = new Dictionary<string, string>()
                {
                    { up.ObjectName, up.DateTime.Ticks.ToString() }
                };
                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                });
                Console.WriteLine("5 Sekunden vergangen");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodName()} with {ExMessage(ex)}");
            }

            return true;
        }
        private async Task<T> UpdateAsync<T>(String Url, T Model)
        {
            Uri path = new Uri(new Uri(BasicAPIPath), Url);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                WebResponse response = request.GetResponse();
                String json;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    json = reader.ReadToEnd();
                }
                return ParseStringToModel<T>(json);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodName()} with {ExMessage(ex)}");
            }
            return default(T);
        }
        
    }
}