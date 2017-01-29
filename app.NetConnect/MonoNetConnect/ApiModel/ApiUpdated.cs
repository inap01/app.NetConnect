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

namespace MonoNetConnect.ApiModel
{
    public class ApiUpdate
    {
        public Int32? ID { get; set; } = null;
        public String ObjectName { get; set; }
        public DateTime DateTime { get; set; }
    }
}