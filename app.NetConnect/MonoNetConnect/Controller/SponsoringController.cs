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
    public interface ISponsoringController : IBaseViewController
    {

    }

    public class SponsoringController : BaseViewController<ISponsoringController>
    {
        public SponsoringController(ISponsoringController viewController)
            : base (viewController)
        {

        }
    }
}