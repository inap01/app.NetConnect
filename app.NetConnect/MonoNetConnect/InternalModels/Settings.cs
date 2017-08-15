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
using MonoNetConnect.Cache;
using Newtonsoft.Json;
using MonoNetConnect.Extensions;
using DataNetConnect.ViewModels.Settings;

namespace MonoNetConnect.InternalModels
{
    public class Settings : SettingsViewModel, IApiModels, IDeepCloneable<Settings>
    {
        private static String SettingsApiPath = @"api.php";       

        public string ApiPath()
        {
            return SettingsApiPath;
        }

        public string GetImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }
        public string GetLocalImageName()
        {
            return null;
        }
        public string GetLocalImageName(string FullApiPathImageName)
        {
            return null;
        }
        public Settings DeepClone()
        {
            return new Settings()
            {
                ActiveBooking = this.ActiveBooking,
                BIC = this.BIC,
                BLZ = this.BLZ,
                AdvancePayment = this.AdvancePayment,
                BoxOffice = this.BoxOffice,
                DaysReserved = this.DaysReserved,
                End = new DateTime(this.End.Ticks),
                Start = new DateTime(this.Start.Ticks),
                ID = this.ID,
                IBan = this.IBan,
                KontoChecked = new DateTime(this.KontoChecked.Ticks),
                Kontonummer = this.Kontonummer,
                LatestChange = this.LatestChange,
                Volume = this.Volume,
            };
        }

        object IDeepCloneable.DeepClone()
        {
            return this.DeepClone();
        }
    }
}