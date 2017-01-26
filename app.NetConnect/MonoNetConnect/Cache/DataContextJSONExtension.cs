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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace MonoNetConnect.Cache
{
    public partial class DataContext
    {
        
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
        private T ParseStringToModel<T>(String json)
        {
            try
            {
                T newModel = JsonConvert.DeserializeObject<T>(json, jsonSettings);
                return newModel;
            }
            catch
            (Exception ex)
            {
                try
                {
                    return ParseObjectFromJson<T>(json);
                }
                catch(Exception innerEx)
                {

                    return default(T);
                }
                // ParseErrorFromJson Custom Parsing
            }
        }
        private T ParseObjectFromJson<T>(String Model)
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            foreach(var prop in model.GetType().GetProperties())
            {
                prop.SetValue(prop.Name, PropertyFromJsonString<T>(prop, Model));
            }
            return model;
        }
        private dynamic PropertyFromJsonString<T>(PropertyInfo prop, String json)
        {
            String apiName = ApiPropNameValue(prop);
            JToken token = JObject.Parse(json);
            dynamic value = token.SelectToken(prop.Name);
            return Convert.ChangeType(value, prop.PropertyType);
        }
        private String ApiPropNameValue(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttributes(false);
            if (attr[0] is ApiPropertyName)
                return (attr[0] as ApiPropertyName).Name;
            return null;
        }
        private String ApiPathNameValue(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttributes(false);
            if (attr[0] is ApiPathName)
                return (attr[0] as ApiPathName).Name;
            return null;
        }
    }

    [MetadataType(typeof(BaseProperties))]      
    public class ApiPathName : Attribute
    {
        public String Name;
        public ApiPathName(String name)
        {
            this.Name = name;
        }
    }

    [MetadataType(typeof(BaseProperties))]
    public class ApiPropertyName : Attribute
    {
        public String Name;
        public ApiPropertyName(String name)
        {
            this.Name = name;
        }
    }
    
}