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
    internal class OpportunityController
    {
        #region GetSet
        public IOrganizationService ServiceClient { get; set; }
        public OpportunityModel Opportunity { get; set; }
        #endregion

        #region Constructor
        public OpportunityController(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Opportunity = new OpportunityModel(ServiceClient);
        }
        public OpportunityController(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Opportunity = new OpportunityModel(ServiceClient);
        }
        #endregion

        public Entity GetRelatedLead(Guid leadId)
        {
            return Opportunity.GetRelatedLead(leadId);
        }
    }
}
