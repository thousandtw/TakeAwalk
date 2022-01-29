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
    public partial class OrderCancle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser.UserLevel == 0)
            {
                this.gv_orderlist.DataSource = OrdersManager.GetOrdersList_AdminOnly();
                this.gv_orderlist.DataBind();
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

            // 取得訂單資料
            var list = OrdersManager.GetOrdersList_AdminOnly();

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);

                this.gv_orderlist.DataSource = pagedList;
                this.gv_orderlist.DataBind();

                this.ucPager.TotalSize = list.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.gv_orderlist.Visible = false;
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

        private List<Manager_OrderList_View> GetPagedDataTable(List<Manager_OrderList_View> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Visible = false;
            string starttxt = this.txb_start.Text;
            string endtxt = this.txb_End.Text;

            if (string.IsNullOrWhiteSpace(starttxt) || string.IsNullOrEmpty(endtxt)) // 檢查有無輸入日期
            {
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤，請重新選取日期</span>";
                return;
            }

            try                                // 檢查是否符合DateTime格式(例外輸入狀況:年份五位數&無選取日期)
            {
                DateTime.Parse(starttxt);
                DateTime.Parse(endtxt);
            }
            catch (Exception)
            {
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤，請重新選取日期</span>";
                return;
            }
            DateTime start_d = DateTime.Parse(starttxt);
            DateTime end_d = DateTime.Parse(endtxt);

            if(end_d <= start_d)
            {
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>結束日期必須大於起始日期，請重新選取日期</span>";
            }

            var list = OrdersManager.GetOrdersByDate_AdminOnly(start_d, end_d);

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);


                this.gv_orderlist.Visible = true;
                this.gv_orderlist.DataSource = pagedList;
                this.gv_orderlist.DataBind();

                this.ucPager.TotalSize = list.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.gv_orderlist.Visible = false;
                this.plcNoData.Visible = true;
            }
        }
    }
}