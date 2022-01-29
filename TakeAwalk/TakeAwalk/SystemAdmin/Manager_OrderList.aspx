<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="Manager_OrderList.aspx.cs" Inherits="TakeAwalk.SystemAdmin.OrderCancle" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>管理客戶訂單</h2>
                <br />
                起始:<asp:TextBox ID="txb_start" runat="server" TextMode="Date"></asp:TextBox>
                結束:<asp:TextBox ID="txb_End" runat="server" TextMode="Date"></asp:TextBox>
                <asp:Button ID="btn_Search" runat="server" Text="搜尋" OnClick="btnSearch_Click" /><br />
                 <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                <asp:GridView ID="gv_orderlist" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" CellSpacing="2">
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="訂單序號" />
                        <asp:BoundField DataField="CreateDate" HeaderText="購買日期" DataFormatString="{0:yyyy/MM/dd HH:mm}" />
                        <asp:BoundField DataField="Name" HeaderText="姓名" />
                        <asp:BoundField DataField="Total" HeaderText="總金額" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <label>元</label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quantity" HeaderText="總張數" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <label>張</label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrderStatus" HeaderText="訂單狀態" DataFormatString="{0:'已完成'}" />
                        <asp:BoundField HeaderText="修改訂單日期" DataField="ModifyDate" DataFormatString="{0:yyyy/MM/dd HH:mm}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="OrderDetail.aspx?ID=<%# Eval("OrderID")%>">
                                    <img src="/Images/icons-search.png" />
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
                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="Manager_OrderList.aspx" />
                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        沒有訂單紀錄...
                    </p>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
