<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="TakeAwalk.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>&nbsp;</h2>
                <h2>&nbsp;</h2>
                <h2>忘記密碼</h2>
                <br />
                <table>
                    <tr>
                        <td>帳號:<asp:TextBox ID="txbAccount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>信箱:<asp:TextBox ID="txbEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                <br />
                <div>
                    <span>
                        <asp:Button ID="btnSave" runat="server" Text="送出" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
