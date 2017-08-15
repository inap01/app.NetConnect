using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNetConnect
{
    public class BaseViewModel
    {
        [JsonProperty("ID")]
        public Int32 ID { get; set; }
        [JsonProperty("last_change")]
        protected virtual DateTime LatestChange { get; set; } = DateTime.MinValue;

        public BaseViewModel()
        {

        }

        public DateTime GetLatestChange()
        {
            return this.LatestChange;
        }
    }

    public class State
    {
        public enum APIResponseState { Success, Warning, Danger, Info }
        public APIResponseState Status { get; set; }
        public String Message { get; set; }

    }
}
