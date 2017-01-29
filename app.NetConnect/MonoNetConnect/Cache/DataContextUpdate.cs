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
using System.Reflection;

namespace MonoNetConnect.Cache
{

    public partial class DataContext
    {
        private static String BasicAPIPath = @"http://lan-netconnect.de/_api/public";
        private String ExMessage(Exception ex) => String.Join("\n", ex.Message, ex.InnerException, ex.StackTrace);
        private String MethodName () => System.Reflection.MethodBase.GetCurrentMethod().Name;
        
        private async Task UpdateAsyncIfNeeded<T>(T model)
           where T : IApiModels
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                await UpdateAsync<T>(t.ApiPath(),model);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
        }
        private async Task UpdateAsyncIfNeeded<T>()
            where T : IApiModels
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                Task image = await UpdateAsync<T>(t.ApiPath(), GetPropertyOfType<T>(typeof(T)));                
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
        }
        private T GetPropertyOfType<T>(Type t)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance);
            var propertyOfTypeT = props.SingleOrDefault(x => x.Name == t.Name);
            T value = (T)Activator.CreateInstance(t);
            value = (T)propertyOfTypeT.GetValue(this, null);
            return value;
        }
        private async Task<Task> UpdateAsync<T>(String Url, T Model)
            where T : IApiModels
        {
            Uri path = new Uri(new Uri(BasicAPIPath), Url);
            try
            {
                HttpClient client = new HttpClient();
                var parameters = new Dictionary<string, string>();
                if (!(Model == null))
                    parameters["TimeStamp"] = Model?.GetLatestChange().ToString();
                else
                    parameters["TimeStamp"] = DateTime.MinValue.ToString();
                //var response = await client.PostAsync(path, new FormUrlEncodedContent(parameters));
                //String json = await response.Content.ReadAsStringAsync();
                String json = json = json = "{\"Data\":{\"FirstName\":\"Joachim\",\"Token\":\"TOKEN\",\"LastName\":\"Gotzes\",\"Nickname\":\"Redan\",\"SteamID\":\"siran94\",\"BattleTag\":\"Redan#2547\",\"ID\":1},\"Status\":{\"State\":0,\"Message\":\"StatusString\"}}";
                BasicAPIModel<T> mod = ParseStringToModel<T>(json);
                Task ImageTask = null;
                if (mod.Data.GetType().GetInterfaces().Contains(typeof(IList<>)))
                {
                    MethodInfo method = this.GetType().GetMethod("DownloadImagesAsync", BindingFlags.NonPublic);
                    MethodInfo generic = method.MakeGenericMethod(mod.Data.GetType().GetGenericArguments().First());
                    ImageTask = new Task(() => { generic.Invoke(null, new object[] { mod.Data }); });
                }
                return ImageTask;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
            return null;
        }
        
        
    }
}