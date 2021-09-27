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
    public partial class Programare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //  tbxOraProgramare.Text = DateTime.Now.ToString();

               // tbxOraProgramare.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
               // tbxDataExpirare.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");


                if (!Page.IsPostBack)
                {
                    Database databaseObject = new Database();
                    databaseObject.OpenConnection();

                    string query = "SELECT Nume from ListaAngajati order by Nume";
                    SqlCommand myCommand = new SqlCommand(query, databaseObject.myConnection);

                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    DropDownListdestinatie.DataSource = ds.Tables[0];
                    DropDownListdestinatie.DataTextField = ds.Tables[0].Columns["Nume"].ToString();
                    DropDownListdestinatie.DataValueField = ds.Tables[0].Columns["Nume"].ToString();

                    DropDownListdestinatie.DataBind();

                    databaseObject.CloseConnection();


                    databaseObject.OpenConnection();

                    string querymotiv = "SELECT Motiv from MotivVizita order by Motiv";
                    SqlCommand myCommandm = new SqlCommand(querymotiv, databaseObject.myConnection);

                    SqlDataAdapter dam = new SqlDataAdapter(myCommandm);
                    DataSet dsm = new DataSet();
                    dam.Fill(dsm);

                    DropDownListmotiv.DataSource = dsm.Tables[0];
                    DropDownListmotiv.DataTextField = dsm.Tables[0].Columns["Motiv"].ToString();
                    DropDownListdestinatie.DataValueField = dsm.Tables[0].Columns["Motiv"].ToString();

                    DropDownListmotiv.DataBind();

                    databaseObject.CloseConnection();
                }

                

                
            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Incarcare Pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        public void autocomple()
        {
            try
            {
                string Nume = tbxNume.Text;
                string Act = tbxAct.Text;
              //  string Companie = tbxCompanie.Text;

                string StatusInstruire="";
                string InstruitDe="";
                DateTime DataInstruire=DateTime.Now;
                string DataInstruires="";


                Database databaseObject = new Database();

                string queryverif = "SELECT StatusInstruire,InstruitDe,DataInstruire from Inregistrari WHERE " +
                       "Nume=@Nume AND Act=@Act";

                SqlCommand mycommandselect = new SqlCommand(queryverif, databaseObject.myConnection);

                mycommandselect.Parameters.AddWithValue("@Nume", Nume);
                mycommandselect.Parameters.AddWithValue("@Act", Act);
              //  mycommandselect.Parameters.AddWithValue("@Companie", Companie);
                try
                {
                    databaseObject.OpenConnection();
                    SqlDataReader re = mycommandselect.ExecuteReader();
                  
                    if (re.HasRows)
                    {
                        while (re.Read())
                        {
                            if (string.IsNullOrEmpty(re[0].ToString()) || string.IsNullOrEmpty(re[1].ToString()) || string.IsNullOrEmpty(re[2].ToString()))
                            {

                            }
                            else
                            {
                                StatusInstruire = re[0].ToString();
                                InstruitDe = re[1].ToString();
                                DataInstruires = re[2].ToString();
                            }
                           

                        }
                re.Close();
                databaseObject.CloseConnection();
                DataInstruire = DateTime.Parse(DataInstruires);

                string queryupdate = "UPDATE Inregistrari SET StatusInstruire=@StatusInstruire, DataInstruire=@DataInstruire, InstruitDe=@InstruitDe " +
                            "WHERE Nume=@Nume AND Act=@Act";

                        SqlCommand mycommandupdate = new SqlCommand(queryupdate, databaseObject.myConnection);

                        mycommandupdate.Parameters.AddWithValue("@Nume", Nume);
                        mycommandupdate.Parameters.AddWithValue("@Act", Act);
                       // mycommandupdate.Parameters.AddWithValue("@Companie", Companie);
                        mycommandupdate.Parameters.AddWithValue("@StatusInstruire", StatusInstruire);
                        mycommandupdate.Parameters.AddWithValue("@InstruitDe", InstruitDe);
                        mycommandupdate.Parameters.AddWithValue("@DataInstruire", DataInstruire);


                        databaseObject.OpenConnection();
                        var rezultat = mycommandupdate.ExecuteNonQuery();
                        databaseObject.CloseConnection();

                    }
                    else
                    {
                        

                    }
                    
            

              }
             catch (Exception)
              {


             }



             }
             catch (Exception)
             {

             }
        }

        protected void btnProgramare_Click(object sender, EventArgs e)
        {
            try
            {
                string Nume = tbxNume.Text;
                string Act = tbxAct.Text;
                string Destinatie = DropDownListdestinatie.SelectedValue;
                string Motiv = DropDownListmotiv.SelectedValue;
                string StatusPersoana = "Programat";
                string Companie = tbxCompanie.Text;
                string NumarMasina = tbxNumarMasina.Text;


                string OraProgramatastring = tbxOraProgramare.Text;
                DateTime OraProgramata = DateTime.ParseExact(OraProgramatastring, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                string DataExpirarestring = tbxDataExpirare.Text;
                DateTime DataExpirare=DateTime.ParseExact(DataExpirarestring, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Nume) || string.IsNullOrEmpty(Act) || string.IsNullOrEmpty(Destinatie) || string.IsNullOrEmpty(Motiv)|| DataExpirare <= OraProgramata)
                {
                    //string.IsNullOrEmpty(Nume)||string.IsNullOrEmpty(Act)||string.IsNullOrEmpty(Destinatie)||string.IsNullOrEmpty(Motiv)
                    string script = "alert(\"Completati corect toate campurile cu * !\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    Database databaseObject = new Database();



                    string queryverif = "SELECT Nume,Act,Destinatie,Motiv,OraProgramata,DataExpirare from Inregistrari WHERE " +
                        "Nume=@Nume AND Act=@Act AND Destinatie=@Destinatie AND Motiv=@Motiv AND OraProgramata=@OraProgramata AND DataExpirare=@DataExpirare";

                    SqlCommand mycommandselect = new SqlCommand(queryverif, databaseObject.myConnection);

                    mycommandselect.Parameters.AddWithValue("@Nume", Nume);
                    mycommandselect.Parameters.AddWithValue("@Act", Act);
                    mycommandselect.Parameters.AddWithValue("@Destinatie", Destinatie);
                    mycommandselect.Parameters.AddWithValue("@Motiv", Motiv);
                    mycommandselect.Parameters.AddWithValue("@OraProgramata", OraProgramata);
                    mycommandselect.Parameters.AddWithValue("@DataExpirare", DataExpirare);

                    try
                    {

                        databaseObject.OpenConnection();
                        var selectobj = mycommandselect.ExecuteScalar();
                        databaseObject.CloseConnection();

                        if(selectobj is null)
                        {


                            databaseObject.OpenConnection();

                            string tempnume;
                            tempnume = Request.Cookies["dateutilizator"].Value;

                            string query = "INSERT INTO Inregistrari (Nume,Act,Destinatie,Motiv,StatusPersoana,Companie,NumarMasina,OraProgramata,DataExpirare,ProgramatDe) VALUES (@Nume,@Act,@Destinatie,@Motiv,@StatusPersoana,@Companie,@NumarMasina,@OraProgramata,@DataExpirare,@ProgramatDe)";

                            SqlCommand myCommand = new SqlCommand(query, databaseObject.myConnection);

                            myCommand.Parameters.AddWithValue("@Nume", Nume);
                            myCommand.Parameters.AddWithValue("@Act", Act);
                            myCommand.Parameters.AddWithValue("@Destinatie", Destinatie);
                            myCommand.Parameters.AddWithValue("@Motiv", Motiv);
                            myCommand.Parameters.AddWithValue("@StatusPersoana", StatusPersoana);
                            myCommand.Parameters.AddWithValue("@Companie", Companie);
                            myCommand.Parameters.AddWithValue("@NumarMasina", NumarMasina);
                            myCommand.Parameters.AddWithValue("@OraProgramata", OraProgramata);
                            myCommand.Parameters.AddWithValue("@DataExpirare", DataExpirare);
                            myCommand.Parameters.AddWithValue("@ProgramatDe", tempnume);

                            var result = myCommand.ExecuteNonQuery();

                            databaseObject.CloseConnection();

                            string script = "alert(\"Programare reusita !\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                            autocomple();

                            tbxNume.Text = "";
                            tbxAct.Text = "";
                        }
                        else
                        {

                            string script = "alert(\"Aceasta Programare exista deja!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                        }


                    }
                    catch (Exception)
                    {

                    }




                }
                

            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Programare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnProgramareConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Nume = tbxNume.Text;
                string Act = tbxAct.Text;
                string Destinatie = DropDownListdestinatie.SelectedValue;
                string Motiv = DropDownListmotiv.SelectedValue;
                string StatusPersoana = "Programat";
                string Companie = tbxCompanie.Text;
                string NumarMasina = tbxNumarMasina.Text;
                string EmailVizitator = tbxEmail.Text;
                string EmailUser ;

                string tempnume;
                tempnume = Request.Cookies["dateutilizator"].Value;
                EmailUser = tempnume + "@marturfompak.com";



                string OraProgramatastring = tbxOraProgramare.Text;
                DateTime OraProgramata = DateTime.ParseExact(OraProgramatastring, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                string DataExpirarestring = tbxDataExpirare.Text;
                DateTime DataExpirare = DateTime.ParseExact(DataExpirarestring, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Nume) || string.IsNullOrEmpty(Act) || string.IsNullOrEmpty(Destinatie) || string.IsNullOrEmpty(Motiv)||string.IsNullOrEmpty(EmailVizitator)|| DataExpirare <= OraProgramata)
                {
                    //string.IsNullOrEmpty(Nume)||string.IsNullOrEmpty(Act)||string.IsNullOrEmpty(Destinatie)||string.IsNullOrEmpty(Motiv)
                    string script = "alert(\"Completati corect toate campurile cu * si Email!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {

                    Database databaseObject = new Database();

                  

                    string queryverif = "SELECT Nume,Act,Destinatie,Motiv,OraProgramata,DataExpirare,Email from Inregistrari WHERE " +
                        "Nume=@Nume AND Act=@Act AND Destinatie=@Destinatie AND Motiv=@Motiv AND OraProgramata=@OraProgramata AND DataExpirare=@DataExpirare AND Email=@Email";

                    SqlCommand mycommandselect = new SqlCommand(queryverif, databaseObject.myConnection);

                    mycommandselect.Parameters.AddWithValue("@Nume", Nume);
                    mycommandselect.Parameters.AddWithValue("@Act", Act);
                    mycommandselect.Parameters.AddWithValue("@Destinatie", Destinatie);
                    mycommandselect.Parameters.AddWithValue("@Motiv", Motiv);
                    mycommandselect.Parameters.AddWithValue("@OraProgramata", OraProgramata);
                    mycommandselect.Parameters.AddWithValue("@DataExpirare", DataExpirare);
                    mycommandselect.Parameters.AddWithValue("@Email", EmailVizitator);


                    try 
                    {
                        databaseObject.OpenConnection();
                        var selectobj = mycommandselect.ExecuteScalar();
                        databaseObject.CloseConnection();

                        if(selectobj is null)
                        {



                            databaseObject.OpenConnection();



                            string query = "INSERT INTO Inregistrari (Nume,Act,Destinatie,Motiv,StatusPersoana,Companie,NumarMasina,OraProgramata,DataExpirare,Email,ProgramatDe) VALUES (@Nume,@Act,@Destinatie,@Motiv,@StatusPersoana,@Companie,@NumarMasina,@OraProgramata,@DataExpirare,@Email,@ProgramatDe)";

                            SqlCommand myCommand = new SqlCommand(query, databaseObject.myConnection);

                            myCommand.Parameters.AddWithValue("@Nume", Nume);
                            myCommand.Parameters.AddWithValue("@Act", Act);
                            myCommand.Parameters.AddWithValue("@Destinatie", Destinatie);
                            myCommand.Parameters.AddWithValue("@Motiv", Motiv);
                            myCommand.Parameters.AddWithValue("@StatusPersoana", StatusPersoana);
                            myCommand.Parameters.AddWithValue("@Companie", Companie);
                            myCommand.Parameters.AddWithValue("@NumarMasina", NumarMasina);
                            myCommand.Parameters.AddWithValue("@OraProgramata", OraProgramata);
                            myCommand.Parameters.AddWithValue("@DataExpirare", DataExpirare);
                            myCommand.Parameters.AddWithValue("@Email", EmailVizitator);
                            myCommand.Parameters.AddWithValue("@ProgramatDe", tempnume);

                            var result = myCommand.ExecuteNonQuery();

                            databaseObject.CloseConnection();

                            string Text;
                            string Subiect;
                            string OraRefacuta;
                            string utilizatornumef;
                            string utilizatornume;

                            string[] Orasitimp = OraProgramatastring.Split('T');
                            OraRefacuta = Orasitimp[0] + "   " + "  " + Orasitimp[1];


                            string[] Util = tempnume.Split('.');
                            utilizatornumef = Util[0] + "    " + "  " + Util[1];
                            utilizatornume = ToTitleCase(utilizatornumef);

                            Subiect = "Email Confirmation ";
                            Text = "<center> <h2> Martur Automotive Seating and Interiors </h2> </center> </br>" +
                                "<center> <u><h3>Appointment Confirmation  " + OraRefacuta + "</h3></u> </center> </br> </br>" +
                                "<center>" +
                                "<p>Dear <b>" + Nume + "</b>,</p>" +
                                "<p>We are writing to confirm your appointment with <b>" + Destinatie + "</b> on date <b>" + OraRefacuta + "</b> </br> at Martur Plant location: Str.Dacia A1, Nr. 250, Sat Catanele, " +
                                "zip code: 117221 Cateasca Arges / Romania </p> " +
                                "<p>For the entrance formalities please be sure to have Identity Document</p>" +
                                "<p> If you require any assistance in finding the location please contact: <b>" + utilizatornume + "</b></p></br></br>" +
                                "<p><b>This Email is automatically generated. Please do not respond/reply to this Email.</b></p></center>";


                            Email ema = new Email();


                            if (ema.Send(Text, Subiect, EmailVizitator, EmailUser))
                            {
                                string script = "alert(\"Programare reusita, o sa primiti confirmare pe Email !\");";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                                autocomple();

                                tbxNume.Text = "";
                                tbxAct.Text = "";

                            }
                            else
                            {
                                string script = "alert(\"Programare reusita, Eroare trimitere mail !\");";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                            }


                        }
                        else
                        {

                            string script = "alert(\"Aceasta Programare exista deja!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                        }

                    }
                    catch (Exception)
                    {

                    }

              
                   


                    


                }


            }
            catch (Exception)
            {
                string script = "alert(\"Eroare Programare!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            autocomple();
        }
    }
    }
