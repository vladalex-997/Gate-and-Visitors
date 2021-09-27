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
    public partial class Vizualizare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                ReloadPagina();

                

            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

        }

        public void ReloadPagina()
        {
            try
            {
                Database databaseObject = new Database();
                databaseObject.OpenConnection();

                string query = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari WHERE StatusPersoana='"+"Deschis"+"' OR StatusPersoana='"+"Programat"+"' order by OraProgramata";

                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                databaseObject.CloseConnection();


              //  if (!Page.IsPostBack)
              //  {
                   // Database databaseObject = new Database();
              //      databaseObject.OpenConnection();
              //      string StatusCartela = "Liber";

              //      string queryfill = "SELECT NumarCartela from Cartela WHERE StatusCartela='" + StatusCartela + "' order by NumarCartela";
              //      SqlCommand myCommand = new SqlCommand(queryfill, databaseObject.myConnection);

              //      SqlDataAdapter da = new SqlDataAdapter(myCommand);
              //      DataSet dsfill = new DataSet();
              //      da.Fill(dsfill);

              //      DropDownListcartela.DataSource = dsfill.Tables[0];
              //      DropDownListcartela.DataTextField = dsfill.Tables[0].Columns["NumarCartela"].ToString();
              //      DropDownListcartela.DataValueField = dsfill.Tables[0].Columns["NumarCartela"].ToString();

              //      DropDownListcartela.DataBind();


              //      databaseObject.CloseConnection();
              ////  }
            }
            catch
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnIntrare_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();

                if(int.TryParse(tbxNumarProgramare.Text, out int docu))
                {
                    int NumarProgramare = int.Parse(tbxNumarProgramare.Text);



                    string queryselectcartela = "SELECT NumarCartela from Cartela WHERE StatusCartela='"+"Liber"+"'";

                    SqlCommand mycommandselectcartela = new SqlCommand(queryselectcartela, databaseObject.myConnection);

                    try
                    {
                        databaseObject.OpenConnection();
                        var Cartelaobj = mycommandselectcartela.ExecuteScalar();
                        databaseObject.CloseConnection();

                        if(Cartelaobj is null)
                        {
                            string script = "alert(\"Nu mai exista cartele libere!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                        else
                        {
                            string Cartela = Cartelaobj.ToString();


                                DateTime timpcurent = DateTime.Now;
                                string StatusPersoana = "Deschis";
                                string queryupdate = "UPDATE Inregistrari SET OraIntrare=@OraIntrare,StatusPersoana=@StatusPersoana,Cartela=@Cartela WHERE IdRecord=@IdRecord";

                                SqlCommand mycommandupdate = new SqlCommand(queryupdate, databaseObject.myConnection);

                                mycommandupdate.Parameters.AddWithValue("@OraIntrare", timpcurent);
                                mycommandupdate.Parameters.AddWithValue("@StatusPersoana", StatusPersoana);
                                mycommandupdate.Parameters.AddWithValue("@Cartela", Cartela);
                                mycommandupdate.Parameters.AddWithValue("@IdRecord", NumarProgramare);

                                databaseObject.OpenConnection();
                                var result = mycommandupdate.ExecuteNonQuery();
                                databaseObject.CloseConnection();

                                if (result != 0)
                                {


                                    string StatusCartela = "Ocupat";

                                    string queryupdatecartela = "UPDATE Cartela SET StatusCartela=@StatusCartela WHERE NumarCartela=@NumarCartela";

                                    SqlCommand mycommandcartela = new SqlCommand(queryupdatecartela, databaseObject.myConnection);

                                    mycommandcartela.Parameters.AddWithValue("@StatusCartela", StatusCartela);
                                    mycommandcartela.Parameters.AddWithValue("@NumarCartela", Cartela);

                                    databaseObject.OpenConnection();
                                    var resultcartela = mycommandcartela.ExecuteNonQuery();
                                    databaseObject.CloseConnection();


                                    string script = "alert(\"Intrare Reusita!\");";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                                    tbxNumarProgramare.Text = "";

                                    atribuirecartela.Text = " Numarului: " + NumarProgramare.ToString() + "    este: " + Cartela;


                                    ReloadPagina();
                                }
                                else
                                {
                                    string script = "alert(\"Nu exista acest Numar Programare!\");";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                }

                            





                        }

                        
                    }
                    catch (Exception)
                    {
                        string script = "alert(\"Nu mai exista cartele libere!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                    

       
                }
                else
                {
                    string script = "alert(\"Numar Programare trebuie sa fie intreg!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }

            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Intrare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnFiltrare_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                string Nume = tbxNume.Text;
                string Status = tbxStatus.Text;
                string queryfin = "";

                var v1 = (Nume == "" && Status == "");
                var v2 = (Nume == "" && Status != "");
                var v3 = (Nume != "" && Status == "");
                var v4 = (Nume != "" && Status != "");

                if (v1)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari WHERE StatusPersoana='" + "Deschis" + "' OR StatusPersoana='" + "Programat" + "' order by OraProgramata";
                }
                else if (v2)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari " +
                        "WHERE StatusPersoana LIKE '%" + Status + "%' order by OraProgramata";

                }
                else if (v3)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari " +
                        "WHERE Nume LIKE '%" + Nume + "%' order by OraProgramata";
                }
                else if (v4)
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari " +
                        "WHERE StatusPersoana LIKE '%" + Status + "%' AND " +
                        "Nume LIKE '%" + Nume + "%' order by OraProgramata";
                }
                else
                {
                    queryfin = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari WHERE StatusPersoana='" + "Deschis" + "' OR StatusPersoana='" + "Programat" + "' order by OraProgramata";
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
                string script = "alert(\"Eroare filtrare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnToate_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                databaseObject.OpenConnection();

                string query = "SELECT IdRecord,Nume,Act,OraProgramata,StatusPersoana,Cartela,DataExpirare,NumarMasina,Companie,ProgramatDe,StatusInstruire from Inregistrari order by OraProgramata";

                SqlCommand myquerytab = new SqlCommand(query, databaseObject.myConnection);

                SqlDataAdapter daquery = new SqlDataAdapter(myquerytab);
                DataTable dttab = new DataTable();
                DataSet ds = new DataSet();
                daquery.Fill(dttab);
                daquery.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                string script = "alert(\"Toate inregistrarile s-au afisat!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                databaseObject.CloseConnection();
            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Incarcare toate rezultatele!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnIesire_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();

                if(int.TryParse(tbxNumarIesire.Text,out int docu))
                {
                    int NumarIesire = int.Parse(tbxNumarIesire.Text);

                    if(int.TryParse(tbxCartelaIesire.Text,out int cart))
                    {
                        int NumarCartelaIesire = int.Parse(tbxCartelaIesire.Text);
                       
                        string queryselectcartela = "SELECT NumarCartela from Cartela WHERE NumarCartela=" + NumarCartelaIesire + "";

                        SqlCommand mycommandselectcartela = new SqlCommand(queryselectcartela, databaseObject.myConnection);

                        databaseObject.OpenConnection();
                        var Cartelaobj = mycommandselectcartela.ExecuteScalar();
                        databaseObject.CloseConnection();

                        if(Cartelaobj is null)
                        {
                            string script = "alert(\"Nu exista cartela cu acest numar!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                        else
                        {

                            string Cartela = "Niciuna";

                            string CartelaNr = Cartelaobj.ToString();


                            //aici mai trebuie un select sa vedem nr cartelei si retinut in variabila

                            string queryb = "SELECT Cartela from Inregistrari WHERE IdRecord="+NumarIesire+"";

                            SqlCommand mycommandb = new SqlCommand(queryb, databaseObject.myConnection);

                            databaseObject.OpenConnection();
                            var resultcheck = mycommandb.ExecuteScalar();
                            databaseObject.CloseConnection();

                            if(resultcheck is null)
                            {
                                string script = "alert(\"Nu exista programare cu acest numar!\");";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                            }
                            else
                            {
                                if(int.TryParse(resultcheck.ToString(),out int qb))
                                {
                                    int tempcartelanr = int.Parse(resultcheck.ToString()); //stocat nr cartela de la inregistrarea corecta

                                    if(NumarCartelaIesire!=tempcartelanr)
                                    {
                                        string script = "alert(\"Cartela introdusa nu corespunde acestei inregistrari!\");";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                    }
                                    else
                                    {
                                        DateTime timpcurent = DateTime.Now;
                                        string StatusPersoana = "Inchis";
                                        string queryupdate = "UPDATE Inregistrari SET OraIesire=@OraIesire,StatusPersoana=@StatusPersoana,Cartela=@Cartela WHERE IdRecord=@IdRecord";

                                        SqlCommand mycommandupdate = new SqlCommand(queryupdate, databaseObject.myConnection);

                                        mycommandupdate.Parameters.AddWithValue("@OraIesire", timpcurent);
                                        mycommandupdate.Parameters.AddWithValue("@StatusPersoana", StatusPersoana);
                                        mycommandupdate.Parameters.AddWithValue("@Cartela", Cartela);
                                        mycommandupdate.Parameters.AddWithValue("@IdRecord", NumarIesire);

                                        databaseObject.OpenConnection();
                                        var result = mycommandupdate.ExecuteNonQuery();
                                        databaseObject.CloseConnection();

                                        if (result != 0)
                                        {


                                            string StatusCartela = "Liber";

                                            string queryupdatecartela = "UPDATE Cartela SET StatusCartela=@StatusCartela WHERE NumarCartela=@NumarCartela";

                                            SqlCommand mycommandcartela = new SqlCommand(queryupdatecartela, databaseObject.myConnection);

                                            mycommandcartela.Parameters.AddWithValue("@StatusCartela", StatusCartela);
                                            mycommandcartela.Parameters.AddWithValue("@NumarCartela", CartelaNr);

                                            databaseObject.OpenConnection();
                                            var resultcartela = mycommandcartela.ExecuteNonQuery();
                                            databaseObject.CloseConnection();


                                            string script = "alert(\"Iesire Reusita!\");";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                                            tbxNumarIesire.Text = "";
                                            tbxCartelaIesire.Text = "";

                                            ReloadPagina();
                                        }
                                        else
                                        {
                                            string script = "alert(\"Nu exista acest Numar Programare!\");";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                        }
                                    }

                        

                                }
                                else
                                {
                                    string script = "alert(\"Aceasta programare nu are cartela alocata!\");";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                }
                            }




                        }

                    }
                    else
                    {
                        string script = "alert(\"Numar Cartela Iesire trebuie sa fie intreg!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }

                }
                else
                {
                    string script = "alert(\"Numar Iesire trebuie sa fie intreg!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }

            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Iesire!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnNeprezentare_Click(object sender, EventArgs e)
        {
            try
            {
                Database databaseObject = new Database();
                if(int.TryParse(tbxNeprezentare.Text,out int docu))
                {

                   
                    int Neprezentare = int.Parse(tbxNeprezentare.Text);

                    //select verifica daca e deschis

                    string StatusCheck = "Programat";

                    string check = "SELECT StatusPersoana from Inregistrari WHERE IdRecord="+Neprezentare+"";

                    SqlCommand mycommandcheck = new SqlCommand(check, databaseObject.myConnection);

                    databaseObject.OpenConnection();

                    var ObjCheck = mycommandcheck.ExecuteScalar();

                    databaseObject.CloseConnection();

                    if(ObjCheck is null)
                    {
                        string script = "alert(\"Nu exista programare cu acest numar!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                    else
                    {
                        string StatusSelect = ObjCheck.ToString();

                        if (StatusCheck == StatusSelect)
                        {


                            string StatusPersoana = "Inchis prin Neprezentare";
                            string querynepre = "UPDATE Inregistrari SET StatusPersoana=@StatusPersoana WHERE IdRecord=@IdRecord";

                            SqlCommand myCommand = new SqlCommand(querynepre, databaseObject.myConnection);

                            myCommand.Parameters.AddWithValue("@StatusPersoana", StatusPersoana);
                            myCommand.Parameters.AddWithValue("@IdRecord", Neprezentare);

                            databaseObject.OpenConnection();

                            var result = myCommand.ExecuteNonQuery();

                            databaseObject.CloseConnection();

                            if (result != 0)
                            {
                                string script = "alert(\"Inchidere prin Neprezentare Reusita!\");";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                ReloadPagina();
                            }
                            else
                            {
                                string script = "alert(\"Nu exista acest Numar Programare!\");";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                            }
                        }
                        else
                        {
                            string script = "alert(\"Puteti modifica numai Inregistrarile Existente cu statusul : Programat!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                    }




                }
                else
                {
                    string script = "alert(\"Id Neprezentare trebuie sa fie intreg!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Inchidere Neprezentare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[10].Text).ToUpper() == "VALABIL")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                }
                else if ((e.Row.Cells[10].Text).ToUpper() == "EXPIRAT")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                }
                else if ((e.Row.Cells[10].Text).ToUpper() == "NEINSTRUIT")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}