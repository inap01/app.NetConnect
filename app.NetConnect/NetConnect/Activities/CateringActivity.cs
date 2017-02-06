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
using Color = Android.Graphics.Color;
using Android;
using MonoNetConnect.InternalModels;
using Java.Lang;
using NetConnect.CustomViews;
using Square.Picasso;

namespace NetConnect.Activities
{
    [Activity(Label = "CateringActivity", MainLauncher =true)]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {
        GridView grid;
        CateringAdapter adapter;
        ViewGroup root;
        RelativeLayout orderDialog;
        System.Boolean orderDialogActive;
        Action CloseOrderDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.CateringLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new CateringController(this);
            grid = FindViewById<GridView>(Resource.Id.CateringGridLayout);
            root = FindViewById<RelativeLayout>(Resource.Id.CateringRoot);
            base.OnCreate(savedInstanceState);
            Controller.setUpUI();
        }

        public void ShowOrderDialog(Product product)
        {
            View v = LayoutInflater.Inflate(Resource.Layout.CateringOrderFragmentDialog, root, false);
            FrameLayout innerRoot = v.FindViewById<FrameLayout>(Resource.Id.CateringOrderRoot);
            orderDialog = v.FindViewById<RelativeLayout>(Resource.Id.CateringOrderFragmentRelativeLayout);
            Button cancel = v.FindViewById<Button>(Resource.Id.CateringCancelButton);
            Button ok = v.FindViewById<Button>(Resource.Id.CateringApproveButton);
            ListView list = v.FindViewById<ListView>(Resource.Id.CateringFragmentListView);
            OrderAdapter adap = new OrderAdapter(this,product.Attributes);

            list.Adapter = adap;
            SetUpClickTouchEvents(product, v, cancel, ok, innerRoot);
            root.AddView(v);
            orderDialogActive = true;
            adapter.OrderOpen = true;
        }

        private void SetUpClickTouchEvents(Product product, View v, Button cancel, Button ok, FrameLayout innerRoot)
        {
            CloseOrderDialog = () =>
            {
                root.RemoveView(v);
                orderDialogActive = false;
                adapter.OrderOpen = false;
            };
            cancel.Click += (o, e) =>
            {
                CloseOrderDialog();
            };
            ok.Click += async (o, e) =>
            {
                var prod = product.DeepClone();
                prod.Attributes = new List<string>();
                var result = await Controller.OrderProduct(prod);
                ShowToast(result, false, "Placeholder");
                CloseOrderDialog();
            };
        }
        public override void OnBackPressed()
        {
            if (orderDialogActive)
                CloseOrderDialog();
            else
                base.OnBackPressed();
        }
        private void ShowToast(bool success, bool shortDur, string Message)
        {
            string NamePlaceholder = "";
            var toast = Toast.MakeText(this, Message , shortDur == true ? ToastLength.Short : ToastLength.Long);
            toast.Show();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (isLoggedIn)
            {
                MenuInflater infl = MenuInflater;
                infl.Inflate(Resource.Menu.LoggedInProfileMenu, menu);
                return true;
            }
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if(orderDialogActive)
            {
                int[] pos = new int[2];
                orderDialog.GetLocationOnScreen(pos);
                int x = pos[0];
                int y = pos[1];
                int owidth = orderDialog.Width;
                int oheight = orderDialog.Height;
                if( ev.GetY() < y ||
                    ev.GetX() > (x+owidth) ||
                    (ev.GetY() > (y+oheight)) ||
                    (ev.GetX() < x))
                {
                    System.Diagnostics.Debug.WriteLine("Outside");
                    return false;
                }
            }
            return base.DispatchTouchEvent(ev);
        }
        public override void update()
        {
            this.Controller.setUpUI();
        }

        public void PopulateGridLayout(Data<Product> products)
        {
            if (grid == null)
                grid = FindViewById<GridView>(Resource.Id.CateringGridLayout);
            if (adapter == null)
            {
                adapter = new CateringAdapter(this, products, (Int32 a) => Controller.OrderDialog(a));
                grid.Adapter = adapter;
            }
            adapter.NotifyDataSetChanged();
        }
    }

    public class OrderAdapter : BaseAdapter<string>
    {
        private List<string> _attributes;
        private Activity _context;
        public OrderAdapter(Activity context, List<string> values)
        {
            _context = context;
            _attributes = values;
        }
        public override string this[int position]
        {
            get
            {
                return _attributes[position];
            }
        }

        public override int Count
        {
            get
            {
                return _attributes.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.CateringOrderAttributeListItem, parent, false);
            TextView tv = convertView.FindViewById<TextView>(Resource.Id.CateringOrderListAttributeName);       
            return convertView;
        }
    }

    public class CateringAdapter : BaseAdapter<Product>
    {
        public bool OrderOpen { get; set; }
        Activity _context;
        Action<Int32> OnProductClickCallback;
        private Data<Product> _products;
        string path (int pos) => System.String.Join("/", _context.ApplicationInfo.DataDir, _products.GetImageDirectoryPath(), _products[pos].ImageName.Split('/').Last());
        public CateringAdapter(Activity context, Data<Product> prod, Action<Int32> _delegate)
        {
            _context = context;
            _products = prod;
            OnProductClickCallback = _delegate;
        }
        public override int Count
        {
            get
            {
                return _products.Count;
            }
        }

        public override Product this[int position]
        {
            get
            {
                return _products[position];
            }
        }
        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.CateringListItemLayout,parent,false);
            ImageViewScaling iv = convertView.FindViewById<ImageViewScaling>(Resource.Id.CateringImage);
            TextView tv = convertView.FindViewById<TextView>(Resource.Id.CateringListItemProductName);
            tv.SetText(_products[position].Name, TextView.BufferType.Normal);
            tv.SetBackgroundResource(Resource.Drawable.FadingGradient);
            Picasso.With(_context).Load(Resource.Drawable.logo).Into(iv);
            convertView.Click += (o, e) => { if(!OrderOpen) OnProductClickCallback(position); };
            return convertView;
        }
    }
}