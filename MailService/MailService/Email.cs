using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;


namespace MailService
{
    class Email
    {
        public bool Send(string text, string subiect, string AdressTo, string AdressCc)
        {
            List<string> To = new List<string>();
            List<string> Cc = new List<string>();
            string[] ato = AdressTo.Split(';');
            string[] acc = AdressCc.Split(';');
            foreach (string adresa in ato)
            {
                if (IsValidEmail(adresa))
                    To.Add(adresa);
            }
            foreach (string adresa in acc)
            {
                if (IsValidEmail(adresa))
                    Cc.Add(adresa);
            }

            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "mail.ustunberkteknik.com";
            client.EnableSsl = false;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("lectra@ustunberkteknik.com", "jh0SjQC74y");

            MailMessage mm = new MailMessage();
            mm.IsBodyHtml = true;
            foreach (string a in To)
            {
                mm.To.Add(a);
            }
            foreach (string a in Cc)
            {
                mm.CC.Add(a);
            }
            mm.From = new MailAddress("lectra@ustunberkteknik.com", "Martur Announcement System");
            mm.Subject = subiect;
            mm.IsBodyHtml = true;
            mm.Body = text;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(mm);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
