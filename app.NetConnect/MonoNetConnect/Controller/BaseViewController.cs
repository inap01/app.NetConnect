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

namespace MonoNetConnect.Controller
{
    public interface IBaseViewController
    {
        void SetActivityTitle();
    }
    public abstract class BaseViewController<T>
        where T : IBaseViewController
    {
        protected T _viewController { get; set; }
        protected DataContext dataContext;
        protected BaseViewController(T viewController)
        {
            this._viewController = viewController;
            dataContext = DataContext.GetDataContext();
        }
        public bool IsLoggedIn()
        {
            if (dataContext != null)
                return dataContext.isLoggedIn;
            else
                return false;
        }
        public void LogoutUser()
        {
            dataContext.User = null;
            dataContext.isLoggedIn = false;
        }
    }
}