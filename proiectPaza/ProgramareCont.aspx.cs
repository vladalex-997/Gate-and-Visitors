using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace proiectPaza
{
    public partial class ProgramareCont : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;

                Database databaseObject = new Database();
                databaseObject.OpenConnection();

                string query = "SELECT IdRecord,Nume,Act,OraProgramata,DataExpirare,StatusPersoana,Cartela,NumarMasina,Companie from Inregistrari " +
                    "WHERE ProgramatDe='" + tempnume + "' order by OraProgramata";

                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();


                databaseObject.CloseConnection();
            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnFiltrare_Click(object sender, EventArgs e)
        {
            try
            {
                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;

                Database databaseObject = new Database();

                string NumeProgramat = tbxNumeProgramat.Text;

                var v1 = (NumeProgramat == "");
                var v2 = (NumeProgramat != "");

                string queryfin = "";

                if (v1)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,DataExpirare,StatusPersoana,Cartela,NumarMasina,Companie from Inregistrari " +
                   "WHERE ProgramatDe='" + tempnume + "' order by OraProgramata";
                }
                else if (v2)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,DataExpirare,StatusPersoana,Cartela,NumarMasina,Companie from Inregistrari " +
                  "WHERE ProgramatDe='" + tempnume + "' AND " +
                  "Nume LIKE '%" + NumeProgramat + "%' order by OraProgramata";
                }
                else
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,DataExpirare,StatusPersoana,Cartela,NumarMasina,Companie from Inregistrari " +
                  "WHERE ProgramatDe='" + tempnume + "' order by OraProgramata";
                }

                databaseObject.OpenConnection();

                SqlCommand myquerytab = new SqlCommand(queryfin, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                string script = "alert(\"Filtrare reusita!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                databaseObject.CloseConnection();

            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Filtrare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}