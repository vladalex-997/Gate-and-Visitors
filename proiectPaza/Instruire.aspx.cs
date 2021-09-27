using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace proiectPaza
{
    public partial class Instruire : System.Web.UI.Page
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
                string Companie = tbxCompanie.Text;
                string Nrzilestring = DropDownListTimpInstruire.SelectedValue;
                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;
                string StatusInstruire = "Valabil";

                if (string.IsNullOrEmpty(SerieBuletin) || string.IsNullOrEmpty(Nume) || string.IsNullOrEmpty(Companie) || Nrzilestring == "0")
                {
                    string script = "alert(\"Completati corect toate campurile cu *!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    DateTime DataInstruire = DateTime.Now;

                    DateTime nrnou;

                    double nrzile = double.Parse(Nrzilestring);

                    nrnou= DataInstruire.AddDays(nrzile);

                    Database databaseObject = new Database();

                    string query = "UPDATE Inregistrari SET InstruitDe=@InstruitDe, StatusInstruire=@StatusInstruire, DataInstruire=@DataInstruire " +
                        "WHERE Act=@Act AND Nume=@Nume AND Companie=@Companie";

                    SqlCommand myCommand = new SqlCommand(query, databaseObject.myConnection);

                    myCommand.Parameters.AddWithValue("@InstruitDe",tempnume);
                    myCommand.Parameters.AddWithValue("@StatusInstruire", StatusInstruire);
                    myCommand.Parameters.AddWithValue("@DataInstruire", nrnou);
                    myCommand.Parameters.AddWithValue("@Act", SerieBuletin);
                    myCommand.Parameters.AddWithValue("@Nume", Nume);
                    myCommand.Parameters.AddWithValue("@Companie", Companie);

                    databaseObject.OpenConnection();

                    var resul = myCommand.ExecuteNonQuery();

                    databaseObject.CloseConnection();

                if (resul != 0)
                {
                    string script = "alert(\"Instruire Reusita!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    tbxNume.Text = "";
                    tbxCompanie.Text = "";
                    tbxSerieBuletin.Text = "";
                    DropDownListTimpInstruire.SelectedIndex = -1;
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
    }
}