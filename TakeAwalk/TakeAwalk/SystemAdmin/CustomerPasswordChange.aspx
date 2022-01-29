<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerPasswordChange.aspx.cs" Inherits="TakeAwalk.SystemAdmin.CustomerPasswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>變更密碼</h2>
                <br />
                <table>
                    <tr>
                        <td>帳號:<asp:Literal ID="ltlAccount" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>原密碼:<asp:TextBox ID="txbPassword" runat="server" TextMode="Password" MaxLength="16"></asp:TextBox>
                            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>新密碼:<asp:TextBox ID="txbNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:Literal ID="ltlMsg2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>確認新密碼:<asp:TextBox ID="txbNewPasswordCmf" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Literal ID="ltlCheckInput" runat="server"></asp:Literal>
                <div>
                    <span>
                        <asp:Button ID="btnSave" runat="server" Text="確定變更" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" Text="取消變更" OnClick="btnCancle_Click" />
                    </span>
                </div>
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
