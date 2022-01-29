<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserLevel.aspx.cs" Inherits="TakeAwalk.SystemAdmin.UserLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>會員權限</h2>
                <br />
                目前使用者:
                <asp:Literal ID="ltl_Name" runat="server"></asp:Literal><br />
                使用者權限:
                <asp:DropDownList runat="server" ID="UserLv_ddl">
                    <asp:ListItem Value="0">管理者</asp:ListItem>
                    <asp:ListItem Value="1">一般使用者</asp:ListItem>
                    <asp:ListItem Value="2">黑名單</asp:ListItem>
                </asp:DropDownList><br />
                <asp:Button runat="server" Text="保存" ID="Save_btn" OnClick="Save_btn_Click" />
            </div>
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
