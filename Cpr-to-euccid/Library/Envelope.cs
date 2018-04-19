using System;
using System.Messaging;
using System.Xml.Serialization;

namespace Library
{
    [Serializable()]
    [XmlRoot(elementName: "Envelope")]
    abstract class Envelope<T>
    {
        protected Envelope(T body)
        {
            Body = body;
        }
        [XmlElement]
        private T Body { get; }
    }

    [Serializable()]
    public class CommandMessage<T> : Envelope<T>
    {
        [XmlElement]
        private string ReturnAdress { get; }
        public CommandMessage(string returnAdress, T body) : base(body)
        {
            ReturnAdress = returnAdress;
        }
    }
}
