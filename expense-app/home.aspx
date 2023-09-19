<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" EnableEventValidation="false" Inherits="expense_app.home" MasterPageFile="~/Site1.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="homeHeader" ContentPlaceHolderID="head" runat="server">
    <title>Expense Tracker | Home</title>
</asp:Content>

<asp:Content ID="homePage" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6 py-3">
                <div class="card">
                    <div class="card-header bg-dark text-white">
                        Today's Summary
                    </div>
                    <div class="card-body">
                        <h5 class="card-title text-danger">Today's Expenses</h5>
                        <asp:Label CssClass="card-text text-danger mb-3" ID="todayExpense" runat="server"></asp:Label>
                        <h5 class="card-title text-success mt-3">Today's Deposits</h5>
                        <asp:Label CssClass="card-text text-success" ID="todayDeposit" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-6 py-3">
                <div class="card">
                    <div class="card-header bg-dark text-white">
                        Monthly Summary
                    </div>
                    <div class="card-body">
                        <h5 class="card-title text-danger">Month's Expenses</h5>
                        <asp:Label CssClass="card-text text-danger" ID="monthlyExpense" runat="server"></asp:Label>
                        <h5 class="card-title text-success mt-3">Month's Deposits</h5>
                        <asp:Label CssClass="card-text text-success" ID="monthlyDeposit" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive container mt-2">
        <div class="row gap-2 w-75 mx-2 my-3">
            <asp:DropDownList CssClass="form-control col" ID="monthpicker" runat="server" AutoPostBack="True" OnSelectedIndexChanged="monthpicker_SelectedIndexChanged">
                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList CssClass="form-control col" ID="yearpicker" runat="server" AutoPostBack="true" OnSelectedIndexChanged="yearpicker_SelectedIndexChanged"></asp:DropDownList>
            <asp:Button ID="export" CssClass="btn btn-success col" runat="server" Text="Export to Excel" OnClick="export_Click" />
        </div>
        <asp:Label ID="empty" CssClass="text-center fs-4 mx-2" runat="server" Text=""></asp:Label>
        <asp:GridView CssClass="table" ID="expenceList" runat="server" OnRowCommand="expenceList_RowCommand">
            <Columns>

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button CssClass="btn btn-sm btn-outline-danger" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
    <div class="my-2 container d-flex justify-content-center align-items-center">
        <asp:Chart ID="expenseDepositChart" Width="800px" Height="400px" runat="server">
            <Titles>
                <asp:Title Text="Monthly Expenses and Deposits of current year" Font="18pt" />
            </Titles>
            <Legends>
                <asp:Legend Name="DefaultLegend" Alignment="Center" Docking="Bottom" />
            </Legends>
            <Series>
                <asp:Series Name="Expenses" IsValueShownAsLabel="true" LabelBackColor="White" IsXValueIndexed="true" YValuesPerPoint="6">
                </asp:Series>
                <asp:Series Name="Deposits" IsValueShownAsLabel="true" LabelBackColor="White" IsXValueIndexed="true" XValueType="Double" YValuesPerPoint="6">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisX IsLabelAutoFit="True">
                        <MajorGrid Enabled="false" />
                    </AxisX>
                    <AxisY IsLabelAutoFit="True">
                        <MajorGrid Enabled="false" />
                    </AxisY>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>
