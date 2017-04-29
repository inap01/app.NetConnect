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
    [Activity(Label = "NetConnect", MainLauncher = true, NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class LoginActivity : BaseActivity<ILoginController, LoginController>, ILoginController
    {
        Action PostLoginAction = null;
        public override void update()
        {
            CheckLoginStatus();
        }

        private void CheckLoginStatus()
        {
            if (this.Controller.IsLoggedIn())
            {
                StartActivity(typeof(TournamentActivity));
                Finish();
            }
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
            CheckLoginStatus();
        }

        private void WireUpClicktEvents(string intent)
        {
            if (intent != null)
            {
                PostLoginAction = () =>
                {
                    ListItemClicked(nameMap[intent]);
                };
            }
            TopRightIconAction = () =>
            {
                this.StartActivity(typeof(SponsoringActivity));
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
            MenuInflater infl = MenuInflater;
            infl.Inflate(Resource.Menu.LoggedInProfileMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.GetItem(0).SetIcon(null);
            menu.GetItem(0).SetTitle("Skip");
            return base.OnPrepareOptionsMenu(menu);
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
                this.NavController.ListItemClicked(typeof(TournamentActivity));
            else
                PostLoginAction();
        }

        public void LoginFailed()
        {
            var t = Toast.MakeText(this, "Falsche Login Daten!", ToastLength.Long);
            t.Show();
            FindViewById<EditText>(Resource.Id.LoginPassword).Text = "";
        }        
    }
}