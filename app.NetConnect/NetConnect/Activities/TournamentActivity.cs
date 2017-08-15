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
using Square.Picasso;

namespace NetConnect.Activities
{
    [Activity(Label = "TournamentActivity",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = false )]
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
        public void UpdateContentList(Data<Tournament> tournaments, string vol)
        {

            if (tournaments.Count > 0)
            {
                FindViewById<TextView>(Resource.Id.TournamentTitle).Text = GetString(Resource.String.netcon_Tournament_TitleText).Replace("#", vol);
                var culture = new System.Globalization.CultureInfo("de-DE");

                var beerpong = tournaments.Where(x => x.GameID == 1).First();
                var csgo = tournaments.Where(x => x.GameID == 2).First();
                var trackmania = tournaments.Where(x => x.GameID == 3).First();
                var cod = tournaments.Where(x => x.GameID == 4).First();
                var hs = tournaments.Where(x => x.GameID == 5).First();
                var ow = tournaments.Where(x => x.GameID == 6).First();
                var enfo = tournaments.Where(x => x.GameID == 7).First();
                var osu = tournaments.Where(x => x.GameID == 8).First();
                FindViewById<TextView>(Resource.Id.TournamentTitleSubHeader1).Text = "Friday Night Cups";
                FindViewById<TextView>(Resource.Id.TournamentTitleSubHeader2).Text = "Saturady Morning Cups";
                FindViewById<TextView>(Resource.Id.TournamentTitleSubHeader3).Text = "Saturday Night Cups";
                FindViewById<TextView>(Resource.Id.TournamentTextBierPong).Text = $"{beerpong.Name}\n{culture.DateTimeFormat.GetDayName(beerpong.StartTime.DayOfWeek)}\n{beerpong.StartTime.ToString("hh:mm")} - {beerpong.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextCOD).Text = $"{cod.Name}\n{culture.DateTimeFormat.GetDayName(cod.StartTime.DayOfWeek)}\n{cod.StartTime.ToString("hh:mm")} - {cod.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextTrackmania).Text = $"{trackmania.Name}\n{culture.DateTimeFormat.GetDayName(trackmania.StartTime.DayOfWeek)}\n{trackmania.StartTime.ToString("hh:mm")} - {trackmania.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextCSGO).Text = $"{csgo.Name}\n{culture.DateTimeFormat.GetDayName(csgo.StartTime.DayOfWeek)}\n{csgo.StartTime.ToString("hh:mm")} - {csgo.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextHearthstone).Text = $"{hs.Name}\n{culture.DateTimeFormat.GetDayName(hs.StartTime.DayOfWeek)}\n{hs.StartTime.ToString("hh:mm")} - {hs.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextEnfos).Text = $"{enfo.Name}\n{culture.DateTimeFormat.GetDayName(enfo.StartTime.DayOfWeek)}\n{enfo.StartTime.ToString("hh:mm")} - {enfo.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextOsu).Text = $"{osu.Name}\n{culture.DateTimeFormat.GetDayName(osu.StartTime.DayOfWeek)}\n{osu.StartTime.ToString("hh:mm")} - {osu.EndTime.ToString("hh:mm")}";
                FindViewById<TextView>(Resource.Id.TournamentTextOverwatch).Text = $"{ow.Name}\n{culture.DateTimeFormat.GetDayName(ow.StartTime.DayOfWeek)}\n{ow.StartTime.ToString("hh:mm")} - {ow.EndTime.ToString("hh:mm")}";

            }        }
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
        public void WebsiteLogin()
        {
            Intent openurl = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://www.lan-netconnect.de/#/login"));
            StartActivity(openurl);
        }
        public void SetUpUi(string vol)
        {
            FindViewById<Button>(Resource.Id.TournamentAnmeldenButton).Click += TournamentActivity_Click;
            FindViewById<TextView>(Resource.Id.TournamentTitle).Text = GetString(Resource.String.netcon_Tournament_TitleText).Replace("#", vol);
            Picasso.With(this).Load(Resource.Drawable.backgroundbeerpong).Into(FindViewById<ImageView>(Resource.Id.TournamentBierPong));
            Picasso.With(this).Load(Resource.Drawable.backgroundcallofduty).Into(FindViewById<ImageView>(Resource.Id.TournamentCOD));
            Picasso.With(this).Load(Resource.Drawable.backgroundcounterstrike).Into(FindViewById<ImageView>(Resource.Id.TournamentCSGO));
            Picasso.With(this).Load(Resource.Drawable.backgroundhearthstone).Into(FindViewById<ImageView>(Resource.Id.TournamentHearthstoneImage));
            Picasso.With(this).Load(Resource.Drawable.backgroundtrackmania).Into(FindViewById<ImageView>(Resource.Id.TournamentTrackmaniaImage));
            Picasso.With(this).Load(Resource.Drawable.backgroundosu).Into(FindViewById<ImageView>(Resource.Id.TournamentOsuImage));
            Picasso.With(this).Load(Resource.Drawable.backgroundow).Into(FindViewById<ImageView>(Resource.Id.TournamentOverwatchImage));
            Picasso.With(this).Load(Resource.Drawable.backgroundenfos).Into(FindViewById<ImageView>(Resource.Id.TournamentEnfosImage));
        }

        private void TournamentActivity_Click(object sender, EventArgs e)
        {
            this.Controller.LoginWebsite();
        }
    }
}