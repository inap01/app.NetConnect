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

        }

        public void UpdateContentList(Data<Tournament> tournaments)
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
    }
}