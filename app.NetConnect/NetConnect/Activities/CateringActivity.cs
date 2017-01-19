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
    [Activity(Label = "CateringActivity")]
    public class CateringActivity : BaseActivity<ICateringController, CateringController>, ICateringController
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityNavigationLayout);
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
                return rootView;
            }

            private void populateView()
            {
                for(int i = 0; i< ((int)entries.Length/3);i++)
                {
                    TableRow tr = new TableRow(Activity);
                }
            }
        }
    }
}