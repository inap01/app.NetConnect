using Android.App;
using Android.Widget;
using Android.OS;
using MonoNetConnect.Controller;
using System;
using MonoNetConnect.Cache;
using Android.Views;
using Android.Content;
using Android.Util;

namespace NetConnect
{
    [Activity(Label = "NetConnect", MainLauncher = true, Icon = "@drawable/icon")]
    public class OverviewActivity : BaseActivity<IOverviewController,OverviewController>, IOverviewController
    {
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
            this.Entries[0] = "Catering";
        }

        private void SetUpCateringUI()
        {
            OverViewFragment.InitClick<Button>(Resource.Id.Button, () => { this.Controller.action("TestString"); });
        }
        protected override void SetUpFragmentManager()
        {
            var Frag = OverViewFragment.NewInstance(() => { SetUpCateringUI(); });
            var fm = this.FragmentManager.BeginTransaction();
            fm.Replace(Resource.Id.content_frame, Frag);
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
            public static Action ButtonInitialisationClick;

            private static View root;
            public static Fragment NewInstance(Action func)
            {
                Fragment fragment = new OverViewFragment();
                OverViewFragment.ButtonInitialisationClick = func;
                Bundle args = new Bundle();        
                return fragment;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                root = inflater.Inflate(Resource.Layout.Main, container, false);
                var x = root.FindViewById<Button>(Resource.Id.Button);
                ButtonInitialisationClick?.Invoke();
                return root;
            }
            public static void InitClick<T>(int id, Action func)
            {
                root.FindViewById<Button>(id).Click += (o, e) => { func(); };                    
            }
            
        }
    }
}

