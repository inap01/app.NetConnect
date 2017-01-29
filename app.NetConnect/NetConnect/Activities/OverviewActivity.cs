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
    [Activity(Label = "NetConnect", Icon = "@drawable/logo")]
    public class OverviewActivity : BaseActivity<IOverviewController,OverviewController>, IOverviewController
    {
        public void someButton(String s)
        {

        }        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.Main);
            this.NavController = new NavigationController(this);
            this.Controller = new OverviewController(this);
            SetUpMethod();
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
    }
}

