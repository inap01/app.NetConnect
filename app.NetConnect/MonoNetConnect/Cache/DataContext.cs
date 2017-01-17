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

namespace MonoNetConnect.Cache
{

    public class DataContext
    {
        public static Dictionary<Int32, String> ImagePathMap { get; set; } = new Dictionary<int, string>();
        private static String SavePath(Context c) => c.FilesDir.AbsolutePath;
        public static string ImagePath(Context c, int id) => String.Concat(SavePath(c),ImagePathMap?.SingleOrDefault(x => x.Key.Equals(id)).Value);
    }
}