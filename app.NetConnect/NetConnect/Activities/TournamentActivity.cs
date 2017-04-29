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
    [Activity(Label = "TournamentActivity",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class TournamentActivity : BaseActivity<ITournamentController, TournamentController>, ITournamentController 
    {
        public override void update()
        {
            this.Controller.UpdateReceived();
        }
        public override void SetActivityTitle()
        {
            ActionBar.Title = "Turniere";
        }
        public void UpdateContentList(Tournaments tournaments)
        {
            var culture = new System.Globalization.CultureInfo("de-DE");
            // Bierpong
            FindViewById<View>(Resource.Id.TournamentBierpong).FindViewById<TextView>(Resource.Id.TournamentGameRegistrations).Text = $"{tournaments.Bierpong.NumberParticipants} Anmeldungen(en)";
            FindViewById<View>(Resource.Id.TournamentBierpong).FindViewById<TextView>(Resource.Id.TournamentGameStyle).Text = $"{tournaments.Bierpong.Mode}";
            FindViewById<View>(Resource.Id.TournamentBierpong).FindViewById<TextView>(Resource.Id.TournamentGameTime).Text = $"{culture.DateTimeFormat.GetDayName(tournaments.Bierpong.StartTime.DayOfWeek)} {tournaments.Bierpong.StartTime.Hour}:{tournaments.Bierpong.StartTime.Minute}";
            FindViewById<View>(Resource.Id.TournamentBierpong).FindViewById<TextView>(Resource.Id.TournamentGameTitle).Text = $"{tournaments.Bierpong.Name}";

            //CoD4

            FindViewById<View>(Resource.Id.TournamentCoD4).FindViewById<TextView>(Resource.Id.TournamentGameRegistrations).Text = $"{tournaments.CallOfDuty.NumberParticipants} Anmeldungen(en)";
            FindViewById<View>(Resource.Id.TournamentCoD4).FindViewById<TextView>(Resource.Id.TournamentGameStyle).Text = $"{tournaments.CallOfDuty.Mode}";
            FindViewById<View>(Resource.Id.TournamentCoD4).FindViewById<TextView>(Resource.Id.TournamentGameTime).Text = $"{culture.DateTimeFormat.GetDayName(tournaments.CallOfDuty.StartTime.DayOfWeek)} {tournaments.CallOfDuty.StartTime.Hour}:{tournaments.CallOfDuty.StartTime.Minute}";
            FindViewById<View>(Resource.Id.TournamentCoD4).FindViewById<TextView>(Resource.Id.TournamentGameTitle).Text = $"{tournaments.CallOfDuty.Name}";

            //Counterstrike

            FindViewById<View>(Resource.Id.TournamentCSGO).FindViewById<TextView>(Resource.Id.TournamentGameRegistrations).Text = $"{tournaments.Counterstrike.NumberParticipants} Anmeldungen(en)";
            FindViewById<View>(Resource.Id.TournamentCSGO).FindViewById<TextView>(Resource.Id.TournamentGameStyle).Text = $"{tournaments.Counterstrike.Mode}";
            FindViewById<View>(Resource.Id.TournamentCSGO).FindViewById<TextView>(Resource.Id.TournamentGameTime).Text = $"{culture.DateTimeFormat.GetDayName(tournaments.Counterstrike.StartTime.DayOfWeek)} {tournaments.Counterstrike.StartTime.Hour}:{tournaments.Counterstrike.StartTime.Minute}";
            FindViewById<View>(Resource.Id.TournamentCSGO).FindViewById<TextView>(Resource.Id.TournamentGameTitle).Text = $"{tournaments.Counterstrike.Name}";

            //Counterstrike

            FindViewById<View>(Resource.Id.TournamentHearthstone).FindViewById<TextView>(Resource.Id.TournamentGameRegistrations).Text = $"{tournaments.Hearthstone.NumberParticipants} Anmeldungen(en)";
            FindViewById<View>(Resource.Id.TournamentHearthstone).FindViewById<TextView>(Resource.Id.TournamentGameStyle).Text = $"{tournaments.Hearthstone.Mode}";
            FindViewById<View>(Resource.Id.TournamentHearthstone).FindViewById<TextView>(Resource.Id.TournamentGameTime).Text = $"{culture.DateTimeFormat.GetDayName(tournaments.Hearthstone.StartTime.DayOfWeek)} {tournaments.Hearthstone.StartTime.Hour}:{tournaments.Hearthstone.StartTime.Minute}";
            FindViewById<View>(Resource.Id.TournamentHearthstone).FindViewById<TextView>(Resource.Id.TournamentGameTitle).Text = $"{tournaments.Hearthstone.Name}";

            //Trackmania

            FindViewById<View>(Resource.Id.TournamentTrackmania).FindViewById<TextView>(Resource.Id.TournamentGameRegistrations).Text = $"{tournaments.Trackmania.NumberParticipants} Anmeldungen(en)";
            FindViewById<View>(Resource.Id.TournamentTrackmania).FindViewById<TextView>(Resource.Id.TournamentGameStyle).Text = $"{tournaments.Trackmania.Mode}";
            FindViewById<View>(Resource.Id.TournamentTrackmania).FindViewById<TextView>(Resource.Id.TournamentGameTime).Text = $"{culture.DateTimeFormat.GetDayName(tournaments.Trackmania.StartTime.DayOfWeek)} {tournaments.Trackmania.StartTime.Hour}:{tournaments.Trackmania.StartTime.Minute}";
            FindViewById<View>(Resource.Id.TournamentTrackmania).FindViewById<TextView>(Resource.Id.TournamentGameTitle).Text = $"{tournaments.Trackmania.Name}";
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater infl = MenuInflater;
            infl.Inflate(Resource.Menu.LoggedInProfileMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.GetItem(0).SetIcon(null);
            return base.OnPrepareOptionsMenu(menu);
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