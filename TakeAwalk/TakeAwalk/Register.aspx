<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TakeAwalk.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>註冊會員</h2>
                <br />
                <table>
                    <tr>
                        <td>帳號:<asp:TextBox ID="txbAccount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>密碼:<asp:TextBox ID="txbPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>姓名:<asp:TextBox ID="txbName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>身分證字號:<asp:TextBox ID="txbId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>電話:<asp:TextBox ID="txbMobilePhone" runat="server" TextMode="Phone" pattern="[0-9]{3}[0-9]{3}[0-9]{4}"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>信箱:<asp:TextBox ID="txbEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Literal ID="ltMsg" runat="server"></asp:Literal><br />
                <br />
                <div>
                    <span>
                        <asp:Button ID="btnRegister" runat="server" Text="註冊" OnClick="btnRegister_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
