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

namespace NetConnect.Activities
{
    [Activity(Label = "NetConnect", MainLauncher = true, NoHistory = true)]
    class LoginActivity : BaseActivity<ILoginController, LoginController>, ILoginController
    {
        Action PostLoginAction = null;
        public override void update()
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            var intent = Intent.GetStringExtra("ActivityType");

            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.LoginLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new LoginController(this);
            WireUpClicktEvents(intent);
            base.OnCreate(savedInstanceState);
        }

        private void WireUpClicktEvents(string intent)
        {
            if (intent != null)
            {
                PostLoginAction = () =>
                {
                    ListItemClicked(nameMap[intent]);
                };
                FindViewById<TextView>(Resource.Id.LoginSkipLogin).Visibility = ViewStates.Gone;
            }
            FindViewById<TextView>(Resource.Id.LoginSkipLogin).Click += (o, e) =>
            {
                this.StartActivity(typeof(OverviewActivity));
            };
            FindViewById<Button>(Resource.Id.LoginButton).Click += (o, e) =>
            {
                this.Login();
            };
            if(intent == null)
            FindViewById<EditText>(Resource.Id.LoginPassword).EditorAction += (o, e) =>
            {
                this.Login();
            };
        }

        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return false;
        }
        public void Login()
        {
            this.Controller.Login(FindViewById<EditText>(Resource.Id.LoginUsername).Text,FindViewById<EditText>(Resource.Id.LoginPassword).Text);
        }

        public void LoginSucessfull()
        {
            var t = Toast.MakeText(this, "Login erfolgreich!", ToastLength.Long);
            t.Show();
            if (PostLoginAction == null)
                this.NavController.ListItemClicked(typeof(OverviewActivity));
            else
                PostLoginAction();
        }

        public void LoginFailed()
        {

        }        
    }
}