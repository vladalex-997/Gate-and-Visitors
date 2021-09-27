using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;

namespace proiectPaza
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            menuusercurent.Visible = false;
            if (Session["login"] != null)
            {
                menuusercurent.Visible = true;
                menulogin.Visible = false;
                menulogout.Visible = true;
                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;

               
                string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AccesPaza.txt");
                string[] files1 = File.ReadAllLines(path1);

                string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AccesSSM.txt");
                string[] files2 = File.ReadAllLines(path2);


                if (files2.Contains(tempnume))
                {
                    menuprogramaricont.Visible = false;
                    menuprogramare.Visible = true;
                    menuvizualizare.Visible = true;
                    menuusercurent.InnerText = "Bun venit, " + tempnume;
                 //   menuinstruire.Visible = true;
                    menustatusinstruire.Visible = true;

                }
                else if(files1.Contains(tempnume))
                {
                    menuprogramaricont.Visible = false;
                    menuprogramare.Visible = true;
                    menuvizualizare.Visible = true;
                    menuusercurent.InnerText = "Bun venit, " + tempnume;
                   // menuinstruire.Visible = false;
                    menustatusinstruire.Visible = false;
                }
                else
                {
                    menuprogramaricont.Visible = true;
                    menuprogramare.Visible = true;
                    menuvizualizare.Visible = false;
                    menuusercurent.InnerText = "Bun venit, " + tempnume;
                  //  menuinstruire.Visible = false;
                    menustatusinstruire.Visible = false;
                }
            }
            else
            {
                menuprogramaricont.Visible = false;
                menuusercurent.Visible = false;
                menulogin.Visible = true;
                menulogout.Visible = false;
                menuprogramare.Visible = false;
                menuvizualizare.Visible = false;
               // menuinstruire.Visible = false;
                menustatusinstruire.Visible = false;
            }
        }
    }
}