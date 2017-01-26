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
    public class Data<T> : List<T>, IApiPath
        where T : IApiPath
    {
        public string ApiPath()
        {
            return ((T)Activator.CreateInstance(typeof(T))).ApiPath().Replace("{/id}", "");
        }
    }
}