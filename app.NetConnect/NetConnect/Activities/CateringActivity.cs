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
using Java.IO;

namespace NetConnect.Activities
{
    [Activity(Label = "CateringActivity", MainLauncher =true)]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {
        GridView grid;
        CateringAdapter adapter;
        OrderAdapter OrderAdapter;
        ListView AttributeList;
        ViewGroup root;
        RelativeLayout orderDialog;
        bool orderd = false;
        bool orderDialogActive;
        Action CloseOrderDialog;
        Action ConfirmOrder;
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
            var TopOrderBag = FindViewById<View>(Resource.Id.openProfile);
            this.TopRightIconAction = () =>
            {
                StartActivity(typeof(OrderActivity));
            };
        }

        public void ShowOrderDialog(Product product)
        {
            View v = LayoutInflater.Inflate(Resource.Layout.CateringOrderFragmentDialog, root, false);
            FrameLayout innerRoot = v.FindViewById<FrameLayout>(Resource.Id.CateringOrderRoot);
            orderDialog = v.FindViewById<RelativeLayout>(Resource.Id.CateringOrderFragmentRelativeLayout);
            Button cancel = v.FindViewById<Button>(Resource.Id.CateringCancelButton);
            Button ok = v.FindViewById<Button>(Resource.Id.CateringApproveButton);
            AttributeList = v.FindViewById<ListView>(Resource.Id.CateringFragmentListView);
            TextView tvName = v.FindViewById<TextView>(Resource.Id.OrderFragmentProductName);
            TextView tvPrice = v.FindViewById<TextView>(Resource.Id.OrderFragmentProductPrice);
            TextView tvDesc = v.FindViewById<TextView>(Resource.Id.OrderFragmentProductDesc);
            tvName.Text = product.Name;
            tvPrice.Text = product.Price.ToString();
            tvDesc.Text = product.Description;            
            SetUpClickTouchEvents(product, v, cancel, ok, innerRoot);
            OrderAdapter = new OrderAdapter(this, product.Attributes);
            AttributeList.Adapter = OrderAdapter;
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
            ok.Click += (o,e) =>
            {
                if(!orderd)
                {
                    orderd = true;
                    var prod = product.DeepClone();
                    prod.Attributes = OrderAdapter.GetAttributes(AttributeList);
                    Controller.AddProductToOrder(prod);
                    CloseOrderDialog();
                    orderd = false;
                }
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
            RunOnUiThread(() =>
            {
                adapter.SetItems(products);
                adapter.NotifyDataSetChanged();
            });
        }
    }

    public class OrderAdapter : BaseAdapter<string>
    {
        private List<string> _attributes;
        public List<string> _setAttributes;
        private Activity _context;
        public OrderAdapter(Activity context, List<string> values)
        {
            _context = context;
            _attributes = values;
            _setAttributes = new List<string>();
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
        public List<string> GetAttributes(ViewGroup root)
        {
            Attributes(root);
            return _setAttributes;
        }
        private void Attributes(ViewGroup parent)
        {            
            for(int i = 0; i < parent.ChildCount;i++)
            {
                if(parent.GetChildAt(i) is ViewGroup)
                    if (((ViewGroup)parent.GetChildAt(i)).ChildCount > 0)
                        Attributes((ViewGroup)parent.GetChildAt(i));             
                if(parent.GetChildAt(i) is CheckBox)
                {
                    if(((CheckBox)(parent.GetChildAt(i))).Checked)
                        _setAttributes.Add(this[i]);
                }
            }
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
        public void SetItems(Data<Product> products)
        {
            _products = products;
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
            string path = System.String.Join("/", _context.ApplicationInfo.DataDir, _products.GetImageDirectoryPath(), _products[position].ImageName.Split('/').Last());
            Picasso.With(_context).Load(new File(path)).Into(iv);
            convertView.Click += (o, e) =>
            {
                if (!OrderOpen) OnProductClickCallback(position);
            };
            return convertView;
        }
    }
}