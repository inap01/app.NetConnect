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
using MonoNetConnect.Controller;

namespace NetConnect.Activities
{
    [Activity(Label = "OrderActivity")]
    public class OrderActivity : BaseActivity<IOrderController, OrderController>, IOrderController
    {
        public override void update()
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.OrderLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new OrderController(this);
            base.OnCreate(savedInstanceState);

            var btn = FindViewById<Button>(Resource.Id.OrderButton);
            btn.Click += (o, e) => 
            {
                this.Controller.Order();
            };
            
            // Create your application here
        }
    }
}