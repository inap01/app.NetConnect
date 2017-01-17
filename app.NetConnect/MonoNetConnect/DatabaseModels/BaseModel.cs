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

namespace MonoNetConnect.DatabaseModels
{
    public class BaseProperties
    {
        Int32 ID { get; set; }
    }
    public class BaseModel<T>
    {
        T Model { get; set; }
        StatusModel Status { get; set; }
    }
}