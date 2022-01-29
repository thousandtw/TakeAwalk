using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using TakeAwalk.DBSource;

namespace TakeAwalk.Auth
{
    public class AuthManager
    {
        /// <summary> 取得已登入的使用者資訊 (如果沒有登入就回傳 null) </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser()
        {

            bool isAuth = HttpContext.Current.Request.IsAuthenticated; //是否登入
            var user = HttpContext.Current.User;                       //取得使用者

            if (isAuth && user != null)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity;  //驗證轉型

                if (identity == null)
                {
                    HttpContext.Current.Response.Redirect("/Login.aspx");
                    return null;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("/Login.aspx");
                return null;
            }
            string account = user.Identity.Name;

            if (account == null)
                return null;

            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                FormsAuthentication.SignOut();
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.CustomerID = userInfo.CustomerID;
            model.Name = userInfo.Name;
            model.IdNumber = userInfo.IdNumber;
            model.MobilePhone = userInfo.MobilePhone;
            model.Email = userInfo.Email;
            model.Account = userInfo.Account;
            model.UserLevel = userInfo.UserLevel;

            return model;
        }

        /// <summary> 登出 </summary>
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary> 嘗試登入 </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            // 檢查空白
            if (string.IsNullOrWhiteSpace(account) ||
                string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "<span style='color:red'>請輸入帳號/密碼</span>";
                return false;
            }

            // 讀取和檢查資料庫
            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            // 檢查 Null
            if (userInfo == null)
            {
                errorMsg = $"<span style='color:red'>帳號: {account} 輸入錯誤</span>";
                return false;
            }

            //檢查帳號是否黑名單
            if (userInfo.UserLevel == 2)
            {
                errorMsg = "<span style='color:red'>此帳號已被停用</span>";
                return false;
            }

            // 檢查帳號/密碼
            if (string.Compare(userInfo.Account, account, true) == 0 &&
                string.Compare(userInfo.Password, pwd, false) == 0)
            {
                //HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "<span style='color:red'>登入失敗，請重新確認帳號/密碼</span>";
                return false;
            }
        }
    }
}
