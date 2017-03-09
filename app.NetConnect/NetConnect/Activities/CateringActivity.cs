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
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;

namespace NetConnect.Activities
{
    [Activity(Label = "CateringActivity", MainLauncher = false)]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {
        
        GridView grid;
        CateringAdapter adapter;
        CateringOrderAdapter OrderAdapter;
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
            ActionBar.Title = "Catering";
            this.TopRightIconAction = () =>
            {
                if (this.Controller.IsLoggedIn())
                    StartActivity(typeof(OrderActivity));
                else
                    this.StartActivityLogin("Order");

            };
        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
        public void ShowOrderDialog(Product product)
        {
            View v = null;
            if (orderDialog == null)
            {
                v = LayoutInflater.Inflate(Resource.Layout.CateringOrderFragmentDialog, root, false);
                orderDialog = v.FindViewById<RelativeLayout>(Resource.Id.CateringOrderFragmentRelativeLayout);
                root.AddView(v);
            }
            FrameLayout innerRoot = v?.FindViewById<FrameLayout>(Resource.Id.CateringOrderRoot);
            Button cancel = orderDialog.FindViewById<Button>(Resource.Id.CateringCancelButton);
            Button ok = orderDialog.FindViewById<Button>(Resource.Id.CateringApproveButton);
            AttributeList = orderDialog.FindViewById<ListView>(Resource.Id.CateringFragmentListView);
            TextView tvName = orderDialog.FindViewById<TextView>(Resource.Id.OrderFragmentProductName);
            TextView tvPrice = orderDialog.FindViewById<TextView>(Resource.Id.OrderFragmentProductPrice);
            TextView tvDesc = orderDialog.FindViewById<TextView>(Resource.Id.OrderFragmentProductDesc);            
            ok.Text = "Ok!";
            cancel.Text = "Abbrechen";
            tvName.Text = product.Name;
            tvPrice.Text = product.Price.ToString() + "€";
            tvDesc.Text = product.Description;
            SetUpClickTouchEvents(product, orderDialog, cancel, ok, innerRoot);
            OrderAdapter = new CateringOrderAdapter(this, product.SingleChoice, product.Attributes);
            AttributeList.Adapter = OrderAdapter;

            orderDialogActive = true;
            adapter.OrderOpen = true; 
            
        }

        private void SetUpClickTouchEvents(Product product, View orderDialog, Button cancel, Button ok, FrameLayout innerRoot)
        {
            CloseOrderDialog = () =>
            {
                innerRoot.Visibility = ViewStates.Gone;
                root.RemoveView(innerRoot);
                this.orderDialog = null;
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
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater infl = MenuInflater;
            infl.Inflate(Resource.Menu.LoggedInProfileMenu, menu);        
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.GetItem(0).SetIcon(Resource.Drawable.warenkorb);
            return base.OnPrepareOptionsMenu(menu);
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

    public class CateringOrderAdapter : BaseAdapter<string>
    {
        private List<string> _attributes;
        private Activity _context;
        private List<string> _setAttributes = new List<string>();
        private bool _singleChoice;
        CheckBox _lastSetCheckBox;
        public CateringOrderAdapter(Activity context, bool singleChoice, List<string> values)
        {
            _singleChoice = singleChoice;
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
            return _setAttributes;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this[position];
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.CateringOrderAttributeListItem, parent, false);
            TextView tv = convertView.FindViewById<TextView>(Resource.Id.CateringOrderListAttributeName);
            tv.Text = item;
            CheckBox cb = convertView.FindViewById<CheckBox>(Resource.Id.checkbox);
            
            cb.CheckedChange += (o, e) =>
            {
                if (_singleChoice)
                {
                    if (_lastSetCheckBox != null)
                    {
                        if(cb.GetHashCode() != _lastSetCheckBox.GetHashCode())
                        _lastSetCheckBox.Checked = false;
                    }
                    _lastSetCheckBox = cb;
                }
                if (e.IsChecked)
                    _setAttributes.Add(item);
                else
                    _setAttributes.Remove(item);
            };
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