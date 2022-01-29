<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="Manager_TicketList.aspx.cs" Inherits="TakeAwalk.SystemAdmin.Manager_TicketList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>優惠票管理清單</h2>
                <br />
                <asp:Button ID="NewTicket_btn" runat="server" Text="新增優惠票" align="right" OnClick="NewTicket_btn_Click" /><br />
                <asp:GridView ID="gv_ticketslist" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:BoundField DataField="TicketID" HeaderText="票券編號" />
                        <asp:BoundField DataField="TicketName" HeaderText="優惠票名稱" />
                        <asp:BoundField DataField="TrainCompany" HeaderText="主辦單位" />
                        <asp:BoundField DataField="ActivityStartDate" HeaderText="活動開始日期" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ActivityEndDate" HeaderText="活動結束日期" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Price" HeaderText="定價" />
                        <asp:BoundField DataField="Stocks" HeaderText="庫存" />
                        <asp:TemplateField HeaderText="上架狀態">
                            <ItemTemplate>
                                <%# Boolean.Parse(Eval("IsEnabled").ToString()) ? "上架中" : "已下架" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="Manager_ChangeTicket.aspx?ID=<%# Eval("TicketID")%>">
                                    <img src="/Images/icons-search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="Manager_TicketList.aspx"/>
                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        沒有訂單紀錄...
                    </p>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
