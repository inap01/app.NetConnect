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

namespace MonoNetConnect.DatabaseModels
{

    public class StatusModel
    {
        protected enum Status { Success, Warning, Danger, Info}

        protected Status state { get; set; }
        protected String Message { get; set; }

    }
}