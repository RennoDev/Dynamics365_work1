using Dynamics365.Work2.LeadModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work2.LeadController
{
    public class LeadController
    {
        #region GetSet
        public IOrganizationService ServiceClient { get; set; }
        public LeadModel Lead { get; set; }
        #endregion

        #region Constructor
        public LeadController(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Lead = new LeadModel(ServiceClient);
        }
        public LeadController(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Lead = new LeadModel(ServiceClient);
        }
        #endregion

        public Entity GetLeadData(Guid id, string[] columns)
        {
            return Lead.GetLeadData(id, columns);
        }
    }
}
