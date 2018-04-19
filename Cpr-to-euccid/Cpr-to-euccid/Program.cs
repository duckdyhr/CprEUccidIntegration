using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library;

namespace Cpr_to_euccid
{
    class Program
    {
        static void Main(string[] args)
        {
            var inTranslator = Service.GenerateMsgQueue("InputTranslator");
            var createEu = Service.GenerateMsgQueue("inputEU");
            var requestEu = Service.GenerateMsgQueue("requestEu");

            var dk = new DKgateway(inTranslator, requestEu, "replyDk");
            var translator = new Translator(inTranslator, createEu);
            
            var dkC = new DKcitizen
            {
                CprNr = "251287-1234",
                FirstName = "Anne Dyhr",
                SurName = "Pedersen"
            };
            var dkC1 = new DKcitizen
            {
                CprNr = "290983-1233",
                FirstName = "Erik Dyhr",
                SurName = "Pedersen"
            };
            dk.CreateDkCitizenInEuSystem(dkC);
            dk.CreateDkCitizenInEuSystem(dkC1);

            var eu = new EUgateway(createEu, requestEu);

            Thread.Sleep(600);

            Service.GetAllEUcitizens().ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}
