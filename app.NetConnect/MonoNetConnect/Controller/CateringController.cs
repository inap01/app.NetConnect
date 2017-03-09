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
using System.Threading.Tasks;

namespace MonoNetConnect.Controller
{
    public interface ICateringController : IBaseViewController
    {
        void PopulateGridLayout(Data<Product> products);
        void ShowOrderDialog(Product p);
    }
    public class CateringController : BaseViewController<ICateringController>
    {
        public CateringController(ICateringController viewController)
            :base(viewController)
        {

        }
        public void AddProductToOrder(Product p)
        {
            if (dataContext.CurrentOrder.Products.Where(x => x.Equals(p)).ToList().Count < 1)
                AddProductToOrder(p,1);
            else
                dataContext.CurrentOrder.Products.Where(x => x.Equals(p)).FirstOrDefault().Count++;
        }
        public void AddProductToOrder(Product p, int count)
        {
            dataContext.CurrentOrder.Products.Add(new OrderProduct()
            {
                Attributes = p.Attributes,
                Price = p.Price,
                Count = count,
                ID = p.ID,
                Name = p.Name
            });
        }
        public void RemoveProductFromOrder(Product p)
        {
            dataContext.CurrentOrder.Products.RemoveAll(x => x.Equals(p));
        }
        public void OrderDialog(Int32 ind)
        {
            _viewController.ShowOrderDialog(dataContext.Products[ind]);
        }
        public void setUpUI()
        {
            _viewController.PopulateGridLayout(dataContext.Products);
        }
    }
}