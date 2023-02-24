using Dynamics365.Work1.Controller;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work2
{
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService Service { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            AccountController accountController = new AccountController(this.Service);

            if (context.MessageName == "Update")
            {
                Entity opportunityPostImage = (Entity)context.PostEntityImages["postimage"];
                EntityReference postAccountReference = (EntityReference)opportunityPostImage["parentaccountid"];
                accountController.DecrementOrIncrementNumberOfOpp(postAccountReference, true);
            }

            if (context.MessageName == "Delete")
            {
                Entity opportunityPreImage = (Entity)context.PreEntityImages["preimage"];
                EntityReference preAccountReference = (EntityReference)opportunityPreImage["parentaccountid"];
                accountController.DecrementOrIncrementNumberOfOpp(preAccountReference, false);
            }
        }
    }
}
