using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Cpr_to_euccid
{
    class EUgateway
    {
        private readonly MessageQueue _inputCreateEuccidRecord;
        private readonly MessageQueue _inputRequestChannel;
        public EUgateway(MessageQueue input, MessageQueue inputRequestChannel)
        {
            this._inputCreateEuccidRecord = input;
            this._inputRequestChannel = inputRequestChannel;
            ReceiveMessage();
        }

        private void ReceiveMessage()
        {
            _inputCreateEuccidRecord.Formatter = new XmlMessageFormatter(new Type[] { typeof(EUcitizen) });
            //start thread?
            _inputCreateEuccidRecord.ReceiveCompleted += new ReceiveCompletedEventHandler(ProcessCreateMessage);
            _inputCreateEuccidRecord.BeginReceive();

            _inputRequestChannel.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});
            _inputRequestChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(ProcessRequestMessage);
            _inputRequestChannel.BeginReceive();
        }

        //Return message is a Documenet message 147
        private void ProcessRequestMessage(object sender, ReceiveCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ProcessCreateMessage(object sender, ReceiveCompletedEventArgs asyncResult)
        {
            Console.WriteLine("EU processing new citizen");
            var messageQueue = (MessageQueue)sender;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var euC = (EUcitizen)message.Body;
            Console.WriteLine(euC);
            Service.CreateNewEuCitizen(euC);
        }
    }
}
