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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.CateringLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new CateringController(this);
            SetUpMethod();
            grid = FindViewById<GridView>(Resource.Id.CateringGridLayout);
            root = FindViewById<RelativeLayout>(Resource.Id.CateringRoot);
            Controller.setUpUI();
        }

        public void ShowOrderDialog(Product product)
        {
            View v = LayoutInflater.Inflate(Resource.Layout.CateringOrderFragmentDialog, root, false);
            Button cancel = FindViewById<Button>(Resource.Id.CateringCancelButton);
            Button ok = FindViewById<Button>(Resource.Id.CateringApproveButton);
            cancel.SetBackgroundResource(Resource.Drawable.FadingGradient);
            ok.SetBackgroundResource(Resource.Drawable.FadingGradient);

            SetUpClickTouchEvents(product, v, cancel, ok);
            root.AddView(v);
        }

        private void SetUpClickTouchEvents(Product product, View v, Button cancel, Button ok)
        {
            cancel.Click += (o, e) =>
            {
                root.RemoveView(v);
            };
            ok.Click += async (o, e) =>
            {
                var prod = product.DeepClone();
                prod.Attributes = new List<string>();
                var result = await Controller.OrderProduct(prod);
                ShowToast(result, false, "Placeholder");
                root.RemoveView(v);
            };
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

        public override void update()
        {
            RunOnUiThread(() =>
            {
                this.Controller.setUpUI();
            });
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


    public class CateringAdapter : BaseAdapter<Product>
    {
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
            Picasso.With(_context).Load(Resource.Drawable.logo).Into(iv);
            convertView.Click +=(o,e) =>  OnProductClickCallback(position);
            return convertView;
        }
    }
}