using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace expense_app
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String username = usernameInput.Text;
            String email = emailInput.Text;
            String password = passwordInput.Text;

            ExpenseEntities db = new ExpenseEntities();
            try
            {
                User user = db.Users.Where(s => s.username == username).FirstOrDefault<User>();
                if (user != null)
                {
                    errLabel.Text = "Username already exists!!";
                    return;
                }
                user = db.Users.Where(s => s.email == email).FirstOrDefault<User>();
                if (user != null)
                {
                    errLabel.Text = "Email already exists!!";
                    return;
                }
                user = new User();
                user.username = username;
                user.email = email;
                user.password = password;
                user= db.Users.Add(user);
                db.SaveChanges();

                Session["user"] = user.username;
                Session["id"] = user.Id;

                Response.Redirect("/home.aspx");
                
            }
            catch (InvalidOperationException ex)
            {
                errLabel.Text = "User not found!!";
            }
        }
    }
}