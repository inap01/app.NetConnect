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
    [Application]
    class AppStart : Application, Application.IActivityLifecycleCallbacks
    {
        private DataContext context;
        public AppStart(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            DataContext d = DataContext.GetDataContext();
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