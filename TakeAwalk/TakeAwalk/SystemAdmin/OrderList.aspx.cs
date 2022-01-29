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
    public partial class OrderList : System.Web.UI.Page
    {
        public UserInfoModel currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AuthManager.GetCurrentUser();

            if (!IsPostBack)
            {
                var init_list = OrdersManager.GetOrdersListbyCustomerID(currentUser.CustomerID);
                this.gv_orderlist.DataSource = init_list;
                //this.gv_orderlist.DataBind();

                if (init_list.Count > 0)  // 檢查有無資料
                {
                    var pagedList = this.GetPagedDataTable(init_list);

                    this.gv_orderlist.DataSource = pagedList;
                    this.gv_orderlist.DataBind();

                    this.ucPager.TotalSize = init_list.Count;
                    this.ucPager.Bind();
                }
                else
                {
                    this.gv_orderlist.Visible = false;
                    this.plcNoData.Visible = true;
                }
            }

            if (currentUser.UserLevel == 0)
            {
                string guidtxt = this.Request.QueryString["CustomerID"];

                var Manager_list = OrdersManager.GetOrdersListbyCustomerID(currentUser.CustomerID);
                this.gv_orderlist.DataSource = Manager_list;
                //this.gv_orderlist.DataBind();

                if (Manager_list.Count > 0)  // 檢查有無資料
                {
                    var pagedList = this.GetPagedDataTable(Manager_list);

                    this.gv_orderlist.DataSource = pagedList;
                    this.gv_orderlist.DataBind();

                    this.ucPager.TotalSize = Manager_list.Count;
                    this.ucPager.Bind();
                }
                else
                {
                    this.gv_orderlist.Visible = false;
                    this.plcNoData.Visible = true;
                }

                if (guidtxt != null)
                {
                    Guid customerid = Guid.Parse(guidtxt);
                    var Customer_list = OrdersManager.GetOrdersListbyCustomerID(customerid);
                    this.gv_orderlist.DataSource = Customer_list;
                    //this.gv_orderlist.DataBind();

                    if (Customer_list.Count > 0)  // 檢查有無資料
                    {
                        var pagedList = this.GetPagedDataTable(Customer_list);

                        this.gv_orderlist.DataSource = pagedList;
                        this.gv_orderlist.DataBind();

                        this.ucPager.TotalSize = Customer_list.Count;
                        this.ucPager.Bind();
                    }
                    else
                    {
                        this.gv_orderlist.Visible = false;
                        this.plcNoData.Visible = true;
                    }
                }
            }


            // 取得訂單資料
            var list = OrdersManager.GetOrdersListbyCustomerID(currentUser.CustomerID);

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
            currentUser = AuthManager.GetCurrentUser();
            this.ltlMsg.Visible = false;//請保留(防止重複按下,提示訊息未清空)                 

            if (string.IsNullOrWhiteSpace(this.txbStr.Text) || string.IsNullOrEmpty(this.txbEnd.Text)) // 檢查有無輸入日期
            {
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤,請重新選取日期</span>";
            }

            string start = this.txbStr.Text;
            string end = this.txbEnd.Text;
            try                                // 檢查是否符合DateTime格式(例外輸入狀況:年份五位數)
            {
                DateTime.Parse(start);
                DateTime.Parse(end);
            }
            catch (Exception)
            {
                this.gv_orderlist.Visible = false;
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤,請重新選取日期</span>";
                return;
            }
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            var list = OrdersManager.GetOrdersByDate(currentUser.CustomerID, startTime, endTime);

            if (list.Count > 0)  // 檢查有無資料
            {
                this.gv_orderlist.Visible = true;
                this.gv_orderlist.DataSource = OrdersManager.GetOrdersByDate(currentUser.CustomerID, startTime, endTime);
                this.gv_orderlist.DataBind();
            }
            else
            {
                this.gv_orderlist.Visible = false;
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = "<span style='color:red'>搜尋日期有錯誤,請重新選取日期</span>";
            }

        }
    }
}