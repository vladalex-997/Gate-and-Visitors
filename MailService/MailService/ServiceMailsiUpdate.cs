using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Data.SqlClient;
using System.IO;

namespace MailService
{
    public partial class ServiceMailsiUpdate : ServiceBase
    {
       

        Timer tmrExecutorMail = new Timer();
        Timer tmrExecutorUpdate = new Timer();
        public ServiceMailsiUpdate()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            tmrExecutorMail.Elapsed += new ElapsedEventHandler(tmrExecutorMail_Elapsed); // adding Event
            tmrExecutorMail.Interval = 28800000; // 8 ore 
            tmrExecutorMail.Enabled = true;
            tmrExecutorMail.Start();

            tmrExecutorUpdate.Elapsed += new ElapsedEventHandler(tmrExecutorUpdate_Elapsed); // adding Event
            tmrExecutorUpdate.Interval = 3600000; // o ora 
            tmrExecutorUpdate.Enabled = true;
            tmrExecutorUpdate.Start();
        }



        private void tmrExecutorMail_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime curent = DateTime.Now;
                DateTime after7 = curent.AddDays(7);

                DataBase databaseObject = new DataBase();

                string queryselect = "SELECT IdRecord,Nume,Act,StatusPersoana,OraProgramata,DataInstruire,StatusInstruire,InstruitDe from Inregistrari " +
                    "WHERE Companie!='"+"' AND OraProgramata BETWEEN @curent AND @after7";

                SqlCommand mycommandselect = new SqlCommand(queryselect, databaseObject.myConnection);

                mycommandselect.Parameters.AddWithValue("@curent",curent);
                mycommandselect.Parameters.AddWithValue("@after7", after7);


                databaseObject.OpenConnection();

                SqlDataAdapter da = new SqlDataAdapter(mycommandselect);
                DataTable dt = new DataTable();
                da.Fill(dt);

                databaseObject.CloseConnection();






                string Subiect;
                string Text;
                string Emailget;
                string Emailsend;

                Subiect = "HSE training status - for the people that are coming in the next 7 days";
                Text = getHtml(dt);
                Emailget = "vlad.arsene@marturfompak.com;ovidiu.gionea@marturfompak.com;andrei.rusu@marturfompak.com;erhan.iclek@marturfompak.com";
                Emailsend = "alexandru.craciun@marturfompak.com;adriana.gardin@marturfompak.com";


/*
                string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mailtrimitere.txt");
                string[] files1 = File.ReadAllLines(path1);
                string adreseto = files1[0];
                string adresecc = files1[1];
*/
                Email ema = new Email();

                ema.Send(Text, Subiect, Emailsend, Emailget);
            }
            catch (Exception)
            {

            }
          
        }

        public static string getHtml(DataTable table1)
        {
            try
            {
                string messageBody = "<font>The following people are going to come on Martur site for different jobs. Please check and be sure about HSE training status and validity date: </font><br><br>";

                if (table1.Rows.Count == 0)
                    return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Id Record " + htmlTdEnd;
                messageBody += htmlTdStart + "Nume si Prenume " + htmlTdEnd;
                messageBody += htmlTdStart + "Carte de identitate " + htmlTdEnd;
                messageBody += htmlTdStart + "Status Persoana " + htmlTdEnd;
                messageBody += htmlTdStart + "Ora Programata Venire " + htmlTdEnd;
                messageBody += htmlTdStart + "Instruire valabila pana la " + htmlTdEnd;
                messageBody += htmlTdStart + "Status Instruire " + htmlTdEnd;
                messageBody += htmlTdStart + "Instruit De " + htmlTdEnd;

                messageBody += htmlHeaderRowEnd;

                //foreach (DataRow Row in dataSet.Tables[0].Rows)
                //{
                //    messageBody = messageBody + htmlTrStart;
                //    messageBody = messageBody + htmlTdStart + Row["fieldName"] + htmlTdEnd;
                //    messageBody = messageBody + htmlTrEnd;
                //}
                //messageBody = messageBody + htmlTableEnd;

                for(int i=0; i<=table1.Rows.Count-1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][0] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][1] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][2] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][3] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][4] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][5] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][6] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + table1.Rows[i][7] + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }

                messageBody = messageBody + htmlTableEnd;
                return messageBody;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private void tmrExecutorUpdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DataBase databaseObject = new DataBase();

                DateTime curent = DateTime.Now;
                string StatusInstruire = "Expirat";

                string queryupdate = "UPDATE Inregistrari SET StatusInstruire=@StatusInstruire WHERE DataInstruire<@DataInstruire";

                SqlCommand mycommandupdate = new SqlCommand(queryupdate, databaseObject.myConnection);

                mycommandupdate.Parameters.AddWithValue("@StatusInstruire",StatusInstruire);
                mycommandupdate.Parameters.AddWithValue("@DataInstruire",curent);

                databaseObject.OpenConnection();
                var rezultat = mycommandupdate.ExecuteNonQuery();
                databaseObject.CloseConnection();



            }
            catch (Exception)
            {

            }


        }

        protected override void OnStop()
        {
            tmrExecutorMail.Enabled = false;
            tmrExecutorUpdate.Enabled = false;
        }

        
    }
}
