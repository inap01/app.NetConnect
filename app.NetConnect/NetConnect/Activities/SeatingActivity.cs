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
using MonoNetConnect.InternalModels;
using static Android.Views.ViewGroup;
using Android.Support.Percent;

namespace NetConnect.Activities
{
    [Activity(Label = "SeatingActivity", MainLauncher = false)]
    public class SeatingActivity : BaseActivity<ISeatingController, SeatingController>, ISeatingController
    {
        float scale = 0;
        public override void update()
        {
            Controller.SetSeatingOrder();
        }
        LinearLayout col1;
        LinearLayout col2;
        LinearLayout col3;
        LinearLayout col4;
        LinearLayout col5;
        private void InitUIProperties()
        {
            col1 = FindViewById<LinearLayout>(Resource.Id.SeatingCol1);
            col2 = FindViewById<LinearLayout>(Resource.Id.SeatingCol2);
            col3 = FindViewById<LinearLayout>(Resource.Id.SeatingCol3);
            col4 = FindViewById<LinearLayout>(Resource.Id.SeatingCol4);
            col5 = FindViewById<LinearLayout>(Resource.Id.SeatingCol5);
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            scale = ApplicationContext.Resources.DisplayMetrics.Density;

            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.SeatingLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new SeatingController(this);
            base.OnCreate(savedInstanceState);
            InitUIProperties();
            PercentRelativeLayout root = FindViewById<PercentRelativeLayout>(Resource.Id.SeatingRoot);
            var obs = root.ViewTreeObserver;
            Controller.SetSeatingOrder();
        }
        private int Pixel(int dp) =>  (int) (dp* scale +0.5f);

        private void SetSeatingBackgroundColor(TextView tv, Seat s)
        {
            switch (s.Status)
            {
                case 0:
                    tv.SetBackgroundColor(Android.Graphics.Color.Rgb(67, 172, 106));
                    tv.SetTextColor(Android.Graphics.Color.White);
                    break;
                case 1:
                    tv.SetBackgroundColor(Android.Graphics.Color.Rgb(233, 144, 2));
                    tv.SetTextColor(Android.Graphics.Color.White);
                    break;
                case 2:
                    tv.SetBackgroundColor(Android.Graphics.Color.Rgb(240, 65, 36));
                    tv.SetTextColor(Android.Graphics.Color.White);
                    break;
                case 99:
                    tv.SetBackgroundColor(Android.Graphics.Color.Rgb(211, 211, 211));
                    tv.SetTextColor(Android.Graphics.Color.Black);
                    break;
            }
        }
        public void SetSeatingOrder(Data<Seat> seats)
        {
            col1.RemoveAllViews();
            col2.RemoveAllViews();
            col3.RemoveAllViews();
            col4.RemoveAllViews();
            col5.RemoveAllViews();
            int tvHeight = (col1.Height / 16) - Pixel(4);
            int tvWidth = (int)(col1.Width * 0.55);
            for (int i = 0;i<seats.Count;i++)
            {
                if(i<16)
                {
                    TextView tv = new TextView(this);
                    tv.Text = $"{i+1}";
                    SetSeatingBackgroundColor(tv, seats[i]);
                    LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(tvWidth,tvHeight);
                    ll.SetMargins(0, Pixel(4), Pixel(4), 0);
                    tv.Gravity = GravityFlags.Center;
                    tv.LayoutParameters = ll;
                    col2.AddView(tv);
                }
                if (i > 15 && i < 32)
                {
                    TextView tv = new TextView(this);
                    tv.Text = $"{i + 1}";
                    SetSeatingBackgroundColor(tv, seats[i]);
                    LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(tvWidth, tvHeight);
                    ll.SetMargins(Pixel(4), Pixel(4), 0, 0);
                    tv.Gravity = GravityFlags.Center;
                    tv.LayoutParameters = ll;
                    col3.AddView(tv);
                }
                if (i > 31 && i < 46)
                {
                    
                    TextView tv = new TextView(this);
                    tv.Text = $"{i + 1}";
                    SetSeatingBackgroundColor(tv, seats[i]);
                    LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(tvWidth, tvHeight);
                    if (i == 32)
                        ll.SetMargins(0, Pixel(12) + 2*tvHeight, Pixel(4), 0);
                    else
                        ll.SetMargins(0, Pixel(4), Pixel(4), 0);
                    tv.Gravity = GravityFlags.Center;
                    tv.LayoutParameters = ll;
                    col4.AddView(tv);
                }
                if (i > 45 && i < 60)
                {
                    TextView tv = new TextView(this);
                    tv.Text = $"{i + 1}";
                    SetSeatingBackgroundColor(tv, seats[i]);
                    LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(tvWidth, tvHeight);
                    if (i == 46)
                        ll.SetMargins(0, Pixel(12) + 2 * tvHeight, Pixel(4), 0);
                    else
                        ll.SetMargins(0, Pixel(4), Pixel(4), 0);
                    tv.Gravity = GravityFlags.Center;
                    tv.LayoutParameters = ll;
                    col5.AddView(tv);
                }
                if (i > 59 && i < 70)
                {
                    TextView tv = new TextView(this);
                    tv.Text = $"{i + 1}";
                    SetSeatingBackgroundColor(tv, seats[i]);
                    LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(tvWidth, tvHeight);
                    ll.SetMargins(Pixel(10), Pixel(4), 0, 0);
                    tv.Gravity = GravityFlags.Center;
                    tv.LayoutParameters = ll;
                    col1.AddView(tv);
                }
            }
        }

        public override void SetActivityTitle()
        {
            ActionBar.Title = "Sitzplan";
        }
    }
}