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
using Newtonsoft.Json.Serialization;

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
        public class ZerosIsoDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
        {
            /// <summary>
            /// The string representing a datetime value with zeros. E.g. "0000-00-00 00:00:00"
            /// </summary>
            private readonly string _zeroDateString;

            /// <summary>
            /// Initializes a new instance of the <see cref="ZerosIsoDateTimeConverter"/> class.
            /// </summary>
            /// <param name="dateTimeFormat">The date time format.</param>
            /// <param name="zeroDateString">The zero date string. 
            /// Please be aware that this string should match the date time format.</param>
            public ZerosIsoDateTimeConverter(string dateTimeFormat, string zeroDateString)
            {
                DateTimeFormat = dateTimeFormat;
                _zeroDateString = zeroDateString;
            }

            /// <summary>
            /// Writes the JSON representation of the object.
            /// If a DateTime value is DateTime.MinValue than the zeroDateString will be set as output value.
            /// </summary>
            /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The calling serializer.</param>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is DateTime && (DateTime)value == DateTime.MinValue)
                {
                    value = _zeroDateString;
                    serializer.Serialize(writer, value);
                }
                else
                {
                    base.WriteJson(writer, value, serializer);
                }
            }

            /// <summary>
            /// Reads the JSON representation of the object.
            /// If  an input value is same a zeroDateString than DateTime.MinValue will be set as return value
            /// </summary>
            /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value of object being read.</param>
            /// <param name="serializer">The calling serializer.</param>
            /// <returns>
            /// The object value.
            /// </returns>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                return reader.Value.ToString() == _zeroDateString
                    ? DateTime.MinValue
                    : base.ReadJson(reader, objectType, existingValue, serializer);
            }
        }
        internal class CustomDateContractResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                JsonContract contract = base.CreateContract(objectType);
                bool b = objectType == typeof(DateTime);
                if (b)
                {
                    contract.Converter = new ZerosIsoDateTimeConverter("yyyy-MM-dd hh:mm:ss", "0000-00-00 00:00:00");
                }
                return contract;
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
        private T ParseStringToBasicModel<T>(String json)
        {
            try
            {
                jsonSettings.Converters.Add(new BoolConverter());
                T newModel = JsonConvert.DeserializeObject<T>(json, jsonSettings);
                return newModel;
            }
            catch (Exception ex)
            {
                throw new Exception("JsonParseException", ex);
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