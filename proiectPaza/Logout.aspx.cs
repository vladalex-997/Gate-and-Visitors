using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace proiectPaza
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("Login.aspx");
                Response.Cookies["dateutilizator"].Expires = DateTime.Now.AddDays(-1D);
                Session.Abandon();
                Session.Clear();
                Response.Cookies.Clear();
                FormsAuthentication.SignOut();
                HttpCookie cooku = new HttpCookie("dateutilizator");
                // IsPersistent = false;
                cooku.Expires = DateTime.Now.AddDays(1);
                cooku.Value = "";
                Response.Cookies.Add(cooku);

                Response.Redirect("~/Default.aspx");

                //SiteMaster s = new SiteMaster();

                //FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception)
            {
                string script = "alert(\"Logout nereusit!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }


        }
    }
}