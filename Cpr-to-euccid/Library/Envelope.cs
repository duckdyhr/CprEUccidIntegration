using System;
using System.Xml.Serialization;

namespace Library
{
    [Serializable()]
    [XmlRoot(elementName: "Envelope")]
    public abstract class Envelope<T>
    {
        protected Envelope() { }
        protected Envelope(T body)
        {
            Body = body;
        }
        [XmlElement] public T Body { get; }
    }

    [Serializable()]
    [XmlRoot(elementName: "EnvelopeCommandMessage")]
    public class CommandMessage<T> : Envelope<T>
    {
        [XmlElement] public string ReturnAdress { get; }
        public CommandMessage() { }
        public CommandMessage(string returnAdress, T body) : base(body)
        {
            ReturnAdress = returnAdress;
        }
    }
}