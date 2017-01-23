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

namespace MonoNetConnect.InternalModels
{
    public class Sponsor
    {
        public String Name { get; set; }
        public String Content { get; set; }
        public String Link { get; set; }
        public String Image { get; set; }
    }
}