<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="TakeAwalk.SystemAdmin.CustomerList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>客戶資料</h2>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" CellSpacing="2">
                    <Columns>
                        <%--        <asp:BoundField DataField="CustomerID" HeaderText="客戶編號" />--%>
                        <asp:BoundField DataField="Name" HeaderText="姓名" />
                        <asp:BoundField DataField="IdNumber" HeaderText="身分證字號" />
                        <asp:BoundField DataField="MobilePhone" HeaderText="手機號碼" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Account" HeaderText="帳號" />
                        <asp:BoundField DataField="Password" HeaderText="密碼" DataFormatString="********" />
                        <asp:TemplateField HeaderText="訂單紀錄">
                            <ItemTemplate>
                                <a href="OrderList.aspx?CustomerID=<%# Eval("CustomerID") %>">
                                    <img src="/Images/icons-search.png" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="UserLevel.aspx?CustomerID=<%# Eval("CustomerID") %>">編輯權限
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="CustomerList.aspx" />
                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        沒有客戶資料...
                    </p>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
