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
    [Activity(Label = "ContactActivity")]
    public class ContactActivity : BaseActivity<IContactController, ContactController>, IContactController
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActivityNavigationLayout);
            setUpUI();
            SetUpNavigationMenu();
            this.NavController = new NavigationController(this);
            this.Controller = new ContactController(this);
            SetUpFragmentManager();
        }

        protected override void SetUpFragmentManager()
        {
            var Frag = ContactFrament.NewInstance();
            var fm = this.FragmentManager.BeginTransaction();
            fm.Replace(Resource.Id.content_frame, Frag);
            fm.Commit();
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

        internal class ContactFrament : DynamicFragment
        {
            private static String[] entries;
            public static Fragment NewInstance()
            {
                Fragment fragment = new ContactFrament();
                Bundle args = new Bundle();
                return fragment;
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                entries = new String[12];
                for (int i = 0; i < entries.Length; i++)
                    entries[i] = $"Eintrag Nr{i}";
                View rootView = inflater.Inflate(Resource.Layout.ContactLayout, container, false);
                rootView.SetBackgroundColor(Color.Transparent);
                var x = rootView.FindViewById<LinearLayout>(Resource.Id.ContactRoot);
                populateView();
                return rootView;
            }

            private void populateView()
            {

            }
        }
    }
}