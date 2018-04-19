using System;
using System.Collections.Generic;
using System.Messaging;
using Library;

namespace Cpr_to_euccid
{
    class Service
    {
        private static readonly List<string> ApartmentNos;
        private static readonly string QueuePath = @".\private$\";
        private static MessageQueue _deadletter;
        private static List<EUcitizen> _euCitizens;

        static Service()
        {
            ApartmentNos = new List<string>()
            {
                "th.", "tv.", "mf.", "kl"
            };
            for (var i = 1; i < 20; i++)
            {
                ApartmentNos.Add(i + ".");
            }
            _euCitizens = new List<EUcitizen>();
        }

        public static List<string> GetApartmentNos()
        {
            return ApartmentNos;
        }
        public static MessageQueue GenerateMsgQueue(string name)
        {
            var path = QueuePath + name;

            // Create the Queue
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }

            var messageQueue = new MessageQueue(path) {Label = name + " channel"};

            return messageQueue;
        }

        public static void CreateNewEuCitizen(EUcitizen euc)
        {
            Console.WriteLine("Registering new EU citizen");
            Console.WriteLine(euc);
            _euCitizens.Add(euc);
            Console.ReadLine();
        }

        public static List<EUcitizen> GetAllEUcitizens()
        {
            return new List<EUcitizen>(_euCitizens);
        }
    }
}