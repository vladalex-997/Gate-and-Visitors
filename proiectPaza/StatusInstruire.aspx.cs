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
    public partial class StatusInstruire : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!Page.IsPostBack)
                {
                    List<int> numberList = new List<int>();
                    for (int i = 0; i <= 365; i++)
                    {
                        numberList.Add(i);
                    }

                    DropDownListTimpInstruire.DataSource = numberList;
                    DropDownListTimpInstruire.DataBind();


                }
            }
            catch (Exception)
            {

            }


            ReloadPagina();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadPagina();
            string script = "alert(\"Reincarcare pagina reusita!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        public void ReloadPagina()
        {
            try
            {
                Database databaseObject = new Database();
                databaseObject.OpenConnection();

                DateTime no = DateTime.Now;
                DateTime af;

                af = no.AddDays(7);

                string query = "SELECT IdRecord,Nume,Act,OraProgramata,DataInstruire,StatusInstruire,InstruitDe,Companie from Inregistrari " +
                    "WHERE Companie!='"+"' AND OraProgramata BETWEEN @dataince AND @datasfar order by OraProgramata";



                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

            myquerytab.Parameters.AddWithValue("@dataince", no);
            myquerytab.Parameters.AddWithValue("@datasfar", af);

            SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                databaseObject.CloseConnection();


            }
            catch
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }


        protected void btnToate_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                databaseObject.OpenConnection();

                DateTime no = DateTime.Now;
                DateTime af;

                af = no.AddDays(7);

                string query = "SELECT IdRecord,Nume,Act,OraProgramata,DataInstruire,StatusInstruire,InstruitDe,Companie from Inregistrari WHERE Companie!='"+"' order by OraProgramata";

                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                databaseObject.CloseConnection();

                string script = "alert(\"Toate Inregistrarile s-au afisat!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);


            }
            catch
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnInstruire_Click(object sender, EventArgs e)
        {
            try
            {
                string SerieBuletin = tbxSerieBuletin.Text;
                string Nume = tbxNume.Text;
              //  string Companie = tbxCompanie.Text;
                string Nrzilestring = DropDownListTimpInstruire.SelectedValue;
                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;
                string StatusInstruire = "Valabil";

                if (string.IsNullOrEmpty(SerieBuletin) || string.IsNullOrEmpty(Nume) || Nrzilestring == "0")
                {
                    string script = "alert(\"Completati corect toate campurile cu *!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    DateTime DataInstruire = DateTime.Now;

                    DateTime nrnou;

                    double nrzile = double.Parse(Nrzilestring);

                    nrnou = DataInstruire.AddDays(nrzile);

                    Database databaseObject = new Database();

                    string query = "UPDATE Inregistrari SET InstruitDe=@InstruitDe, StatusInstruire=@StatusInstruire, DataInstruire=@DataInstruire " +
                        "WHERE Act=@Act AND Nume=@Nume";

                    SqlCommand myCommand = new SqlCommand(query, databaseObject.myConnection);

                    myCommand.Parameters.AddWithValue("@InstruitDe", tempnume);
                    myCommand.Parameters.AddWithValue("@StatusInstruire", StatusInstruire);
                    myCommand.Parameters.AddWithValue("@DataInstruire", nrnou);
                    myCommand.Parameters.AddWithValue("@Act", SerieBuletin);
                    myCommand.Parameters.AddWithValue("@Nume", Nume);
                  //  myCommand.Parameters.AddWithValue("@Companie", Companie);

                    databaseObject.OpenConnection();

                    var resul = myCommand.ExecuteNonQuery();

                    databaseObject.CloseConnection();

                    if (resul != 0)
                    {
                        string script = "alert(\"Instruire Reusita!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                        tbxNume.Text = "";
                      //  tbxCompanie.Text = "";
                        tbxSerieBuletin.Text = "";
                        DropDownListTimpInstruire.SelectedIndex = -1;
                        ReloadPagina();
                    }
                    else
                    {
                        string script = "alert(\"Nu exista inregistrare cu aceste date!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }



                }


            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Instruire!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[5].Text).ToUpper() == "VALABIL")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[5].Text).ToUpper() == "EXPIRAT")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[5].Text).ToUpper() == "NEINSTRUIT")
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}