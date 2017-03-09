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
    public interface ILoginController : IBaseViewController
    {
        void LoginSucessfull();
        void LoginFailed();
    }
    public class LoginController : BaseViewController<ILoginController>
    {
        public LoginController(ILoginController viewController)
            :base(viewController)
        {
            
        }
        public void Login(string user, string pwd)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user+":"+pwd)));
            headers.Add("Auth-Token", dataContext.Token);
            var result = dataContext.PostRequestWithExpectedResult<User,BasicAPIModel<User>>(new User(), User.UserApiPath, headers);
            if(result.Status.State == ApiModel.StatusModel.Status.Success)
            {
                dataContext.isLoggedIn = true;
                dataContext.User = result.Data;
                this._viewController.LoginSucessfull();
            }
            else
            {
                dataContext.isLoggedIn = false;
                this._viewController.LoginFailed();
            }
        }
        
    }
}