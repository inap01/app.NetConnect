using System;

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

namespace NetConnect
{
    
    public abstract class BaseActivity<T,Z> : FragmentActivity,  NavigationAdapter.OnItemClickListener, INavigationController
        where T : IBaseViewController where Z : BaseViewController<T>
    {

        #region Navigation Properties
        protected DrawerLayout mDrawerLayout;
        protected RecyclerView mDrawerList;
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

        protected String[] Entries { get; set; } = { "Eintrag1", "Eintrag2", "Eintrag3", "Eintrag4" };

        #endregion

        public virtual Boolean OnClick(View view, Action func)
        {
            if (view != null)
            {
                view.Click += (o, e) => { func(); };
                return true;
            }
            else
                return false;
        }       
        public void ListItemClicked(int id)
        {
            string key = Entries[id];
            Type t = nameMap[key];
            StartActivity(typeof(OverviewActivity));
            Intent i = new Intent(this, t);
            StartActivity(i);
        }

        #region Navigation Methods and Setup
        protected void SetUpNavigationMenu()
        {
            mDrawerList = new RecyclerView(this);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mDrawerList = FindViewById<RecyclerView>(Resource.Id.left_drawer);
            Title = NavTitle;
            mDrawerList.HasFixedSize = true;
            mDrawerList.SetLayoutManager(new LinearLayoutManager(this));

            mDrawerList.SetAdapter(new NavigationAdapter(Entries, this));
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);
            mDrawerToggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, mDrawerLayout,
                Resource.String.drawerOpen,
                Resource.String.drawerClosed);
            mDrawerLayout.AddDrawerListener(mDrawerToggle);
        }
        protected void setUpBeforeNavigation(String[] entries, String navigationTitle)
        {
            this.Entries = entries;
            this.NavTitle = navigationTitle;

        }
        protected void setUpUI()
        {
            nameMap = new Dictionary<string, Type>();
            Type t = typeof(OverviewActivity);
            nameMap.Add(Entries[0], t);
            nameMap.Add(Entries[1], t);
            nameMap.Add(Entries[2], t);
            nameMap.Add(Entries[3], t);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    NavBarOpenClose();
                    break;
                case Resource.Menu.LoggedInProfileMenu:

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

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        void NavigationAdapter.OnItemClickListener.OnClick(View view, int position)
        {
            this.NavController.ListItemClicked(position);
        }

        #endregion


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
        internal abstract class DynamicFragment : Fragment
        {
            public DynamicFragment()
            {

            }
            public abstract override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState);            
        }
    }

    
    public class NavigationAdapter : RecyclerView.Adapter
    {
        private static String[] entries;
        private OnItemClickListener mListener;

        public override int ItemCount
        {
            get
            {
                return entries.Length;
            }
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public readonly TextView textView;
            public ViewHolder(TextView v) : base(v)
            {
                textView = v;
            }
        }
        public interface OnItemClickListener
        {
            void OnClick(View view, int position);
        }
        public NavigationAdapter(String[] entries, OnItemClickListener listener)
        {
            NavigationAdapter.entries = entries;
            this.mListener = listener;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder rawHolder, int position)
        {
            var holder = (ViewHolder)rawHolder;
            holder.textView.Text = entries[position];
            holder.textView.Click += (o, e) => {
                mListener.OnClick((View)o, position);
            };
        }
        public static String GetEntry(int id)
        {
            return entries[id];
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var vi = LayoutInflater.From(parent.Context);
            var v = vi.Inflate(Resource.Layout.DrawerListItem, parent, false);
            var tv = v.FindViewById<TextView>(Resource.Id.TexField);
            return new ViewHolder(tv);
        }
    }
}