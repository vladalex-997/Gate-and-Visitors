using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiectPaza
{
    public class MailRegulat
    {
        public void SendMailRegulat()
        {
            string Subiect;
            string Text;
            string Emailget;
            string Emailsend;

            Subiect = "test";
            Text = "bla";
            Emailget = "vlad.arsene@marturfompak.com";
            Emailsend = "ovidiu.gionea@marturfompak.com";

            Email ema = new Email();

            ema.Send(Text, Subiect, Emailsend, Emailget);


        }
    }
}