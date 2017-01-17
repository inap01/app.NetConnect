using System;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MonoNetConnect.Controller;
using Android.Support.V7.App;
using Fragment = Android.App.Fragment;
using System.Collections.Generic;

namespace NetConnect.Activities
{
    [Activity(Label = "NavigationActivity")]
    public class NavigationActivity : BaseActivity, NavigationAdapter.OnItemClickListener, INavigationController
    {

        
        
        private new String Title;

        public new void ItemClicked()
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
    }

   
}