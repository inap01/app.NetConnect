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

namespace NetConnect.Activities
{
    [Activity(Label = "CateringActivity")]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {
        FrameLayout content;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            
            content = FindViewById<FrameLayout>(Resource.Id.content_frame);
            this.NavController = new NavigationController(this);
            this.Controller = new CateringController(this);
            SetUpMethod();
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
    }
}