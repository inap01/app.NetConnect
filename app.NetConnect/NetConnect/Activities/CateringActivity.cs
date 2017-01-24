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
    [Activity(Label = "CateringActivity")]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {
        FrameLayout content;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            content = FindViewById<FrameLayout>(Resource.Id.content_frame);
            setUpUI();
            SetUpNavigationMenu();
            this.NavController = new NavigationController(this);
            this.Controller = new CateringController(this);
            SetUpFragmentManager();
        }
        protected override void SetUpFragmentManager()
        {
            var Frag = CateringFrament.NewInstance();
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
        public override void SetContentView(int layoutResID)
        {
            if(content != null)
            {
                LayoutInflater inf = (LayoutInflater)this.GetSystemService(LayoutInflaterService);
                ViewGroup.LayoutParams lp = new ViewGroup.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent);
                View _content = inf.Inflate(layoutResID, content, false);
                content.AddView(_content);
            }

        }
        public override void SetContentView(View view)
        {
            if(content != null)
            {

            }
        }

        public override void SetContentView(View view, ViewGroup.LayoutParams @params)
        {
            base.SetContentView(view, @params);
        }


        internal class CateringFrament : DynamicFragment
        {
            String[] entries;
            public static Fragment NewInstance()
            {
                Fragment fragment = new CateringFrament();
                Bundle args = new Bundle();
                return fragment;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                entries = new String[12];
                for (int i = 0; i < entries.Length; i++)
                    entries[i] = $"Eintrag Nr{i}";
                View rootView = inflater.Inflate(Resource.Layout.CateringLayout, container, false);
                rootView.SetBackgroundColor(Color.Transparent);
                var x = rootView.FindViewById<LinearLayout>(Resource.Id.CateringRoot);
                populateView(x);
                return rootView;
            }

            private void populateView(LinearLayout root)
            {
                LinearLayout ll = root;
                TableLayout tl = root.FindViewById<TableLayout>(Resource.Id.tableLayout1);
                //tl.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

                    TableRow tr = new TableRow(Activity);
                    var x = new TableRow.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                    tr.LayoutParameters = x;

                        ImageView iv = new ImageView(Activity);
                        var y = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        iv.LayoutParameters = y;
                tr.AddView(iv);
                tl.AddView(tr,1);
                //for (int i = 0; i< 1;i++)
                //{
                    
                //    for(int j = 0;j<1;j++)
                //    {
                        
                //        if (j == 0)
                //        {
                //            iv.SetImageResource(Resource.Drawable.icon);
                //            iv.SetScaleType(ImageView.ScaleType.FitXy);
                //        }                    
                //        //if(j == 1)
                //        //    iv.SetBackgroundColor(Android.Graphics.Color.Red);
                //        //if(j==2)
                //        //    iv.SetBackgroundColor(Android.Graphics.Color.LawnGreen);
                //        //tr.AddView(iv);
                //    }
                //}
            }
        }
    }
}