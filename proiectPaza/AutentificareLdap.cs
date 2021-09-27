using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace proiectPaza
{
    public class AutentificareLdap
    {

        public bool LDAPautentification(string user, string parola)
        {
            bool isValid = false;
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "ustunberk.local"))
                {
                    //da false;
                    isValid = pc.ValidateCredentials(user, parola);
                }
            }
            catch
            {

            }
            return isValid;
        }

    }
}