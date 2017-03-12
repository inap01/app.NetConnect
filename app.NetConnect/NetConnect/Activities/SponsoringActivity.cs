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
using Square.Picasso;
using Java.IO;
using System.Reflection;

namespace NetConnect.Activities
{
    [Activity(Label = "Sponsoring", MainLauncher = false)]
    public class SponsoringActivity : BaseActivity<ISponsoringController, SponsoringController>, ISponsoringController
    {
        ListView list;
        SponsoringAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.SponsoringLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new SponsoringController(this);
            base.OnCreate(savedInstanceState);
            this.Controller.ListItems();
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
            this.Controller.ListItems();
        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
        public void SetListItems(Data<Sponsor> sponsors)
        {
            if(list == null)
                list = FindViewById<ListView>(Resource.Id.SponsoringListView);
            if(adapter == null)
                adapter = new SponsoringAdapter(this);
            if (list?.Adapter == null)
                list.Adapter = adapter;
            adapter.SetSponsors(sponsors);
            this.RunOnUiThread(() => adapter.NotifyDataSetChanged());
        }

        public class SponsoringAdapter : BaseAdapter<Sponsor>
        {
            Activity context;
            private Data<Sponsor> sponsors;

            public SponsoringAdapter(Activity _context)
                : base()
            {
                this.context = _context;
                sponsors = new Data<Sponsor>();
            }
            public void SetSponsors(Data<Sponsor> spons)
            {
                sponsors.Clear();
                sponsors.AddRange(spons);
            }
            public override Sponsor this[int position]
            {
                get
                {
                    return sponsors[position];
                }
            }
            public override int Count
            {
                get
                {
                    return sponsors.Count;
                }
            }
            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                System.Diagnostics.Debug.WriteLine($"Currently in Method {MethodBase.GetCurrentMethod().Name} drawing image {sponsors[position].Image.Split('/').Last()}");
                convertView = context.LayoutInflater.Inflate(Resource.Layout.SponsoringListViewItem, parent, false);
                convertView.FindViewById<ImageView>(Resource.Id.SponsoringImage1);
                string path = System.String.Join("/", context.ApplicationInfo.DataDir, sponsors.GetImageDirectoryPath(), sponsors[position].Image.Split('/').Last());
                using(File imageFile = new File(path))
                    Picasso.With(context).Load(imageFile).Into(convertView.FindViewById<ImageView>(Resource.Id.SponsoringImage1));
                convertView.FindViewById<ImageView>(Resource.Id.SponsoringImage1).Click += (o, e) =>
                {
                    Intent i = new Intent(Intent.ActionView);
                    i.SetData(Android.Net.Uri.Parse(sponsors[position].Link));
                    context.StartActivity(i);
                };
                return convertView;
            }
        }
    }
}