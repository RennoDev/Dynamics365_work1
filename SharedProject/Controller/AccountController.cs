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
    public class AccountController
    {
        #region GetSet
        public IOrganizationService ServiceClient { get; set; }
        public AccountModel Account { get; set; }
        #endregion

        #region Constructor
        public AccountController(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Account = new AccountModel(ServiceClient);
        }
        public AccountController(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Account = new AccountModel(ServiceClient);
        }
        #endregion

        /*public Guid CreateController(string answerAccountName, string answerCNPJ, int answerCompanyStores, int answerCityStores, string answerCompanyEmployees, decimal answerCompanyRating)
        {
            AccountModel account = new AccountModel(this.ServiceClient);
            return account.CreateModel(answerAccountName, answerCNPJ, answerCompanyStores, answerCityStores, answerCompanyEmployees, answerCompanyRating);
        }

        public Entity GetAccountByCNPJ(string answerCNPJ)
        {
            return Account.QueryCNPJ(answerCNPJ);
        }
        */

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return Account.GetAccountById(id, columns);
        }

        public void DecrementOrIncrementNumberOfOpp(EntityReference oppAccount, bool? decrementOrIncrement)
        {
            Account.DecrementOrIncrementNumberOfOpp(oppAccount, decrementOrIncrement);
        }
    }
}
