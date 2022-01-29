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
    public partial class Manager_CreateTicket : System.Web.UI.Page
    {
        public UserInfoModel currentUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser= AuthManager.GetCurrentUser();
            if (currentUser.UserLevel != 0)
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void Return_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/Manager_TicketList.aspx");
        }

        protected void Save_btn_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltlMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            Regex rx = new Regex(@"[\d\u4E00-\u9FA5A-Za-z]");
            if (!rx.IsMatch(Name_tbx.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>票券名稱不能為特殊字元,請重新輸入</span>";
                return;
            }
            if (!rx.IsMatch(Company_tbx.Text))
            {
                this.ltlMsg.Text = "<span style='color:red'>主辦單位不能為特殊字元,請重新輸入</span>";
                return;
            }

            currentUser = AuthManager.GetCurrentUser();
            var customerid = currentUser.CustomerID;

            DateTime start_d = DateTime.Parse(this.Start_tbx.Text);
            DateTime end_d = DateTime.Parse(this.End_tbx.Text);
            int price = int.Parse(this.Price_tbx.Text);
            int stocks = int.Parse(this.Stocks_tbx.Text);

            if (end_d < start_d)
            {
                this.ltlMsg.Text = "<span style='color:red'>活動日期輸入無效，結束日期必須大於開始日期。請重新輸入</span>";
                return;
            }
            if (price < 0)
            {
                this.ltlMsg.Text = "<span style='color:red'>定價不能為負或零元,請重新輸入</span>";
                return;
            }
            if (stocks < 0)
            {
                this.ltlMsg.Text = "<span style='color:red'>庫存不能為負或零,請重新輸入</span>";
                return;
            }

            int enable_value = int.Parse(this.Enabled_ddl.SelectedValue);
            bool enablestatus;
            if (enable_value == 0)
            {
                enablestatus = true;
            }
            else
                enablestatus = false;

            TrainTicket trainticket = new TrainTicket()
            {
                TicketName = this.Name_tbx.Text,
                TrainCompany = this.Company_tbx.Text,
                ActivityStartDate = start_d,
                ActivityEndDate = end_d,
                Price = price,
                Stocks = stocks,
                Creator = customerid,
                CreateDate = DateTime.Now,
                IsEnabled = enablestatus

            };

            TicketManager.CreateTickets(trainticket);
            Response.Redirect("/SystemAdmin/Manager_TicketList.aspx");
        }
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msglist = new List<string>();

            if (string.IsNullOrWhiteSpace(this.Name_tbx.Text) || string.IsNullOrEmpty(this.Name_tbx.Text))
                msglist.Add("<span style='color:red'>票券名稱為必填</span>");
            else if (string.IsNullOrWhiteSpace(this.Company_tbx.Text) || string.IsNullOrEmpty(this.Company_tbx.Text))
                msglist.Add("<span style='color:red'>主辦單位為必填</span>");
            else if (string.IsNullOrWhiteSpace(this.Start_tbx.Text) || string.IsNullOrEmpty(this.Start_tbx.Text))
                msglist.Add("<span style='color:red'>活動開始日期為必填</span>");
            else if (string.IsNullOrWhiteSpace(this.End_tbx.Text) || string.IsNullOrEmpty(this.End_tbx.Text))
                msglist.Add("<span style='color:red'>活動結束日期為必填</span>");
            else if (string.IsNullOrWhiteSpace(this.Price_tbx.Text) || string.IsNullOrEmpty(this.Price_tbx.Text))
                msglist.Add("<span style='color:red'>定價為必填</span>");
            else if (string.IsNullOrWhiteSpace(this.Stocks_tbx.Text) || string.IsNullOrEmpty(this.Stocks_tbx.Text))
                msglist.Add("<span style='color:red'>庫存為必填</span>");

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;
        }

    }
}