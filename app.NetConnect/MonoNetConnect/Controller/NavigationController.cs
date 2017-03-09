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
    public interface INavigationController : IBaseViewController
    {
        void ListItemClicked(int id);
        void ListItemClicked(Type t);
    }
    public class NavigationController : BaseViewController<INavigationController>
    {
        public NavigationController(INavigationController viewController)
            :base(viewController)
        {

        }
        public void ListItemClicked(int id)
        {
            this._viewController.ListItemClicked(id);
        }
        public void ListItemClicked(Type t)
        {
            this._viewController.ListItemClicked(t);
        }
    }
}