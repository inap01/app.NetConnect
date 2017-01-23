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
using Java.Lang;
using MonoNetConnect.Controller;

using Color = Android.Graphics.Color;
using MonoNetConnect.InternalModels;

namespace NetConnect.Activities
{
    [Activity(Label = "Sponsoring")]
    public class SponsoringActivity : BaseActivity<ISponsoringController, SponsoringController>, ISponsoringController 
    {
        ListView list;
        SponsoringAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            setUpUI();
            SetUpNavigationMenu();
            this.NavController = new NavigationController(this);
            this.Controller = new SponsoringController(this);
            SetUpFragmentManager();
            list = FindViewById<ListView>(Resource.Id.SponsoringListView);
            List<Sponsor> l = new List<Sponsor>() { new Sponsor() { Content = "Content", Image = "ImagePath", Link = "LinkText", Name = "NameText" } };
            adapter = new SponsoringAdapter(this, l);
            list.Adapter = adapter;

            //list.SetOnClickListener(new View.IOnClickListener()
            //{

            //});
        }

        protected override void SetUpFragmentManager()
        {
            var Frag = SponsoringFrament.NewInstance();
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

        public class SponsoringAdapter : BaseAdapter<Sponsor>
        {
            Activity context;
            List<Sponsor> sponsoren = new List<Sponsor>();

            public SponsoringAdapter(Activity _context, List<Sponsor> _list)
                : base()
            {
                this.context = _context;
                this.sponsoren = _list;
            }
            public override Sponsor this[int position]
            {
                get
                {
                    return sponsoren[position];
                }
            }

            public override int Count
            {
                get
                {
                    return (int)System.Math.Ceiling(sponsoren.Count / 2.0f);
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = context.LayoutInflater.Inflate(Resource.Layout.SponsoringListViewItem, parent);
                var img1 = view.FindViewById<ImageView>(Resource.Id.SponsoringImage1);
                var img2 = view.FindViewById<ImageView>(Resource.Id.SponsoringImage2);

                img1.SetImageResource(Resource.Drawable.speedlink);
                img2.SetImageResource(Resource.Drawable.speedlink);

                return view;
            }
        }

        internal class SponsoringFrament : DynamicFragment
        {
            public static Fragment NewInstance()
            {
                Fragment fragment = new SponsoringFrament();
                Bundle args = new Bundle();
                return fragment;
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View rootView = inflater.Inflate(Resource.Layout.SponsoringLayout, container, false);
                rootView.SetBackgroundColor(Color.Transparent);
                var x = rootView.FindViewById<LinearLayout>(Resource.Id.SponsoringRoot);
                populateView();
                return rootView;
            }

            private void populateView()
            {

            }
        }
        
        
    }
}