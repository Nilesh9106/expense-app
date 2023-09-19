<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="expense_app.login" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="loginHeader" ContentPlaceHolderID="head" runat="server">
    <title>Expense Tracker | Login</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContent" ID="loginPage" runat="server">
    <div class="my-5 table-responsive">
        
        <table align="center" class="w-100 table table-hover">
            <tr>
                <td>
                    <asp:Label ID="usernameLabel" runat="server" AssociatedControlID="usernameInput" CssClass="form-label" Text="Username"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="usernameInput" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="usernameInput" ErrorMessage="Username is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="passwordLabel" runat="server" AssociatedControlID="passwordInput" CssClass="form-label" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="passwordInput" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="passwordInput" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="signup.aspx">Don't have an account?</a>
                </td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-dark w-100" OnClick="btnLogin_Click" Text="Login" />
                </td>
                <td>
                    <asp:Label ID="errLabel" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>