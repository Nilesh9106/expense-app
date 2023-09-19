using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace expense_app
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Response.Redirect("/home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String username = usernameInput.Text;
            String password = passwordInput.Text;

            ExpenseEntities db =new ExpenseEntities();
            try
            {
                User user = db.Users.Where(s => s.username == username).FirstOrDefault<User>();
                if(user == null)
                {
                    errLabel.Text = "User not found!!";
                }
                else
                {
                    if (user.password == password)
                    {
                        Session["user"] = user.username;
                        Session["id"] = user.Id;

                        Response.Redirect("/home.aspx");
                    }
                    else
                    {
                        errLabel.Text = "Password does not match!!";
                    }
                }
            }
            catch(InvalidOperationException ex)
            {
                errLabel.Text = "User not found!!";
            }
        }
    }
}