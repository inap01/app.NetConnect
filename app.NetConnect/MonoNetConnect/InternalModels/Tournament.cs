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
using System.IO;
using DataNetConnect.ViewModels.Tournament;

namespace MonoNetConnect.InternalModels
{  
    public class Tournament : TournamentViewModel, IApiModels
    {
        private static readonly String IconPath = @"Images/Icons/Tournament";
        private static readonly String ApiUpdatePath = "api.php/app/Tournament/lan";
        [JsonProperty("player")]
        public List<Team> player { get; set; }
        public string GetLocalImageName(string FullApiPathImageName)
        {
            return FullApiPathImageName.Split('/').Last();
        }
        public string ApiPath()
        {
            return string.Join("/", ApiUpdatePath, DataContext.GetDataContext().Settings.Volume);
        }

        public string GetImageDirectoryPath()
        {
            return "";
        }

        public bool IsClassWithImage()
        {
            return new Participant().IsClassWithImage() || new Team().IsClassWithImage();
        }

        public string GetLocalImageName()
        {
            return null;
        }

        public class Participant : ParticipantViewModel, IApiModels
        {

            public string ApiPath()
            {
                return "";
            }

            public string GetImageDirectoryPath()
            {
                throw new NotImplementedException();
            }

            public bool IsClassWithImage()
            {
                return false;
            }
            public string GetLocalImageName(string FullApiPathImageName)
            {
                return null;
            }

            public string GetLocalImageName()
            {
                return null;
            }
        }
        public class Team : TeamViewModel, IApiModels
        {
            public string ApiPath()
            {
                return "";
            }

            public string GetImageDirectoryPath()
            {
                throw new NotImplementedException();
            }

            public bool IsClassWithImage()
            {
                return false;
            }
            public string GetLocalImageName(string FullApiPathImageName)
            {
                return null;
            }

            public string GetLocalImageName()
            {
                return null;
            }
        }

        
    }
    
}