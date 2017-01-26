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
using MonoNetConnect.DatabaseModels;

namespace MonoNetConnect
{
    public partial class BaseProperties
    {
        public Int32 ID { get; set; }
    }
    public class BasicAPIModel<T>
    {
        T Data { get; set; }
        StatusModel Status { get; set; }
    }
}