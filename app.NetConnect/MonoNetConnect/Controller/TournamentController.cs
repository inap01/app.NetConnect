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
using MonoNetConnect.InternalModels;

namespace MonoNetConnect.Controller
{
    public interface ITournamentController : IBaseViewController
    {
        void UpdateContentList(Tournaments tournaments);
        void SetUpUi(string vol);
    }
    public class TournamentController : BaseViewController<ITournamentController>
    {
        public TournamentController(ITournamentController viewController)
            :base(viewController)
        {

        }
        public void UpdateReceived()
        {
            _viewController.UpdateContentList(dataContext.Tournaments);
        }
        public void setUpUi()
        {
            _viewController.SetUpUi(dataContext.Settings.Volume);
            UpdateReceived();
        }
    }
}