using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UAParser;

namespace proiectPaza
{
    public partial class _Default : Page
    {
        //public System.Timers.Timer aTimer;

       //public void Application_Start(object sender, EventArgs e)
       // {
       //     //// Code that runs on application startup
       //     //System.Timers.Timer timer = new System.Timers.Timer();
       //     //// interval primul numar=minute.
       //     //timer.Interval = 1 * 60 * 1000;
       //     //timer.AutoReset = true;
       //     //timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
       //     //timer.Enabled = true;
       //     //timer.Start();

       //     aTimer = new System.Timers.Timer(1 * 60 * 1000); //one hour in milliseconds
       //     aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
       //     aTimer.Start();


       // }

       // public void OnTimedEvent(object src, ElapsedEventArgs e)
       // {


       //     //  MailRegulat me = new MailRegulat();

       //     //   me.SendMailRegulat();

       //     string Subiect;
       //     string Text;
       //     string Emailget;
       //     string Emailsend;

       //     Subiect = "test";
       //     Text = "bla";
       //     Emailget = "vlad.arsene@marturfompak.com";
       //     Emailsend = "ovidiu.gionea@marturfompak.com";

       //     Email ema = new Email();

       //     ema.Send(Text, Subiect, Emailsend, Emailget);

       // }

        //public void TimerElapsed(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    // Instanciate the Mail class.
        //   MailRegulat me = new MailRegulat();
        //    // Call the send mail method.
        //    me.SendMailRegulat();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = tbxEmail.Text;
                string Pass = tbxParola.Text;

                Response.Cookies["dateutilizator"].Expires = DateTime.Now.AddDays(-1);

                var userAgent =Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);

                string numebrowser = c.UA.Family;

                  if (numebrowser=="Edge"||numebrowser=="Chrome")
                  {
                   
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Pass))
                    {
                        string script = "alert(\"Completati toate campurile!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                    else
                    {
                        AutentificareLdap auth = new AutentificareLdap();
                        var statusauten = auth.LDAPautentification(Username, Pass);
                        if (statusauten == true)
                        {

                            HttpCookie cooku = new HttpCookie("dateutilizator");
                            cooku.Expires = DateTime.Now.AddDays(1);
                            cooku.Value = Username;
                            Response.Cookies.Add(cooku);

                            Session["login"] = Username;



                            Response.Redirect("~/Programare.aspx");
                        }
                        else if (statusauten == false)
                        {
                            string script = "alert(\"Eroare autentificare!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                        else
                        {
                            string script = "alert(\"Eroare autentificare!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                    }


                }
                  else
                  {
                    string script = "alert(\"Acest Browser nu este compatibil! // Folositi Google Chrome sau Edge Chromium (varianta noua) \");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                }



            }
            catch (Exception)
            {
                string script = "alert(\"Eroare pagina!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}