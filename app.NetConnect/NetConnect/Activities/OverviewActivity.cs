using Android.App;
using Android.Widget;
using Android.OS;
using MonoNetConnect.Controller;
using System;
using NetConnect.Activities;
using MonoNetConnect.Cache;
using Android.Views;

namespace NetConnect
{
    [Activity(Label = "NetConnect", MainLauncher = true, Icon = "@drawable/icon")]
    public class OverviewActivity : BaseActivity<IOverviewController,OverviewController>, IOverviewController
    {
        //private OverviewController _controller;
        //public OverviewController Controller { get { return _controller; } set { this._controller = value; } }
        public void someButton(String s)
        {
            DataContext.ImagePathMap.Add(1, "Catering/Hallo.jpg");
            var x = Toast.MakeText(this,DataContext.ImagePath(this,1),ToastLength.Short);
            x.Show();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            setUpUI();
            SetUpNavigationMenu();
            this.NavController = new NavigationController(this);
            this.Controller = new OverviewController(this);
            SetUpFragmentManager();
            OnClick(FindViewById<Button>(Resource.Id.Button), () =>
            {
                this.Controller.action("ICH BIN ANONYM");
            });
        }

        private void SetUpFragmentManager()
        {
            var frag = OverViewFragment.NewInstance();
            var fm = this.FragmentManager.BeginTransaction();
            fm.Replace(Resource.Id.content_frame, frag);
            fm.Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if(isLoggedIn)
            {
                MenuInflater infl = MenuInflater;
                infl.Inflate(Resource.Menu.LoggedInProfileMenu,menu);
                return true;
            }
            return base.OnCreateOptionsMenu(menu);
        }

        internal class OverViewFragment : DynamicFragment
        {
            public static Fragment NewInstance()
            {
                Fragment fragment = new OverViewFragment();
                Bundle args = new Bundle();
                return fragment;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View rootView = inflater.Inflate(Resource.Layout.Main, container, false);
                return rootView;
            }
        }
    }
}

