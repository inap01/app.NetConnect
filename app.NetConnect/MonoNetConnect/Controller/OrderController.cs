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
    public interface IOrderController : IBaseViewController
    {

    }
    public class OrderController : BaseViewController<IOrderController>
    {
        public OrderController(IOrderController viewController)
            : base(viewController)
        {
        }

        public Boolean Order()
        {
            dataContext.PostRequestWithExpectedResult<Order, BasicAPIModel>(dataContext.CurrentOrder, dataContext.CurrentOrder.ApiPath());
            return true;
        }
    }
}