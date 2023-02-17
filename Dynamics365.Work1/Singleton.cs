using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work1
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "orgef42a8de";
            string clientId = "26c3b5e4-9cc7-4cdb-879a-0499213dc252";
            string clientSecret = "QA48Q~8nVuQZQJhpAdyVLY..qanAtto6h4PLravX";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
            {
                Console.WriteLine("Conexão feita!\n");
            }
            else
            {
                Console.WriteLine("Erro na conexão.");
            }

            return serviceClient;
        }
    }
}

