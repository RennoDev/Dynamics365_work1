using Dynamics365.Work1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work1.Controller
{
    public class ContactController
    {
        #region GetSet
        public CrmServiceClient ServiceClient { get; set; }
        public ContactModel Contact { get; set; }
        #endregion

        #region Constructor
        public ContactController(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Contact = new ContactModel(ServiceClient);
        }
        #endregion

        public Guid CreateController(string answerContactCPF, string answerContactName, string answerContactTelephone, Guid accountid)
        {
            ContactModel contact = new ContactModel(this.ServiceClient);
            return contact.CreateModel(answerContactCPF, answerContactName, answerContactTelephone, accountid);
        }

        public Entity GetContactByName(string contactName)
        {
            return Contact.QueryEmployee(contactName);
        }

        public Entity GetContactByCPF(string answerContactCPF)
        {
            return Contact.QueryCPF(answerContactCPF);
        }
    }
}
