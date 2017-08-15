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
using Newtonsoft.Json;
using DataNetConnect.ViewModels.Seating;

namespace MonoNetConnect.InternalModels
{
        public class Seat : SeatingViewModel, IApiModels
    {
        private static readonly String SeatingApiPath = @"api.php/app/Seating";
        
        public string ApiPath()
        {
            return SeatingApiPath;
        }

        public string GetImageDirectoryPath()
        {
            return "";
        }

        public string GetLocalImageName()
        {
            return null;
        }

        public string GetLocalImageName(string FullApiPathImageName)
        {
            return null;
        }

        public bool IsClassWithImage()
        {
            return false;
        }
        
    }
}