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
using MonoNetConnect.ApiModel;

namespace MonoNetConnect
{
    public abstract partial class BaseProperties
    {
        [JsonProperty("ID")]
        public Int32 ID { get; set; }
        [JsonProperty("latest_change")]
        protected DateTime LatestChange { get; set; } = DateTime.MinValue;

        public DateTime GetLatestChange()
        {
            return this.LatestChange;
        }
    }
    public class BasicAPIModel
    {
        [JsonProperty("status")]
        public StatusModel Status { get; set; }
    }
    public class BasicAPIModel<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("status")]
        public StatusModel Status { get; set; }
    }
}