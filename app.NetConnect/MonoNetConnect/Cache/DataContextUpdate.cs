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
using Android.Util;
using Java.Security;

namespace MonoNetConnect.Cache
{

    public partial class DataContext
    {
        
        
        private static String BasicAPIPath = @"http://lan-netconnect.de/_api";
        private String ExMessage(Exception ex) => String.Join("\n", ex.Message, ex.InnerException, ex.StackTrace);

        private Z UpdatePostWithResult<T, Z>(T ModelToPost, String relativePath)
            where T : IApiImageModel
        {
            Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {relativePath}");
            Dictionary<string, DateTime> images = new Dictionary<string, DateTime>();
            foreach (var x in ModelToPost.GetImages())
            {
                images.Add(x, ModelToPost.GetLatestChange());
            }
            var content = new StringContent(JsonConvert.SerializeObject(images), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            
            var response = client.PostAsync(String.Join("/",BasicAPIPath,relativePath), content);
            var str = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Z>(str);
        }
        private void GenerateAndRunUpdateTasks()
        {
            Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
            current.lastUpdated = DateTime.Now;
            UpdateSingleProperty<ChangesRequestModel>("Changes", typeof(ChangesRequestModel));

            if (Settings?.GetLatestChange().AddMinutes(2) <= Changes.Settings)
            {
                Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
                UpdateSingleProperty<Settings>("Settings", typeof(Settings));
            }

            if (Tournaments?.GetLatestChange().AddMinutes(2) <= Changes.Tournaments)
            {
                Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
                UpdateSingleProperty<Data<Tournament>>("Tournaments", typeof(Tournament));
            }

            if (Products?.GetLatestChange().AddMinutes(2) <= Changes.Products)
            {
                bool cont = UpdateSingleProperty<Data<Product>>("Products",typeof(Product));
                if(cont)
                {
                    UpdateImageOfSingleProperty<Data<Product>>(Products);
                    ResolveUnreferencedImages<Data<Product>>(Products);
                }
            }

            if (Sponsors?.GetLatestChange().AddMinutes(2) <= Changes.Settings)
            {
                bool cont = UpdateSingleProperty<Data<Sponsor>>("Sponsors", typeof(Sponsor));
                if (cont)
                {
                    UpdateImageOfSingleProperty<Data<Sponsor>>(Sponsors);
                    ResolveUnreferencedImages<Data<Sponsor>>(Sponsors);
                }
            }
        }
        private Boolean UpdateAsyncIfNeeded<T>(String PropertyName, Type GenericType)
            where T : IApiModels
        {
            Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {PropertyName}");
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                return UpdateAsync<T>(t.ApiPath(), GenericType, PropertyName).Result;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
                return false;
            }
        }
        private T GetPropertyOfType<T>(Type t, String PropertyName)
        {

            Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {PropertyName}");
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance);
            var propertyOfTypeT = props.SingleOrDefault(x => x.Name == PropertyName);
            T value = (T)Activator.CreateInstance(t);
            value = (T)propertyOfTypeT.GetValue(this, null);
            return value;
        }
        private void SetPropertyOfType<T>(Type t, String PropertyName, BasicAPIModel<T> model)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance);
            var propertyOfTypeT = props.SingleOrDefault(x => x.Name == PropertyName);
            propertyOfTypeT.SetValue(this, model.Data);
        }
        private async Task<Boolean> UpdateAsync<T>(String Url, Type GenericType, String PropertyName)
            where T : IApiModels
        {

            Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params {PropertyName}");
            Uri path = new Uri(new Uri(BasicAPIPath), Url);
            String p = String.Join("/", BasicAPIPath, Url);
            try
            {
                HttpWebRequest client = new HttpWebRequest(new Uri(p));
                client.Method = "GET";
                Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null);
                using (var responseStream = responseTask.Result.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream);
                    BasicAPIModel<T> mod = ParseStringToModel<T>(reader.ReadToEnd());
                    if(mod.Status.State == ApiModel.StatusModel.Status.Success)
                    {
                        SetPropertyOfType(GenericType, PropertyName, mod);
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
                return false;
            }
            return false;
        }
        private BasicAPIModel<T> ModelFromResponse<T>(HttpWebResponse response) where T : IApiModels
        {
            StreamReader content = new StreamReader(response.GetResponseStream(),true);
            String json = content.ReadToEnd();
            BasicAPIModel<T> mod = ParseStringToModel<T>(json);
            return mod;
        }        
    }
}