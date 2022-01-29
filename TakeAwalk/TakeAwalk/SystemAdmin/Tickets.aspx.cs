using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.SystemAdmin
{
    public partial class Tickets : System.Web.UI.Page
    {
        public UserInfoModel currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AuthManager.GetCurrentUser();
            if (!this.IsPostBack)
            {
                this.gv_ticket.DataSource = TicketManager.GetTrainTicketsList();
                this.gv_ticket.DataBind();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6]
            { new DataColumn("TicketID"), new DataColumn("TicketContent_Confirm"), new DataColumn("TrainCompany_Confirm"), new DataColumn("TicketPrice_Confirm"), new DataColumn("Quantity_Confirm"), new DataColumn("Stocks_Confirm") });

            List<string> MsgList = new List<string>();

            foreach (GridViewRow row in gv_ticket.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbox = (row.Cells[7].FindControl("cbox") as CheckBox);
                    if (cbox.Checked)
                    {
                        int ticketid = int.Parse(row.Cells[0].Text);
                        string ticketcontent = row.Cells[1].Text;
                        string traincompany = row.Cells[2].Text;
                        string ticketprice = row.Cells[5].Text;
                        DropDownList q = row.Cells[6].FindControl("ddl_quantity") as DropDownList;
                        int quantity = int.Parse(q.SelectedValue);
                        string StocksMsg;

                        TicketManager.IsEnoughStocks(ticketid, quantity, ticketcontent, out StocksMsg);
                        if (StocksMsg != null)
                        {
                            MsgList.Add(StocksMsg);
                        }

                        dt.Rows.Add(ticketid, ticketcontent, traincompany, ticketprice, quantity);
                    }
                }
            }
            if (dt.Rows.Count == 0)
            {
                this.lbError.Visible = true;
                this.lbError.Text = "未勾選任何優惠票，請按取消後重新操作";
                this.btnConfirm.Visible = false;
                this.btnBuy.Visible = true;
                this.btnBuy.Enabled = false;
                return;
            }
            else if (MsgList.Count != 0)
            {
                foreach (var Msg in MsgList)
                {
                    this.ltlMsg.Text += (Msg + "<br/>");
                }
                this.ltlMsg.Visible = true;
                this.btnConfirm.Enabled = false;
                return;
            }
            else
            {
                this.gv_ticket.Visible = false;
                this.gv_selected.Visible = true;
                this.gv_selected.DataSource = dt;
                this.gv_selected.DataBind();
                this.btnConfirm.Visible = false;
                this.btnBuy.Visible = true;
                this.btnBuy.Enabled = true;

                int dr_cnt = dt.Rows.Count;
                decimal total = 0;
                int ticket_cnt = 0;
                for (int i = 0; i < dr_cnt; i++)
                {
                    decimal amount = decimal.Parse(dt.Rows[i]["TicketPrice_Confirm"].ToString()) * decimal.Parse(dt.Rows[i]["Quantity_Confirm"].ToString());
                    total += amount;
                    int ticket = int.Parse(dt.Rows[i]["Quantity_Confirm"].ToString());
                    ticket_cnt += ticket;
                }
                this.lbAmount.Visible = true;
                this.lbAmount.Text = $"小計: {total} 元 \t\t {ticket_cnt} 張";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnConfirm.Enabled = true;
            this.btnConfirm.Visible = true;
            this.btnBuy.Visible = false;
            this.gv_ticket.Visible = true;
            this.gv_selected.Visible = false;
            this.lbError.Visible = false;
            this.lbAmount.Visible = false;
            this.ltlMsg.Visible = false;
            this.ltlMsg.Text = string.Empty;
            this.gv_ticket.DataSource = TicketManager.GetTrainTicketsList();
            this.gv_ticket.DataBind();
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

            this.gv_selected.Visible = true;
            string TicketFullName = "";
            var currentUser = AuthManager.GetCurrentUser();

            Order order = new Order()
            {
                CustomerID = currentUser.CustomerID,
                OrderStatus = 0,
                CreateDate = DateTime.Now,
                Creator = currentUser.CustomerID
            };
            TicketManager.CreateTicketOrders_OrdersTable(order);

            for (int i = 0; i < gv_selected.Rows.Count; i++)
            {
                TicketFullName += this.gv_selected.Rows[i].Cells[1].Text.Trim() + this.gv_selected.Rows[i].Cells[5].Text.Trim() + "張,";

                string ticketnametxt = this.gv_selected.Rows[i].Cells[1].Text.Trim();
                string ticketidtxt = this.gv_selected.Rows[i].Cells[0].Text;
                int ticketid = int.Parse(ticketidtxt);
                string quantitytxt = this.gv_selected.Rows[i].Cells[5].Text;
                int quantity = int.Parse(quantitytxt);

                TicketManager.UpdateStock(ticketid, quantity);

                TakeAwalk.ORM.DBModels.OrderDetail orderdetail = new TakeAwalk.ORM.DBModels.OrderDetail()
                {
                    OrderID = order.OrderID,
                    TicketID = ticketid,
                    Quantity = quantity,
                    CreateDate = order.CreateDate,
                    Creator = order.Creator
                };
                TicketManager.CreateTicketOrders_OrderDetailsTable(orderdetail);
            };

            var name = currentUser.Name;
            string subject = "TakeAwalk火車訂票系統-訂票完成通知信";
            string body = $"親愛的{name}會員 您好\r\n您的訂票內容如下:\r\n{TicketFullName}\r\n感謝您的支持,TakeAwalk祝您旅途平安.";
            string email = currentUser.Email;
            try
            {
                UserInfoManager.SendAutomatedEmail(email, body, subject);
            }
            catch (Exception)
            {
                this.ltlMsg.Visible = true;
                this.ltlMsg.Text = $"<span style='color:red'>訂單已成立,由於系統異常,通知信尚未寄發,稍後為您寄送.</span>";
                return;
            }

            Response.Redirect("/SystemAdmin/OrderList.aspx");
        }
    }
}