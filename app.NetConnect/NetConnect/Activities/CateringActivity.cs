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

            // Create your application here
        }
        internal class CateringFrament : DynamicFragment
        {
            public static Fragment NewInstance()
            {
                Fragment fragment = new CateringFrament();
                Bundle args = new Bundle();
                return fragment;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View rootView = inflater.Inflate(Resource.Layout.CateringLayout, container, false);
                return rootView;
            }
        }
    }
}