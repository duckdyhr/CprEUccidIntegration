using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Library;

namespace Cpr_to_euccid
{
    class Translator
    {
        private readonly MessageQueue _inputChannel;
        private readonly MessageQueue _outputChannel;
        //Must take into consideration whether it's a baby citizen or one with an upgraded cpr/euccid
        public Translator(MessageQueue input, MessageQueue output)
        {
            _inputChannel = input;
            _outputChannel = output;
            ReceiveMessage();
        }

        private void ReceiveMessage()
        {
            _inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(DKcitizen) });
            //start thread?
            _inputChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(ProcessMessage);
            _inputChannel.BeginReceive();
        }

        private void ProcessMessage(object source, ReceiveCompletedEventArgs asyncResult)
        {
            Console.WriteLine("Translator process message");
            var messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var dkC = (DKcitizen)message.Body;
            Console.WriteLine(dkC);
            var euC = Translator.TranslateDKtoEu(dkC);
            var outMsg = new Message()
            {
                Label = "DK to EU converted citizen",
                Body = euC
            };
            Console.WriteLine(euC);
            Console.ReadLine();
            _outputChannel.Send(outMsg);
            _inputChannel.BeginReceive();
        }

        public static EUcitizen TranslateDKtoEu(DKcitizen dkCitizen)
        {
            var euCitizen = new EUcitizen();
            if (dkCitizen.CprNr.Length < 13)
            {
                euCitizen.Euccid = dkCitizen + "42";
            }
            euCitizen.ChristianName = dkCitizen.FirstName;
            euCitizen.FamilyName = dkCitizen.SurName;
            euCitizen.Gender = Convert.ToInt32(dkCitizen.CprNr[10]) % 2 == 0 ? "Female" : "Male";

            euCitizen.StreetAndHouseNo = dkCitizen.Adress1;
            //Muligt at lave en flottere, mere komples konvertering...
            if (dkCitizen.Adress2 != null)
            {
                var q = Service.GetApartmentNos().Any(a => dkCitizen.Adress2.Contains(a));
                if (q)
                {
                    euCitizen.ApartmentNo = dkCitizen.Adress2;
                }
                else
                {
                    euCitizen.StreetAndHouseNo += " ";
                    euCitizen.StreetAndHouseNo += dkCitizen.Adress2;
                }
            }

            var county = "Region ";
            county += Convert.ToInt32(dkCitizen.PostalCode) < 3000 ? "Hovedstaden" : "Ikke Hovedstaden";
            euCitizen.County = county;

            euCitizen.City = dkCitizen.PostalCode + " " + dkCitizen.City;
            euCitizen.CurrentCountry = "Denmark";
            return euCitizen;
        }
    }
}
