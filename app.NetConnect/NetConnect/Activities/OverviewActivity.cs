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
    [Activity(Label = "NetConnect", Icon = "@drawable/logo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class OverviewActivity : BaseActivity<IOverviewController,OverviewController>, IOverviewController
    {
        public void someButton(String s)
        {

        }        
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.Main);
            this.NavController = new NavigationController(this);
            this.Controller = new OverviewController(this);
            base.OnCreate(bundle);
        }        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if(this.Controller.IsLoggedIn())
            {
                MenuInflater infl = MenuInflater;
                infl.Inflate(Resource.Menu.LoggedInProfileMenu,menu);
                return true;
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override void update()
        {

        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
    }
}

