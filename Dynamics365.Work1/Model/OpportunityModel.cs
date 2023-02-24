using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work1.Model
{
    public class OpportunityModel
    {
        #region GetSet
        public IOrganizationService ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Constructor
        public OpportunityModel(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "opportunity";
        }
        public OpportunityModel(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "opportunity";
        }
        #endregion
    }
}
