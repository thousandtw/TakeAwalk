using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TakeAwalk.Auth;
using TakeAwalk.DBSource;

namespace TakeAwalk.SystemAdmin
{
    public partial class UserLevel : System.Web.UI.Page
    {
        public UserInfoModel currentUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AuthManager.GetCurrentUser();
            if (currentUser.UserLevel != 0)
            {
                Response.Redirect("/Login.aspx");
            }

            if (!IsPostBack)
            {
                string customeridtxt = this.Request.QueryString["CustomerID"];
                Guid customerid = Guid.Parse(customeridtxt);

                var currentUser = UserInfoManager.GetUserInfoByID(customerid);
                string name = currentUser.Name;


                this.ltl_Name.Text = name;
                this.UserLv_ddl.SelectedValue = currentUser.UserLevel.ToString();
            }
        }

        protected void Save_btn_Click(object sender, EventArgs e)
        {
            string customeridtxt = this.Request.QueryString["CustomerID"];
            Guid customerid = Guid.Parse(customeridtxt);

            string leveltxt = this.UserLv_ddl.SelectedValue.ToString();
            int level = int.Parse(leveltxt);

            UserInfoManager.UpdateUserLevel(customerid, level);

            Response.Redirect("/SystemAdmin/CustomerList.aspx");
        }
    }
}