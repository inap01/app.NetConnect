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
    [Activity(Label = "ContactActivity", MainLauncher = false)]
    public class ContactActivity : BaseActivity<IContactController, ContactController>, IContactController
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.ContactLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new ContactController(this);
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (this.Controller.IsLoggedIn())
            {
                MenuInflater infl = MenuInflater;
                infl.Inflate(Resource.Menu.LoggedInProfileMenu, menu);
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