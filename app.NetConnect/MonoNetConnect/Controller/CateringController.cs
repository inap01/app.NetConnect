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
        public async Task<Boolean> OrderProduct(Product p)
        {

            return true;
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