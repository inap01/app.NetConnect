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
    public interface ISeatingController : IBaseViewController
    {
        void SetSeatingOrder(Data<Seat> s);
    }
    public class SeatingController : BaseViewController<ISeatingController>
    {
        public SeatingController(ISeatingController viewController)
            :base(viewController)
        {

        }
        public void SetSeatingOrder()
        {
            this._viewController.SetSeatingOrder(dataContext.Seating);
        }

    }
}