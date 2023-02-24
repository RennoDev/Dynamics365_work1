using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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

        public Entity GetRelatedLead(Guid leadId)
        {
            QueryExpression queryOpportunity = new QueryExpression(this.LogicalName);
            queryOpportunity.ColumnSet.AddColumns("parentaccountid");
            queryOpportunity.Criteria.AddCondition("originatingleadid", ConditionOperator.Equal, leadId);
            return RetrieveOpportunity(queryOpportunity);
        }

        public Entity RetrieveOpportunity(QueryExpression queryOpportunity)
        {
            EntityCollection opportunitys = this.ServiceClient.RetrieveMultiple(queryOpportunity);

            if (opportunitys.Entities.Count() > 0)
            {
                return opportunitys.Entities.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
