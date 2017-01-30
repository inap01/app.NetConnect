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
using System.IO;

namespace MonoNetConnect.Cache
{
    public partial class DataContext
    {

        public class BoolConverter : JsonConverter
        {
            
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(bool);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                switch (reader.Value.ToString().ToLower().Trim())
                {
                    //case "true":
                    //case "yes":
                    //case "y":
                    case "1":
                        return true;
                    //case "false":
                    //case "no":
                    //case "n":
                    case "0":
                        return false;
                }
                // If we reach here, we're pretty much going to throw an error so let's let Json.NET throw it's pretty-fied error message.
                return new JsonSerializer().Deserialize(reader, objectType);
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
            }
        }

        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };

        private BasicAPIModel<T> ParseStringToModel<T>(String json)
        {
            try
            {
                jsonSettings.Converters.Add(new BoolConverter());

                BasicAPIModel<T> newModel = JsonConvert.DeserializeObject<BasicAPIModel<T>>(json, jsonSettings);
                return newModel;
            }
            catch (Exception ex)
            {                
                throw new Exception("JsonParseException",ex);
            }
        }
        private T ParseObjectFromJsonWrapper<T>(String json)
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            foreach(var prop in model.GetType().GetProperties())
            {
                if(IsSimpleType(prop.PropertyType))
                {
                    prop.SetValue(prop.Name, PropertyFromJsonString<T>(prop, json));
                }
                else
                {
                    MethodInfo method = this.GetType().GetMethod(MethodBase.GetCurrentMethod().Name,BindingFlags.NonPublic);
                    MethodInfo generic = method.MakeGenericMethod(prop.PropertyType);
                    generic.Invoke(null, new object[] { json });
                }
            }
            return model;
        }
        private T ParseObjectFromJson<T>(String json)
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            foreach(var prop in model.GetType().GetProperties())
            {
                prop.SetValue(prop.Name, PropertyFromJsonString<T>(prop, json));
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
            if (attr.Any(x => x is ApiPropertyName))
                return (attr.SingleOrDefault(x => x is ApiPropertyName) as ApiPropertyName).Name;
            return null;
        }
        private String ApiPathNameValue<T>(T model)
        {
            var attr = typeof(T).GetCustomAttributes(false);
            if (attr[0] is ApiPathName)
                return (attr[0] as ApiPathName).Name;
            return null;
        }
        private Boolean IsSimpleType(Type t)
        {
            if (t.IsPrimitive)
                return true;
            if (t.Module.ScopeName.Equals("CommonLanguageRuntimeLibrary"))
                return true;
            return false;
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