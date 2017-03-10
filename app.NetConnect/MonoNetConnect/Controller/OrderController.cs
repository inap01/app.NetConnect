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
        void PopulateRadioButtonGrid(List<int> seatNumbers);
        int GetSelectedSeat();
    }
    
    public class OrderController : BaseViewController<IOrderController>
    {
        public OrderController(IOrderController viewController)
            : base(viewController)
        {
        }
        public void PopulateRadioButtonGrid()
        {
            var x1 = dataContext.Seating.Where(x => x.UserID == dataContext.User.ID);
            var x2 = x1.Select(x => x.ID);
            this._viewController.PopulateRadioButtonGrid(dataContext.Seating.Where(x => x.UserID == dataContext.User.ID).Select(x => x.ID).ToList());
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
            dataContext.CurrentOrder.UserID = dataContext.User.ID;
            dataContext.CurrentOrder.SeatID = this._viewController.GetSelectedSeat();
            dataContext.PostRequestWithExpectedResult<Order, BasicAPIModel>(dataContext.CurrentOrder, dataContext.CurrentOrder.ApiPath(), null);
            return true;
        }
    }
}