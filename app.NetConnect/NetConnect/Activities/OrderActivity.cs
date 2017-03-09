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
using MonoNetConnect.InternalModels;
using Android.Text;
using Android.Text.Style;

namespace NetConnect.Activities
{
    [Activity(Label = "OrderActivity")]
    public class OrderActivity : BaseActivity<IOrderController, OrderController>, IOrderController
    {
        RelativeLayout confirmDeleteDialog;
        OrderAdapter adapter;
        ListView list;
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
            SetUpUi();
            
        }
        public void SetUpUi()
        {
            list = FindViewById<ListView>(Resource.Id.OrderingListView);
            Controller.PopulateListView();
            var btn = FindViewById<Button>(Resource.Id.OrderButton);
            btn.Click += (o, e) =>
            {
                this.Controller.Order();
            };
        }

        public void PopulateListView(List<OrderProduct> products)
        {
            adapter = new OrderAdapter(this, products);
            list.Adapter = adapter;
        }

        public Boolean OpenCancelDialogForResult()
        {
            Action<object, DialogClickEventArgs> cancel;
            AlertDialog.Builder b = new AlertDialog.Builder(this);
            var dialog = b.Create();


            return true;
        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
    }

    public class OrderAdapter : BaseAdapter<OrderProduct>
    {
        private List<OrderProduct> _products;
        private OrderActivity _context;
        public OrderAdapter(OrderActivity activity, List<OrderProduct> products)
        {
            _context = activity;
            _products = products;
        }

        public override OrderProduct this[int position]
        {
            get
            {
                return _products[position];
            }
        }

        public override int Count
        {
            get
            {
                return _products.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this[position];
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.OrderListItemLayout, parent, false);
            var name = convertView.FindViewById<TextView>(Resource.Id.OrderListItemName);
            var details = convertView.FindViewById<TextView>(Resource.Id.OrderListItemDetails);
            var price = convertView.FindViewById<TextView>(Resource.Id.OrderListItemPrice);
            var amount = convertView.FindViewById<TextView>(Resource.Id.OrderListItemCount);
            var inc = convertView.FindViewById<ImageView>(Resource.Id.OrderListItemIncrementItem);
            var dec = convertView.FindViewById<ImageView>(Resource.Id.OrderListItemDecrementItem);
            var delete = convertView.FindViewById<ImageView>(Resource.Id.OrderListItemDeleteItem);

            name.SetTextColor(Android.Graphics.Color.Black);
            details.SetTextColor(Android.Graphics.Color.Black);
            price.SetTextColor(Android.Graphics.Color.Black);
            amount.SetTextColor(Android.Graphics.Color.Black);

            name.Text = item.Name;
            details.Text = String.Join(",", item.Attributes);
            price.Text = Math.Round((decimal)(item.Price * item.Count),2).ToString() + "�";
            amount.Text = item.Count.ToString();

            inc.Click += (o, e) =>
            {
                if (_context.GetController().IncrementPartialOrder(item.ID))
                {
                    price.Text = Math.Round((decimal)(item.Price * item.Count), 2).ToString() + "�";
                    amount.Text = item.Count.ToString();
                }
            };
            dec.Click += (o, e) =>
            {
                if (_context.GetController().DecrementPartialOrder(item.ID))
                {
                    price.Text = Math.Round((decimal)(item.Price * item.Count), 2).ToString() + "�";
                    amount.Text = item.Count.ToString();
                }
                else
                    _context.GetController().PopulateListView();
            };
            delete.Click += (o, e) =>
            {
                _context.GetController().RemoveItemFromOrder(item.ID);
                _context.GetController().PopulateListView();

            };
            return convertView;
        }
    }
}