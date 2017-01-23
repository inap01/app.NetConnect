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
    public interface IContactController : IBaseViewController
    {

    }

    public class ContactController : BaseViewController<IContactController>
    {
        public ContactController(IContactController viewController)
            : base (viewController)
        {

        }
    }
}