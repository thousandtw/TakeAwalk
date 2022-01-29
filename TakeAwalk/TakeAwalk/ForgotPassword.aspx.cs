using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.DBSource;

namespace TakeAwalk
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            Regex rx = new Regex(@"[\d\u4E00-\u9FA5A-Za-z]");           //正則表達式排除特殊字元
            if (!rx.IsMatch(txbAccount.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>帳號不能為特殊字元及留空,請重新輸入</span>";
                return;
            }

            string account = txbAccount.Text;
            string email = txbEmail.Text;
            string time = DateTime.Now.ToLongDateString();

            string errormsg;
            if (!UserInfoManager.trySearch(account, email, out errormsg))
            {
                this.ltlMsg.Text = $"<span style='color:red'>{errormsg}</span>";
                return;
            }

            string body = $"親愛的會員 您好\r\n您的帳號 : {account} ,於{time}申請重新設定密碼.\r\n您的驗證碼為 : 9267434351 ,請回到火車訂票系統完成驗證.";
            string subject = "TakeAwalk火車訂票系統-忘記密碼確認信";
            try
            {
               UserInfoManager.SendAutomatedEmail(email, body, subject);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                this.ltlMsg.Text = $"<span style='color:red'>系統異常,忘記密碼確認信尚未寄發,請您稍後再試一次</span>";
                return;
            }

            Session["UserLoginInfo"] = txbAccount.Text;
            Response.Redirect("/SystemAdmin/ForgotPasswordChange.aspx");
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }
    }
}