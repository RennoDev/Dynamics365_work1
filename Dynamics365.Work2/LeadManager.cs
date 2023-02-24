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

            /*
                Valor do status pegarei do Target
                Validar status do lead 
                Checar se é valor 3 = Qualificado
                Encontrar a oportunidade relacionada à qualificação (ver o campo lógico)
                Dentro da oportunidade, buscar a conta relacionada
                Se for qualificado, executar plugin para atualizar a quantidade de oportunidades da conta em que o lead está.
            */

            tracingService.Trace("FIRST");

            Entity lead = (Entity)context.InputParameters["Target"];

            if (context.MessageName == "Update")
            {
                tracingService.Trace("second");
                Entity leadPostImage = (Entity)context.PostEntityImages["postimage"];
                tracingService.Trace("third");
                Guid leadId = lead.Id;
                tracingService.Trace("Início Plugin");

                tracingService.Trace("1");
                
                OptionSetValue leadStatus = leadPostImage.Contains("statuscode") ? (OptionSetValue)leadPostImage["statuscode"] : null;

                tracingService.Trace("2");

                if (leadStatus.Value == 3)
                {
                    tracingService.Trace("3");

                    OpportunityController opportunityController = new OpportunityController(this.Service);
                    Entity opportunity = opportunityController.GetRelatedLead(leadId);
                    EntityReference account = null;

                    if (opportunity != null)
                    {
                        account = opportunity.Attributes.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null;
                    }

                    tracingService.Trace("4");

                    if (account != null)
                    {
                        tracingService.Trace("5");

                        AccountController accountController = new AccountController(this.Service);
                        accountController.DecrementOrIncrementNumberOfOpp(account, true);

                        tracingService.Trace("6");
                    }
                }
            }



        }
    }
}
