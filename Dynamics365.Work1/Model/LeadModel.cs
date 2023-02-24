using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work2.LeadModel
{
    public class LeadModel
    {
        #region GetSet
        public IOrganizationService ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Constructor
        public LeadModel(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "lead";
        }
        public LeadModel(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "lead";
        }
        #endregion

        public Entity GetLeadData(Guid id, string[] columns)
        {
            QueryExpression leadData = new QueryExpression(this.LogicalName);
            leadData.ColumnSet.AddColumns(columns);
            leadData.Criteria.AddCondition("leadid", ConditionOperator.Equal, id);
            return RetrieveAccount(leadData);
        }

        private Entity RetrieveAccount(QueryExpression leadData)
        {
            EntityCollection lead = this.ServiceClient.RetrieveMultiple(leadData);

            if (lead.Entities.Count() > 0)
            {
                return lead.Entities.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
