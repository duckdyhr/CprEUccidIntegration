using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    [Serializable(), XmlRoot("citizen")]
    public class EUcitizen
    {
        public EUcitizen() { }
        [XmlElement]
        //Omdøb til eu-ccid til xml filen
        public string Euccid { get; set; }
        [XmlElement]
        public string ChristianName { get; set; }
        [XmlElement]
        public string FamilyName { get; set; }
        [XmlElement]
        public string Gender { get; set; }
        [XmlElement]
        public string StreetAndHouseNo { get; set; }
        [XmlElement]
        public string ApartmentNo { get; set; }
        [XmlElement]
        public string County { get; set; }
        [XmlElement]
        public string City { get; set; }
        [XmlElement]
        public string BirthCountry { get; set; }
        [XmlElement]
        public string CurrentCountry { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Euccid);
            sb.Append(", ");
            sb.Append(ChristianName);
            sb.Append(", ");
            sb.Append(FamilyName);
            sb.Append(", ");
            sb.Append(Gender);
            sb.Append(", ");
            sb.Append(StreetAndHouseNo);
            sb.Append(", ");
            sb.Append(ApartmentNo);
            sb.Append(", ");
            sb.Append(County);
            sb.Append(", ");
            sb.Append(City);
            sb.Append(", ");
            sb.Append(BirthCountry);
            sb.Append(", ");
            sb.Append(CurrentCountry);
            return sb.ToString();
        }
    }

    public enum Gender
    {
        Unidentified,
        Male,
        Female
    }

}
