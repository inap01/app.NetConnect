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

namespace MonoNetConnect.Utility
{
    public interface IPublisher
    {
        void Attach(ISubcscriber sub);
        void Detach(ISubcscriber sub);
        void Notify(ISubcscriber sub);
    }
    public interface ISubcscriber
    {
        void Update();
    }
}