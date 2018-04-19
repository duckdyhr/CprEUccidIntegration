using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Library
{
    [Serializable()]
    [XmlRoot(elementName: "Citizen")]
    public class DKcitizen
    {
        public DKcitizen()
        {
        }

        [XmlElement] public string FirstName { get; set; }
        [XmlElement] public string SurName { get; set; }
        [XmlElement] public string CprNr { get; set; }
        [XmlElement] public string Adress1 { get; set; }
        [XmlElement] public string Adress2 { get; set; }
        [XmlElement] public string PostalCode { get; set; }
        [XmlElement] public string City { get; set; }
        [XmlElement] public string MaritalStatus { get; set; } = $"Unmarried";
        [XmlElement] public string SpouseCpr { get; set; }
        [XmlElement] public List<string> ChildrenCpr { get; set; }
        [XmlElement] public List<string> ParentsCpr { get; set; }
        [XmlElement] public string DoctorCvr { get; set; }

        public override string ToString()
        {
            return "DK citizen " + CprNr;
        }
    }
}
