using Dynamics365.Work1.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365.Work1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();
            AccountController accountController = new AccountController(serviceClient);
            ContactController contactController = new ContactController(serviceClient);

            Menu(accountController, contactController);

            Console.ReadKey();
        }

        private static void Menu(AccountController accountController, ContactController contactController)
        {
            Console.WriteLine("Por favor, informe o nome da empresa:");
            string answerAccountName = Console.ReadLine();

            Guid accountid = AccountCreate(accountController, contactController, answerAccountName);

            if (accountid != Guid.Empty)
            {
                Console.WriteLine("Deseja criar um contato para a empresa? (S/N)");
                string answerCreateContact = Console.ReadLine().ToUpper();

                if (answerCreateContact == "S")
                {
                    ContactCreate(contactController, accountController, accountid);
                }
                else
                {
                    Console.WriteLine("Deseja voltar ao início? (S/N)");
                    string answerHome = Console.ReadLine().ToUpper();

                    if (answerHome == "S")
                    {
                        Menu(accountController, contactController);
                    }
                    else
                    {
                        Console.WriteLine("Fim do programa.");
                        return;
                    }
                }
            }
        }

        private static Guid AccountCreate(AccountController accountController, ContactController contactController, string answerAccountName)
        {
            Console.WriteLine("Qual o CNPJ da sua empresa?");
            string answerCNPJ = Console.ReadLine();
            Entity accountCNPJ = accountController.GetAccountByCNPJ(answerCNPJ);
            
            if (accountCNPJ != null)
            {
                Console.WriteLine("Conta já existente, voltando ao início...");
                Menu(accountController, contactController);
                return Guid.Empty;
            }

            Console.WriteLine("Quantas lojas tem sua empresa?");
            int answerCompanyStores = int.Parse(Console.ReadLine());

            Console.WriteLine("Em qual cidade sua empresa tem loja?");
            int answerCityStores = WorkingCities();

            Console.WriteLine("Digite o nome do funcionário do mês:");
            string answerCompanyEmployees = Console.ReadLine();

            Console.WriteLine("Qual a avaliação da empresa no site RECLAME AQUI?");
            decimal answerCompanyRating = decimal.Parse(Console.ReadLine());

           Console.WriteLine("Criando conta...");
            Guid accountId = accountController.CreateController(answerAccountName, answerCNPJ, answerCompanyStores, answerCityStores, answerCompanyEmployees, answerCompanyRating);
            Console.WriteLine("Conta criada com sucesso! copie e cole o link abaixo no seu navegado:");
            Console.WriteLine($"https://orgef42a8de.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=account&id={accountId}");
            return accountId;
        }

        private static void ContactCreate(ContactController contactController, AccountController accountController, Guid accountid)
        {
            Console.WriteLine("Qual o CPF do contato?");
            string answerContactCPF = Console.ReadLine();
            Entity contactCPF = contactController.GetContactByCPF(answerContactCPF);

            if (contactCPF != null)
            {
                Console.WriteLine("Contato já existente, voltando ao início...");
                Menu(accountController, contactController);
                return;
            }

            Console.WriteLine("Qual o nome do contato");
            string answerContactName = Console.ReadLine();

            Console.WriteLine("Qual o telefone do contato?");
            string answerContactTelephone = Console.ReadLine();

            Console.WriteLine("Criando contato...");
            Guid contactId = contactController.CreateController(answerContactCPF, answerContactName, answerContactTelephone, accountid);
            Console.WriteLine("Contato criado com sucesso! copie e cole o link abaixo no seu navegador:");
            Console.WriteLine($"https://orgef42a8de.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=contact&id={contactId}");
        }
        
        private static int WorkingCities()
        {
            Console.WriteLine("1. Hortolandia");
            Console.WriteLine("2. Campinas");
            Console.WriteLine("3. Americana\n");
            string answerWorkingCities = Console.ReadLine();
            int valor = 0;

            if (answerWorkingCities == "1")
            {
                valor = 1;
            }
            if (answerWorkingCities == "2")
            {
                valor = 2;
            }
            if (answerWorkingCities == "3")
            {
                valor = 3;
            }
            return valor;
        }
    }
}
