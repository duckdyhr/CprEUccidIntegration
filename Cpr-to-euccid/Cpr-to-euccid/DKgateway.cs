using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Threading;
using Library;

namespace Cpr_to_euccid
{
    class DKgateway
    {
        private readonly MessageQueue _translatorChannel;
        private readonly MessageQueue _euRequestChannel;
        private readonly string _dkReplyChannelAddress;

        public DKgateway(MessageQueue translatorChannel, MessageQueue euRequestChannel, string dkReplyChannelAddress)
        {
            _translatorChannel = translatorChannel;
            _euRequestChannel = euRequestChannel;
            _dkReplyChannelAddress = dkReplyChannelAddress;
        }

        public void StartThread(DKcitizen dkc)
        {
            new Thread(() =>
            {
                CreateDkCitizenInEuSystem(dkc);
                
              //  Environment.Exit(-1);
            }).Start();
        }

        public void CreateDkCitizenInEuSystem(DKcitizen dkc)
        {
            //Make envelope wrapper...
            Message msg = new Message()
            {
                Label= "Danish person to euccid",
                Body = dkc
            };
            Console.WriteLine("DK sending citizen to translation");
            _translatorChannel.Send(msg);
        }

        //Needs to be a command message 145, specifying a return adress, as well as a correlation identifier
        public void RequestEUcitizen(string euccid)
        {
            var request = new Message()
            {
                Label = "Requesting EU citizen",
                Body = new CommandMessage<string>(_dkReplyChannelAddress, euccid)
            };
            Console.WriteLine("DK requesting EU citizen");
            _euRequestChannel.Send(request);
        }
    }
}
