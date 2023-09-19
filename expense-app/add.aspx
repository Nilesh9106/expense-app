<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="expense_app.add" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="addHead" ContentPlaceHolderID="head" runat="server">
    <title>Add Transaction</title>
</asp:Content>

<asp:Content ID="addContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="container my-5">
        <h1>Add Transaction</h1>
        <div class="row mb-3 mt-4">
            <div class="col">
                <input type="number" class="form-control" id="amount" placeholder="Amount" runat="server">
            </div>

            <div class="col">
                <asp:DropDownList ID="Category" runat="server" CssClass="form-control">
                    
                </asp:DropDownList>
            </div>
            <div class="col">
                <asp:DropDownList ID="transactionType" runat="server" CssClass="form-control" OnSelectedIndexChanged="transactionType_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>Expense</asp:ListItem>
                    <asp:ListItem>Deposit</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="my-3">
            <textarea id="description" cols="20" rows="2" runat="server" class="form-control" placeholder="Description"></textarea>
        </div>
        <asp:Button CssClass="btn btn-dark w-100" ID="submit" runat="server" Text="Submit" OnClick="submit_Click" />
    </div>
</asp:Content>
