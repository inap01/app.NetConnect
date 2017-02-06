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

namespace MonoNetConnect.Extensions
{
    public interface IDeepCloneable<T> : IDeepCloneable
    {
        T DeepClone();
    }
    public interface IDeepCloneable
    {
        object DeepClone();
    }
}