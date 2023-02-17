using Dynamics365.Work1.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dynamics365.Work1.Model
{
    public class AccountModel
    {
        #region GetSet
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Constructor
        public AccountModel(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "account";
        }
        #endregion

        public Guid CreateModel(string answerAccountName, string answerCNPJ, int answerCompanyStores, int answerCityStores, string answerCompanyEmployees, decimal answerCompanyRating)
        {
            ContactController contactController = new ContactController(this.ServiceClient);
            Entity employeeId = contactController.GetContactByName(answerCompanyEmployees);
            Entity account = new Entity(this.LogicalName);

            account["name"] = answerAccountName;
            account["wrk1_cnpj"] = answerCNPJ;
            account["wrk1_store_quantity"] = answerCompanyStores;

            if (answerCityStores != 0)
            {
                account["wrk1_stores_list"] = new OptionSetValue(answerCityStores);
            }

            if (employeeId != null)
            {
                account["wrk1_employees"] = new EntityReference("account", (Guid)employeeId["contactid"]);
            } 

            account["wrk1_company_rating"] = answerCompanyRating;

            Guid accountId = this.ServiceClient.Create(account);

            return accountId;
        }

        public Entity QueryCNPJ(string answerCNPJ)
        {
            QueryExpression queryCNPJ = new QueryExpression(this.LogicalName);
            queryCNPJ.Criteria.AddCondition("wrk1_cnpj", ConditionOperator.Equal, answerCNPJ);
            return RetrieveAccount(queryCNPJ);
        }

        private Entity RetrieveAccount(QueryExpression queryAccount)
        {
            EntityCollection account = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (account.Entities.Count() > 0)
            {
                return account.Entities.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
