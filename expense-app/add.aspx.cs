using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace expense_app
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]  == null)
            {
                Response.Redirect("/login.aspx");
            }
            if (!IsPostBack)
            {
                Category.Items.Clear();
                Category.Items.Add("Food");
                Category.Items.Add("Rent ");
                Category.Items.Add("Travel ");
                Category.Items.Add("Deposit ");
                Category.Items.Add("Entertainment ");
                Category.Items.Add("Shopping ");
                Category.Items.Add("Health ");
                Category.Items.Add("Debt ");
                Category.Items.Add("Miscellaneous ");
            }
            
        }

        protected void transactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (transactionType.SelectedValue == "Deposit") {      
                Category.Items.Clear();
                Category.Items.Add("Salary");
                Category.Items.Add("Loan");
                Category.Items.Add("Rental Income");
                Category.Items.Add("Savings");
                Category.Items.Add("Gift");
                Category.Items.Add("Other");
            }
            else
            {
                Category.Items.Clear();
                Category.Items.Add("Food");
                Category.Items.Add("Rent ");
                Category.Items.Add("Travel ");
                Category.Items.Add("Deposit ");
                Category.Items.Add("Entertainment ");
                Category.Items.Add("Shopping ");
                Category.Items.Add("Health ");
                Category.Items.Add("Debt ");
                Category.Items.Add("Miscellaneous ");
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            ExpenseEntities db = new ExpenseEntities();
            int ammount = int.Parse(amount.Value);
            DateTime dateTime = DateTime.Now;
            string category = Category.Text;
            string type = transactionType.Text;
            string desc = description.InnerText;

            Transaction ob = new Transaction();
            ob.userId = int.Parse(Session["id"].ToString());
            ob.Amount = ammount;
            ob.Date = dateTime;
            ob.Category = category;
            ob.TransactionType = type;
            ob.Description = desc;
            db.Transactions.Add(ob);
            db.SaveChanges();
            Response.Redirect("/home.aspx");
            
        }
    }
}