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

namespace MonoNetConnect.Controller
{
    public interface IOverviewController : IBaseViewController
    {
        void someButton(String s);
    }
    public class OverviewController :BaseViewController<IOverviewController> 
    {
        public OverviewController(IOverviewController viewController)
            :base(viewController)
        {

        }
        public void action(string a)
        {
            this._viewController.someButton(a);
        }
    }
}