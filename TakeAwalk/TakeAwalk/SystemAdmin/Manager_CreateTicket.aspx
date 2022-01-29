<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="Manager_CreateTicket.aspx.cs" Inherits="TakeAwalk.SystemAdmin.Manager_CreateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>新增優惠票</h2>
                <br />
                票券名稱:<asp:TextBox ID="Name_tbx" runat="server"></asp:TextBox><br />
                主辦單位:<asp:TextBox ID="Company_tbx" runat="server"></asp:TextBox><br />
                活動開始日期:<asp:TextBox ID="Start_tbx" runat="server" TextMode="Date"></asp:TextBox><br />
                活動結束日期:<asp:TextBox ID="End_tbx" runat="server" TextMode="Date"></asp:TextBox><br />
                定價:<asp:TextBox ID="Price_tbx" runat="server" TextMode="Number"></asp:TextBox><br />
                庫存:<asp:TextBox ID="Stocks_tbx" runat="server" TextMode="Number"></asp:TextBox><br />
                上架狀態:<asp:DropDownList ID="Enabled_ddl" runat="server">
                    <asp:ListItem Value="0">上架</asp:ListItem>
                    <asp:ListItem Value="1">下架</asp:ListItem>
                </asp:DropDownList><br />
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal><br />
                <asp:Button ID="Save_btn" runat="server" Text="保存" OnClick="Save_btn_Click" />
                <asp:Button ID="Return_btn" runat="server" Text="返回" OnClick="Return_btn_Click" />
            </div>
        </div>
    </div>
</asp:Content>
