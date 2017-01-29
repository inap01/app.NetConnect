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
namespace NetConnect.Activities
{
    [Activity(Label = "ContactActivity", MainLauncher = true)]
    public class ContactActivity : BaseActivity<IContactController, ContactController>, IContactController
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new ContactController(this);
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