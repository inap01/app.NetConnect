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
    public class Data<T> : List<T>, IApiModels
        where T : IApiModels
    {
        public string ApiPath()
        {
            return ((T)Activator.CreateInstance(typeof(T))).ApiPath().Replace("{/id}", "");
        }

        public DateTime GetLatestChange()
        {
            return this.Max(x => x.GetLatestChange());
        }

        public string ImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public string ImagePath()
        {
            return ((T)Activator.CreateInstance(typeof(T))).ImageDirectoryPath();
        }
    }
}