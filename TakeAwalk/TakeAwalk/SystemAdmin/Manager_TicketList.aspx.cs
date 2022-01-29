using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.SystemAdmin
{
    public partial class Manager_TicketList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser.UserLevel == 0)
            {
                this.gv_ticketslist.DataSource = TicketManager.GetTrainTicketsList_AdminOnly();
                this.gv_ticketslist.DataBind();
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

            var list = TicketManager.GetTrainTicketsList_AdminOnly();

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);

                this.gv_ticketslist.DataSource = pagedList;
                this.gv_ticketslist.DataBind();

                this.ucPager.TotalSize = list.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.gv_ticketslist.Visible = false;
                this.plcNoData.Visible = true;
            }
        }
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;
            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;
            if (intPage <= 0)
                return 1;
            return intPage;
        }

        private List<TrainTicket> GetPagedDataTable(List<TrainTicket> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }

        protected void NewTicket_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/Manager_CreateTicket.aspx");
        }
    }
}