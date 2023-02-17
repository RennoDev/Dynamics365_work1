using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamics365.Work1.Controller;

namespace Dynamics365.Work1.Model
{
    public class ContactModel
    {
        #region GetSet
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Constructor
        public ContactModel(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "contact";
        }
        #endregion

        public Guid CreateModel(string answerContactCPF, string answerContactName, string answerContactTelephone, Guid accountid)
        {
            Entity contact = new Entity(this.LogicalName);

            contact["firstname"] = answerContactName;
            contact["wrk1_contact_cpf"] = answerContactCPF;
            contact["telephone1"] = answerContactTelephone;
            contact["parentcustomerid"] = new EntityReference("account", accountid);

            Guid contactId = this.ServiceClient.Create(contact);

            return contactId;
        }
        public Entity QueryEmployee(string answerCompanyEmployees)
        {
            QueryExpression queryEmployee = new QueryExpression(this.LogicalName);
            queryEmployee.ColumnSet.AddColumns("contactid");
            queryEmployee.Criteria.AddCondition("firstname", ConditionOperator.Equal, answerCompanyEmployees);
            return RetrieveContact(queryEmployee);
        }

        public Entity QueryCPF(string answerContactCPF)
        {
            QueryExpression queryCPF = new QueryExpression(this.LogicalName);
            queryCPF.Criteria.AddCondition("wrk1_contact_cpf", ConditionOperator.Equal, answerContactCPF);
            return RetrieveContact(queryCPF);
        }

        private Entity RetrieveContact(QueryExpression queryContact)
        {
            EntityCollection contact = this.ServiceClient.RetrieveMultiple(queryContact);

            if (contact.Entities.Count() > 0)
            {
                return contact.Entities.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}           
