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
using DataNetConnect.ViewModels.Partner;

namespace MonoNetConnect.InternalModels
{
    public class Sponsor : PartnerViewModel, IApiModels, IHasImage
    {
        private static String SponsorApiPath = @"api.php/app/Partner";
        private static String SponsorImagePath = @"Images/Sponsor";

        public string ApiPath()
        {
            return SponsorApiPath;
        }

        public string GetImageDirectoryPath()
        {
            return SponsorImagePath;
        }

        public bool IsClassWithImage()
        {
            return true;
        }

        public string GetImage()
        {
            return this.Image;
        }
        public string GetLocalImageName()
        {
            var splitted = Image.Split('/');
            return splitted[splitted.Length - 2] + ".png";
        }
        public string GetLocalImageName(string FullApiPathImageName)
        {
            var splitted = FullApiPathImageName.Split('/');
            return splitted[splitted.Length - 2] + ".png";
        }
    }
}