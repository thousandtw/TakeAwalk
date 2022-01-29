using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.Support
{
    public class dbSupport
    {
        public static bool trySearch(string account, string email, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(email))
            {
                errorMsg = "請輸入帳號和信箱。";
                return false;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                errorMsg = "帳號輸入錯誤。";
                return false;
            }
            if (string.Compare(userInfo.Account, account) == 0 &&
                string.Compare(userInfo.Email, email) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "請重新確認帳號與信箱。";
                return false;
            }

        }
    }
}

