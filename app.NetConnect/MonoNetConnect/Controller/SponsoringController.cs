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
using MonoNetConnect.InternalModels;

namespace MonoNetConnect.Controller
{
    public interface ISponsoringController : IBaseViewController
    {
        void SetListItems(Data<Sponsor> sponsors);
    }

    public class SponsoringController : BaseViewController<ISponsoringController>
    {
        public SponsoringController(ISponsoringController viewController)
            : base (viewController)
        {

        }
        public void ListItems()
        {
            _viewController.SetListItems(dataContext.Sponsors);
        }
    }
}