using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace expense_app
{
    public partial class home : System.Web.UI.Page
    {

        protected void UpdateSummary()
        {
            int id = int.Parse(Session["id"].ToString());
            DateTime today = DateTime.Now.Date;

            ExpenseEntities db = new ExpenseEntities();
            var todayExpensesValue = db.Transactions.Where(t => t.userId == id && t.TransactionType == "Expense" && EntityFunctions.TruncateTime(t.Date) == EntityFunctions.TruncateTime(today)).Sum(t => (decimal?)t.Amount) ?? 0;

            var todayDepositValue = db.Transactions.Where(t => t.userId == id && t.TransactionType == "Deposit" && EntityFunctions.TruncateTime(t.Date) == EntityFunctions.TruncateTime(today)).Sum(t => (decimal?)t.Amount) ?? 0;

            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var monthExpensesValue = db.Transactions
            .Where(t => t.userId == id && t.TransactionType == "Expense" && EntityFunctions.TruncateTime(t.Date) >= firstDayOfMonth && EntityFunctions.TruncateTime(t.Date) <= lastDayOfMonth)
            .Sum(t => (decimal?)t.Amount) ?? 0;

            var monthDepositValue = db.Transactions
           .Where(t => t.userId == id && t.TransactionType == "Deposit" && EntityFunctions.TruncateTime(t.Date) >= firstDayOfMonth && EntityFunctions.TruncateTime(t.Date) <= lastDayOfMonth)
           .Sum(t => (decimal?)t.Amount) ?? 0;

            todayExpense.Text = $"₹ {todayExpensesValue}";
            todayDeposit.Text = $"₹ {todayDepositValue}";
            monthlyExpense.Text = $"₹ {monthExpensesValue}";
            monthlyDeposit.Text = $"₹ {monthDepositValue}";
        }

        protected void UpdateList()
        {
            int month = int.Parse(monthpicker.SelectedValue);
            int year = int.Parse(yearpicker.SelectedValue);

            DateTime sdate =new DateTime(year, month, 1);
            DateTime edate = sdate.AddMonths(1).AddDays(-1);
            int id = int.Parse(Session["id"].ToString());
            ExpenseEntities db = new ExpenseEntities();

            var expenses = db.Transactions.Where(t => t.userId==id && EntityFunctions.TruncateTime(t.Date) <= edate && EntityFunctions.TruncateTime(t.Date) >= sdate).Select(item=>new
            {
                ID = item.Id,
                Amount = item.Amount,
                TransactionType = item.TransactionType,
                Category = item.Category,
                Date = item.Date,
                Description = item.Description ==null  ? "Not found":item.Description
            }).ToList();
            expenceList.DataSource = expenses;
            expenceList.DataBind();
            if(expenses.Count() == 0)
            {
                empty.Text = "No Transaction Found in this Month";
            }
            else
            {
                empty.Text = "";
            }
        }

        private void BindChartData()
        {
            ExpenseEntities db = new ExpenseEntities();
            List<int> imonths = Enumerable.Range(1, 12).ToList();
            List<decimal> expenses = new List<decimal>();
            List<decimal> deposits = new List<decimal>();
            int currentYear = DateTime.Now.Year;
            int id = int.Parse(Session["id"].ToString());
            foreach (int month in imonths)
            {
                DateTime sdate = new DateTime(currentYear, month, 1);
                DateTime edate = sdate.AddMonths(1).AddDays(-1);
                decimal texpense = db.Transactions.Where(t => t.userId==id && EntityFunctions.TruncateTime(t.Date) <= edate && EntityFunctions.TruncateTime(t.Date) >= sdate && t.TransactionType == "Expense").Sum(item => (decimal?)item.Amount) ?? 0;
                decimal tdeposit = db.Transactions.Where(t => t.userId==id && EntityFunctions.TruncateTime(t.Date) <= edate && EntityFunctions.TruncateTime(t.Date) >= sdate && t.TransactionType == "Deposit").Sum(item => (decimal?)item.Amount) ?? 0;
                expenses.Add(texpense);
                deposits.Add(tdeposit);
            }
            expenseDepositChart.Series["Expenses"].Points.DataBindY(expenses);
            expenseDepositChart.Series["Deposits"].Points.DataBindY(deposits);

            List<string> months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            expenseDepositChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Clear();
            for (int i = 0; i < months.Count; i++)
            {
                expenseDepositChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(i + 0.5, i + 1.5, months[i]);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) 
            { 
                Response.Redirect("/login.aspx");
            }
            if (!IsPostBack)
            {
                int currentYear = DateTime.Now.Year;
                for (int year = 2000; year <= currentYear; year++)
                {
                    yearpicker.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                yearpicker.SelectedValue = currentYear.ToString();
                int curentMonth = DateTime.Now.Month;
                monthpicker.SelectedValue = curentMonth.ToString();
                UpdateList();
            }
            BindChartData();
            UpdateSummary();
        }

        protected void ExpenseUpdate(object sender, GridViewUpdatedEventArgs e)
        {
            UpdateSummary();
        }protected void ExpenseDelete(object sender, GridViewDeletedEventArgs e)
        {
            UpdateSummary();
        }

        protected void monthpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        protected void yearpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void export_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "expense-" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            expenceList.GridLines = GridLines.Both;
            expenceList.HeaderStyle.Font.Bold = true;
            expenceList.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void expenceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int recordId = Convert.ToInt32(e.CommandArgument);

                ExpenseEntities db = new ExpenseEntities();
                var item = db.Transactions.Find(recordId);
                if (item != null)
                {
                    db.Transactions.Remove(item);
                    db.SaveChanges();
                }
                UpdateList();
                BindChartData();
            }
        }
    }
    
}