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
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser.UserLevel == 0)
            {
                this.GridView1.DataSource = UserInfoManager.GetUserInfoList_AdminOnly();
                this.GridView1.DataBind();
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

            // 取得訂單資料
            var list = UserInfoManager.GetUserInfoList_AdminOnly();

            if (list.Count > 0)  // 檢查有無資料
            {
                var pagedList = this.GetPagedDataTable(list);

                this.GridView1.DataSource = pagedList;
                this.GridView1.DataBind();

                this.ucPager.TotalSize = list.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.GridView1.Visible = false;
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

        private List<UserInfo> GetPagedDataTable(List<UserInfo> list)
        {
            int startIndex = (this.GetCurrentPage() - 1) * 10;
            return list.Skip(startIndex).Take(10).ToList();
        }
    }
}