<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerDetail.aspx.cs" Inherits="TakeAwalk.SystemAdmin.CustomerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>會員中心</h2>
                <table>
                    <tr>
                        <th>帳號:</th>
                        <td>
                            <asp:Literal ID="ltlAccount" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <th>姓名:</th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>身分證字號:</th>
                        <td>
                            <asp:TextBox ID="txtID" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>電話:</th>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" runat="server" TextMode="Phone" pattern="[0-9]{3}[0-9]{3}[0-9]{4}" oninvalid="setCustomValidity('電話長度應為十碼數字')" oninput="this.setCustomValidity('')"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Email:</th>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal><br />
                <asp:Button ID="btnEdit" runat="server" Text="確定修改" OnClick="btnEdit_Click" />
                <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" /><br />
                <br />
                <asp:Button ID="btnPwd" runat="server" Text="變更密碼" OnClick="btnPwd_Click" />
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
