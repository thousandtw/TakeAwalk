using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            Guid Cid = Guid.NewGuid();

            Regex rx = new Regex(@"[\d\u4E00-\u9FA5A-Za-z]");           //正則表達式排除特殊字元
            if (!rx.IsMatch(txbName.Text))
            {
                this.ltMsg.Text = "<span style='color:red'>姓名不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txbMobilePhone.Text))
            {
                this.ltMsg.Text = "<span style='color:red'>電話不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txbAccount.Text))
            {
                this.ltMsg.Text = "<span style='color:red'>帳號不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txbPassword.Text))
            {
                this.ltMsg.Text = "<span style='color:red'>密碼不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txbId.Text))
            {
                this.ltMsg.Text = "<span style='color:red'>身分證不能為特殊字元,請重新輸入</span>";
                return;
            }

            var account = txbAccount.Text;
            var email = txbEmail.Text;
            if ( UserInfoManager.GetUserByEmail(email) || UserInfoManager.GetUserByAccount(account))
            {
                this.ltMsg.Text = "<span style='color:red'>帳號或信箱已註冊會員,請重新輸入</span>";
                return;
            }
           
            UserInfo userInfo = new UserInfo()
            {
                Name = txbName.Text,
                MobilePhone = txbMobilePhone.Text,
                Email = txbEmail.Text,
                Account = txbAccount.Text,
                Password = txbPassword.Text,
                IdNumber = txbId.Text,
                CustomerID = Cid,
                UserLevel=1
            };

            UserInfoManager.CreateCustomer(userInfo);

            Response.Redirect("/Login.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msglist = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txbAccount.Text) || string.IsNullOrEmpty(this.txbAccount.Text))
                msglist.Add("<span style='color:red'>帳號為必填(英數字元)</span>");
            else if (this.txbAccount.Text.Length < 3 || this.txbAccount.Text.Length >18)
                msglist.Add("<span style='color:red'>帳號長度應為3到18碼</span>");

            else if (string.IsNullOrWhiteSpace(this.txbPassword.Text) || string.IsNullOrEmpty(this.txbPassword.Text))
                msglist.Add("<span style='color:red'>密碼為必填(英數字元)</span>");
            else if (this.txbPassword.Text.Length < 8 || this.txbPassword.Text.Length >18)
                msglist.Add("<span style='color:red'>密碼長度應為8到18碼</span>");

            else if (string.IsNullOrWhiteSpace(this.txbName.Text) || string.IsNullOrEmpty(this.txbName.Text))
                msglist.Add("<span style='color:red'>姓名為必填</span>");

            else if (string.IsNullOrWhiteSpace(this.txbId.Text) || string.IsNullOrEmpty(this.txbId.Text))
                msglist.Add("<span style='color:red'>身分字號為必填</span>");
            else if (this.txbId.Text.Length != 10)
                msglist.Add("<span style='color:red'>身分證長度應為10碼</span>");

            else if (string.IsNullOrWhiteSpace(this.txbMobilePhone.Text) || string.IsNullOrEmpty(this.txbMobilePhone.Text))
                msglist.Add("<span style='color:red'>電話為必填</span>");
            else if (this.txbMobilePhone.Text.Length != 10)
                msglist.Add("<span style='color:red'>電話長度應為10碼</span>");

            else if (string.IsNullOrWhiteSpace(this.txbEmail.Text) || string.IsNullOrEmpty(this.txbEmail.Text))
                msglist.Add("<span style='color:red'>信箱為必填</span>");

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;
        }
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            this.txbAccount.Text = string.Empty;
            this.txbPassword.Text = string.Empty;
            this.txbName.Text = string.Empty;
            this.txbId.Text = string.Empty;
            this.txbMobilePhone.Text = string.Empty;
            this.txbEmail.Text = string.Empty;
            Response.Redirect("/Default.aspx");
        }
    }
}