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

namespace NetConnect.Activities
{
    [Activity(Label = "TournamentActivity")]
    public class TournamentActivity : BaseActivity<ITournamentController, TournamentController>, ITournamentController 
    {
        public override void update()
        {
            this.Controller.setUpUi();
        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = this.GetType().Name.Replace("Activity", "");
        }
        public void UpdateContentList(Data<Tournament> tournaments)
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ActivityNavigationLayout);
            SetInnerLayout(Resource.Layout.TournamentLayout);
            this.NavController = new NavigationController(this);
            this.Controller = new TournamentController(this);
            base.OnCreate(savedInstanceState);
            this.Controller.setUpUi();
        }

        public void SetUpUi(string vol)
        {
            FindViewById<TextView>(Resource.Id.TournamentTitleVol).Text = GetString(Resource.String.netcon_Tournament_TitleText).Replace("#", vol);
        }
    }
}