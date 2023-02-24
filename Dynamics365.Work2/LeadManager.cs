using Dynamics365.Work1.Controller;
using Dynamics365.Work2.LeadController;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work2
{
    public class LeadManager : IPlugin
    {
        public IOrganizationService Service { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity lead = (Entity)context.InputParameters["Target"];

            if (context.MessageName == "Update")
            {
                Entity leadPostImage = (Entity)context.PostEntityImages["postimage"];
                Guid leadId = lead.Id;

                OptionSetValue leadStatus = leadPostImage.Contains("statuscode") ? (OptionSetValue)leadPostImage["statuscode"] : null;

                if (leadStatus.Value == 3)
                {
                    OpportunityController opportunityController = new OpportunityController(this.Service);
                    Entity opportunity = opportunityController.GetRelatedLead(leadId);
                    EntityReference account = null;

                    if (opportunity != null)
                    {
                        account = opportunity.Attributes.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null;
                    }

                    if (account != null)
                    {
                        AccountController accountController = new AccountController(this.Service);
                        accountController.DecrementOrIncrementNumberOfOpp(account, true);
                    }
                }
            }
        }
    }
}
