<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerInfo.aspx.cs" Inherits="TakeAwalk.SystemAdmin.Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>會員中心</h2>
                <br />
                <table>
                    <tr>
                        <th>帳號:</th>
                        <td>
                            <asp:Literal ID="ltAccount" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>姓名:</th>
                        <td>
                            <asp:Literal ID="ltName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>身分證字號:</th>
                        <td>
                            <asp:Literal ID="ltID" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>電話:</th>
                        <td>
                            <asp:Literal ID="ltMobilePhone" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>Email:</th>
                        <td>
                            <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnEdit" runat="server" Text="編輯" OnClick="btnEdit_Click" />
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
