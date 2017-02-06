using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MonoNetConnect.Controller;
using Android.Support.V7.App;

using Fragment = Android.App.Fragment;

using System.Collections.Generic;
using Android.Support.V4.App;
using NetConnect.Activities;
using static Android.App.ActionBar;
using MonoNetConnect.Cache;
using System.Reflection;

namespace NetConnect
{
    public abstract class BaseActivity<T,Z> : FragmentActivity, INavigationController, ISubscriber
        where T : IBaseViewController where Z : BaseViewController<T>
    {
        #region Navigation Properties
        List<TextView> NavEntries { get; set; } = new List<TextView>();
        protected DrawerLayout mDrawerLayout;
        protected Android.Support.V7.App.ActionBarDrawerToggle mDrawerToggle;
        #endregion
        #region Shared Properties
        protected String NavTitle { get; set; }
        protected Boolean isLoggedIn { get; set; } = true;
        protected Dictionary<string, Type> nameMap;
        private NavigationController _navController;
        protected NavigationController NavController
        {
            get { return _navController; }
            set { _navController = value; }
        }

        private Z _controller;
        protected Z Controller {
            get { return _controller; }

            set { _controller = value; }
        }
        protected List<String> Entries
        {
            get; set;
        } = new List<string>(new String[] { "Home", "Catering", "Sponsoren", "Tournament", "Kontakt" });

        #endregion        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} params");
            base.OnCreate(savedInstanceState);
            SetUpMethod();
            DataContext d = DataContext.GetDataContext();
            d.Attach(this);
        }
        public void ListItemClicked(int id)
        {
            string key = Entries[id];
            Type t = nameMap[key];
            StartActivity(typeof(OverviewActivity));
            Intent i = new Intent(this, t);
            StartActivity(i);
        }
        protected override void OnDestroy()
        {
            DataContext d = DataContext.GetDataContext();
            d.Detach(this);
            base.OnDestroy();
        }
        private void SetUpMethod()
        {
            setUpUI();
            SetUpNavigationMenu();
        }
        #region Navigation Methods and Setup

        private void SetUpNavigationMenu()
        {
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Title = NavTitle;            
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);
            mDrawerToggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, mDrawerLayout,
                Resource.String.drawerOpen,
                Resource.String.drawerClosed);
            mDrawerLayout.AddDrawerListener(mDrawerToggle);
            ListView list = FindViewById<ListView>(Resource.Id.NavbarListView);
            NavAdapter adap = new NavAdapter(this,nameMap);
            list.Adapter = adap;
        }
        protected void SetInnerLayout(int id)
        {
            LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            ViewGroup vg = FindViewById<FrameLayout>(Resource.Id.content_frame);
            var ll = inflater.Inflate(id, vg);
        }
        private void setUpUI()
        {
            populateNameMap();
            SetUpNavClick();
        }
        public Z GetController()
        {
            return this._controller;
        }
        private void populateNameMap()
        {
            nameMap = new Dictionary<string, Type>();
            Type t = typeof(OverviewActivity);
            nameMap.Add(Entries[0], typeof(OverviewActivity));
            nameMap.Add(Entries[1], typeof(CateringActivity));
            nameMap.Add(Entries[2], typeof(SponsoringActivity));
            nameMap.Add(Entries[3], typeof(TournamentActivity));
            nameMap.Add(Entries[4], typeof(ContactActivity));
        }

        protected void SetUpNavClick()
        {
            
        }
        protected void StartActivityWrapper(Type t)
        {
            Intent i = new Intent(this, t);
            StartActivity(i);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    NavBarOpenClose();
                    break;
                case Resource.Id.openProfile:
                    break;
                default:
                    NavBarOpenClose();
                    break;
            }
            return true;
        }

        private void NavBarOpenClose()
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
                mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
            else
                mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }
        protected override void OnResume()
        {
            base.OnResume();
            mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }
        
        public static void InitViewClick(View v, Action func)                
        {
            v.Click += (o, e) => { func(); };
        }
        #endregion
        public override void OnBackPressed()
        {
            mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
            Finish();
        }

        public abstract void update();
        public class NavAdapter : BaseAdapter<string>
        {
            Activity _context;
            protected Dictionary<string, Type> _nameMap;
            public NavAdapter(Activity context, Dictionary<string, Type> nameMap)
            {
                _nameMap = nameMap;
                _context = context;
            }
            public override string this[int position]
            {
                get
                {
                    return _nameMap.Keys.ElementAt(position);
                }
            }

            public override int Count
            {
                get
                {
                    return _nameMap.Count;
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                convertView = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1,parent,false);
                ((TextView)convertView).SetText(this[position], TextView.BufferType.Normal);
                convertView.Click += (o,e) => _context.StartActivity(_nameMap[this[position]]);
                return convertView;
            }
        }
        internal class MyActionBarDrawerToggle : Android.Support.V7.App.ActionBarDrawerToggle
        {
            OverviewActivity owner;

            public MyActionBarDrawerToggle(OverviewActivity activity, DrawerLayout layout, int imgRes, int openRes, int closeRes)
                : base(activity, layout, openRes, closeRes)
            {
                owner = activity;                
            }

            public override void OnDrawerClosed(View drawerView)
            {
                owner.ActionBar.Title = owner.Title;
                owner.InvalidateOptionsMenu();
            }

            public override void OnDrawerOpened(View drawerView)
            {
                owner.ActionBar.Title = owner.Title;
                owner.InvalidateOptionsMenu();
            }
        }
    }
}