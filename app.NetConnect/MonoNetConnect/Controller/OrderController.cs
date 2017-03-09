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
        void PopulateListView(List<OrderProduct> list);
    }
    public class OrderController : BaseViewController<IOrderController>
    {
        public OrderController(IOrderController viewController)
            : base(viewController)
        {
        }
        public Boolean DecrementPartialOrder(int id)
        {
            var item = dataContext.CurrentOrder.Products.Where(x => x.ID == id)?.First();
            if (dataContext.CurrentOrder.Products!= null && item.Count > 1)
            {
                item.Count--;
                if (item.Count == 0)
                {
                    RemoveItemFromOrder(id);
                    return false;
                }
                return true;
            }
            return false;
        }
        public Boolean IncrementPartialOrder(int id)
        {
            var item = dataContext.CurrentOrder.Products.Where(x => x.ID == id)?.First();
            if (dataContext.CurrentOrder.Products != null && item.Count > 0)
            {
                item.Count++;
                return true;
            }
            return false;
        }
        public void RemoveItemFromOrder(int id)
        {
            dataContext.CurrentOrder.Products.RemoveAll(x => x.ID == id);
        }
        public void PopulateListView()
        {
            _viewController.PopulateListView(dataContext.CurrentOrder.Products);
        }
        public Boolean Order()
        {
            dataContext.PostRequestWithExpectedResult<Order, BasicAPIModel>(dataContext.CurrentOrder, dataContext.CurrentOrder.ApiPath(), null);
            return true;
        }
    }
}