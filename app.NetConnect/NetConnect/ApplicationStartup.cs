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
using MonoNetConnect.Cache;

namespace NetConnect
{
    public class ApplicationStartup : Application, Application.IActivityLifecycleCallbacks
    {
        int a = 1;
        public ApplicationStartup()
        {
            DataContext data = DataContext.GetDataContext();
            data.UpdateDataContext();
            AppDomain domain = AppDomain.CurrentDomain;

        }
        void IActivityLifecycleCallbacks.OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivityDestroyed(Activity activity)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivityPaused(Activity activity)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivityResumed(Activity activity)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivityStarted(Activity activity)
        {
            throw new NotImplementedException();
        }

        void IActivityLifecycleCallbacks.OnActivityStopped(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}