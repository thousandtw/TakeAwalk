using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txbAccount.Text;
            string inp_PWD = txbPassword.Text;

            string msg;
            if (!AuthManager.TryLogin(inp_Account, inp_PWD, out msg))
            {
                this.ltlMsg.Text = msg;
                return;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount(inp_Account);

            if (userInfo.Account == this.txbAccount.Text && userInfo.Password == this.txbPassword.Text)
            {
                string account = userInfo.Account;       //帳號
                string usreID = userInfo.IdNumber;    //id
                string[] roles = { userInfo.Name };  //角色
                bool isPersistance = false;

                FormsAuthentication.SetAuthCookie(account, isPersistance);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                    (
                    1,                            //版本
                    account,                      //帳號
                    DateTime.Now,                 //發證時間
                    DateTime.Now.AddHours(12.0),  //逾時時間
                    isPersistance,                //是否在50年
                    usreID                        //id            
                    );

                FormsIdentity identity = new FormsIdentity(ticket);
                HttpCookie cookie =
                    new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket)                    //序列化混淆
                        );
                cookie.HttpOnly = true;

                GenericPrincipal gp = new GenericPrincipal(identity, roles);   //放入驗證&角色
                HttpContext.Current.User = gp;
                HttpContext.Current.Response.Cookies.Add(cookie);

                Response.Redirect("/SystemAdmin/CustomerInfo.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}