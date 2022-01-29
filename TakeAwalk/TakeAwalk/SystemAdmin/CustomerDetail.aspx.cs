using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.SystemAdmin
{
    public partial class CustomerDetail : System.Web.UI.Page
    {
        public UserInfoModel currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AuthManager.GetCurrentUser();

            if (!IsPostBack)
            {
                this.ltlAccount.Text = currentUser.Account;
                this.txtName.Text = currentUser.Name;
                this.txtID.Text = currentUser.IdNumber;
                this.txtMobilePhone.Text = currentUser.MobilePhone.ToString();
                this.txtEmail.Text = currentUser.Email;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/CustomerInfo.aspx");
        }

        protected void btnPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/CustomerPasswordChange.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            Regex rx = new Regex(@"[\d\u4E00-\u9FA5A-Za-z]");
            if (!rx.IsMatch(txtName.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>姓名不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txtMobilePhone.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>電話不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(txtID.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>身分證不能為特殊字元,請重新輸入</span>";
                return;
            }

            currentUser = AuthManager.GetCurrentUser();
            var customerid = currentUser.CustomerID;

            UserInfo userInfo = new UserInfo()
            {
                Name = txtName.Text,
                MobilePhone = txtMobilePhone.Text,
                Email = txtEmail.Text,
                IdNumber = txtID.Text,
            };

            UserInfoManager.UpdateCustomer(customerid, userInfo);

            Response.Redirect("CustomerInfo.aspx");

        }
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msglist = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrEmpty(this.txtName.Text))
                msglist.Add("<span style='color:red'>姓名為必填</span>");

            else if (string.IsNullOrWhiteSpace(this.txtEmail.Text) || string.IsNullOrEmpty(this.txtEmail.Text))
                msglist.Add("<span style='color:red'>信箱為必填</span>");

            else if (string.IsNullOrWhiteSpace(this.txtMobilePhone.Text) || string.IsNullOrEmpty(this.txtMobilePhone.Text))
                msglist.Add("<span style='color:red'>電話為必填</span>");
            else if (this.txtMobilePhone.Text.Length != 10)
                msglist.Add("<span style='color:red'>電話長度應為十碼.</span>");

            else if (string.IsNullOrWhiteSpace(this.txtID.Text) || string.IsNullOrEmpty(this.txtID.Text))
                msglist.Add("<span style='color:red'>身分字號為必填</span>");
            else if (this.txtID.Text.Length != 10)
                msglist.Add("<span style='color:red'>身分證長度應為十碼.</span>");

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;
        }

    }
}