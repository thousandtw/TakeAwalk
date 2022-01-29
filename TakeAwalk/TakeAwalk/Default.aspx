<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TakeAwalk.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gx-5 align-items-center justify-content-center" style="background-color: lightgray">
        <div class="col-lg-8 col-xl-7 col-xxl-6">
            <div class="my-5 text-center text-xl-start">
                <h2>熱門優惠票</h2>
                <br />
                <asp:GridView ID="gdvTicket" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField HeaderText="票券名稱" DataField="TicketName" />
                        <asp:BoundField HeaderText="主辦單位" DataField="TrainCompany" />
                        <asp:BoundField HeaderText="售票起始日" DataField="ActivityStartDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="售票截止日" DataField="ActivityEndDate" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="票價" DataField="Price" />
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
            </div>
        </div>
        <div class="col-xl-5 col-xxl-6 d-none d-xl-block text-center">
            <img class="img-fluid rounded-3 my-5" src="Images/HornbyHarryPotter.jpg" alt="..." />
        </div>
    </div>
</asp:Content>
