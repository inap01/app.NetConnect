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
using MonoNetConnect.InternalModels;

namespace NetConnect
{
    [Application]
    class AppStart : Application, Application.IActivityLifecycleCallbacks
    {
        DataContext context;
        System.Threading.Timer timer;
        public AppStart(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {
            DataContext.InitializeDataContext(this.ApplicationInfo.DataDir);
            context = DataContext.GetDataContext();
            timer = new System.Threading.Timer(
                (e) =>
                {
                    context.UpdateSingleProperty<Data<Tournament>>("Tournaments", typeof(Tournament));
                },null,0,(int)TimeSpan.FromMinutes(10).TotalMilliseconds);
        }

        public override void OnCreate()
        {            
            
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