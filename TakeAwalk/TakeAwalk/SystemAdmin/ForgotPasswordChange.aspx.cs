using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;

namespace TakeAwalk.SystemAdmin
{
    public partial class ForgotPasswordChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
            {
                HttpContext.Current.Response.Redirect("/Login.aspx");
            }

            this.ltlCheckInput.Text = string.Empty;
            this.ltlMsg.Text = string.Empty;
            this.ltlMsg2.Text = string.Empty;
            this.ltlMsg1.Text = "<span style='color:red'>請於信箱收取驗證碼</span>";
            ltlAccount.Text = this.Session["UserLoginInfo"] as string;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txbAttest.Text != "9267434351")
            {
                ltlMsg1.Text = "驗證碼錯誤,請重新輸入";
                return;
            }

            Regex rx = new Regex(@"[\d\u4E00-\u9FA5A-Za-z]");             //正則表達式排除特殊字元
            if (!rx.IsMatch(txbNewPassword.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>新密碼不能為特殊字元及留空,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txbNewPasswordCmf.Text))
            {
                this.ltlMsg2.Text = "<span style='color:red'>確認密碼不能為特殊字元及留空,請重新輸入</span>";
                return;
            }

            string iptNewPWD = txbNewPassword.Text;
            string iptNewPWD_Check = txbNewPasswordCmf.Text;
            string account = this.Session["UserLoginInfo"] as string;
            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            List<string> msgList = new List<string>();
            if (!CheckInput(out msgList))
            {
                this.ltlCheckInput.Text = string.Join("<br/>", msgList);
                return;
            }
            else
            {

                if (iptNewPWD == iptNewPWD_Check)
                {
                    UserInfoManager.UpdatePWD(userInfo.CustomerID, iptNewPWD_Check);
                    Response.Redirect("/Login.aspx");
                    return;
                }
                else
                {
                    this.ltlMsg.Text = "<span style='color:red'>新密碼與確認密碼不一致,請重新輸入</span>";
                    return;
                }
            }

        }
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txbNewPassword.Text.Trim()) || string.IsNullOrEmpty(this.txbNewPassword.Text.Trim()))
            {
                msgList.Add("<span style='color:red'>請輸入新密碼</span>");
            }
            if (this.txbNewPassword.Text.Length < 8)
            {
                msgList.Add("<span style='color:red'>新密碼長度應為8到18碼</span>");
            }

            if (string.IsNullOrWhiteSpace(this.txbNewPasswordCmf.Text.Trim()) || string.IsNullOrEmpty(this.txbNewPasswordCmf.Text.Trim()))
            {
                msgList.Add("<span style='color:red'>請輸入確認密碼</span>");
            }
            if (this.txbNewPasswordCmf.Text.Length < 8)
            {
                msgList.Add("<span style='color:red'>確認密碼長度應為8到18碼</span>");
            }
            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
    }
}