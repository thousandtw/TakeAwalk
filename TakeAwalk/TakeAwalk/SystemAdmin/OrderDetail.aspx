<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="TakeAwalk.SystemAdmin.OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>購票內容</h2>
                <br />
                <asp:GridView ID="gv_orderdetails" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" CellSpacing="2">
                    <Columns>
                        <asp:BoundField DataField="TicketID" HeaderText="票券編號" />
                        <asp:BoundField DataField="TicketName" HeaderText="票券名稱" />
                        <asp:BoundField DataField="TrainCompany" HeaderText="主辦單位" />
                        <asp:BoundField DataField="ActivityStartDate" DataFormatString="{0:d}" HeaderText="活動開始日期" />
                        <asp:BoundField DataField="ActivityEndDate" DataFormatString="{0:d}" HeaderText="活動結束日期" />
                        <asp:BoundField DataField="Price" HeaderText="票價" />
                        <asp:BoundField DataField="Quantity" HeaderText="數量" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </div>
            <asp:Button ID="btn_delete" runat="server" OnClick="btn_delete_Click" Text="刪除訂單" OnClientClick="return confirm('確認要刪除這筆訂單嗎?');" />
        </div>
    </div>
    <!--使用者登入時,隱藏管理者頁面-->
    <script>
        var admin = document.getElementById('admin');
        var admin2 = document.getElementById('admin2');
        var admin3 = document.getElementById('admin3');
        if (1 ==<%=currentUser.UserLevel%>) {
            admin.style.display = 'none';
            admin2.style.display = 'none';
            admin3.style.display = 'none';
        }
    </script>
</asp:Content>
