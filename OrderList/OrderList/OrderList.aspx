<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="OrderList.OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlList" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblTitle" runat="server" Text="Order List"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Order_Id" HeaderText="Order Id" />
                                        <asp:TemplateField HeaderText="Order Item">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn" runat="server" Text='<%#Eval("Order_Item").ToString() %>' OnClick="lbtn_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Price" HeaderText="Price" />
                                        <asp:BoundField DataField="Cost" HeaderText="Cost" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDetail" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTitle2" runat="server" Text="Detail"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Order ID
                            </td>
                            <td>
                                <asp:Label ID="lblOrderId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Order Item
                            </td>
                            <td>
                                <asp:Label ID="lblOrderItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price
                            </td>
                            <td>
                                <asp:Label ID="lblPrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cost
                            </td>
                            <td>
                                <asp:Label ID="lblCost" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
