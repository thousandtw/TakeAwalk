<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="TakeAwalk.SystemAdmin.OrderList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>購票紀錄</h2>
                <br />
                起始:<asp:TextBox ID="txbStr" runat="server" TextMode="Date"></asp:TextBox>
                結束:<asp:TextBox ID="txbEnd" runat="server" TextMode="Date"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" /><br />
                <asp:GridView ID="gv_orderlist" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" CellSpacing="2">
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="訂單序號" />
                        <asp:BoundField DataField="CreateDate" HeaderText="購買日期" DataFormatString="{0:yyyy/MM/dd HH:mm}" />
                        <asp:BoundField HeaderText="總金額" DataField="Total" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <label>元</label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="總張數" DataField="Quantity" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <label>張</label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrderStatus" HeaderText="訂單狀態" DataFormatString="{0:'已完成'}"/>
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
                <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="OrderList.aspx" />
                <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        沒有購買紀錄...
                    </p>
                </asp:PlaceHolder>
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
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
