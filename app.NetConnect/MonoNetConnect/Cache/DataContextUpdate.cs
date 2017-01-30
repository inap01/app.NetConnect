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
        private static String BasicAPIPath = @"http://lan-netconnect.de/_api";
        private String ExMessage(Exception ex) => String.Join("\n", ex.Message, ex.InnerException, ex.StackTrace);
        private String MethodName () => System.Reflection.MethodBase.GetCurrentMethod().Name;
        private async Task<Task> UpdateAsyncIfNeeded<T>(String PropertyName, Type GenericType)
            where T : IApiModels
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                Task image = UpdateAsync<T>(t.ApiPath(), GenericType, PropertyName);
                return image;           
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
                return null;
            }
        }
        private T GetPropertyOfType<T>(Type t, String PropertyName)
        {
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
        private Task UpdateAsync<T>(String Url, Type GenericType, String PropertyName)
            where T : IApiModels
        {
            Uri path = new Uri(new Uri(BasicAPIPath), Url);
            String p = String.Join("/", BasicAPIPath, Url);
            try
            {
                HttpWebRequest client = new HttpWebRequest(new Uri(p));
                client.Method = "GET";
                using (HttpWebResponse respone = (HttpWebResponse)client.GetResponse())
                {
                    BasicAPIModel<T> mod = ModelFromResponse<T>(respone);
                    Task ImageTask = null;
                    if (mod?.Status.State == DatabaseModels.StatusModel.Status.Success)
                    {
                        SetPropertyOfType<T>(GenericType,PropertyName,mod);
                        if (mod.Data.IsClassWithImage() && mod.Data.GetType().GetInterfaces().Where(x => x.Name == typeof(IList<>).Name).ToList().Count > 0)
                        {
                            ImageTask = CreateImageDownloadTask(GenericType, mod);
                        }
                    }
                    return ImageTask;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {MethodBase.GetCurrentMethod().Name} with {ExMessage(ex)}");
            }
            return null;
        }
        private BasicAPIModel<T> ModelFromResponse<T>(HttpWebResponse respone) where T : IApiModels
        {
            StreamReader content = new StreamReader(respone.GetResponseStream(),true);
            String json = content.ReadToEnd();
            BasicAPIModel<T> mod = ParseStringToModel<T>(json);
            return mod;
        }

        private Task CreateImageDownloadTask<T>(Type InnerGenericType, BasicAPIModel<T> mod)
            where T : IApiModels
        {
            Task ImageTask;
            MethodInfo method = this.GetType().GetMethod("DownloadImagesAsync", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo generic = method.MakeGenericMethod(InnerGenericType);
            ImageTask = new Task(
                () =>
                {
                    generic.Invoke(this, new object[] { mod.Data });
                });
            return ImageTask;
        }

    }
}