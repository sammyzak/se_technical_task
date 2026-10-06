using System.Xml.Serialization;

namespace DealerDatabase.Import.Sources.XML.SAF
{
    [XmlRoot("SafRegister")]
    public class SafRegister
    {
        [XmlAttribute("exported")]
        public DateTime Exported { get; set; }

        [XmlElement("Member")]
        public List<SafMember> Members { get; set; } = [];
    }

    public class SafMember
    {
        [XmlAttribute("id")]
        public string Id { get; set; } = string.Empty;

        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("TradingAs")]
        public string? TradingAs { get; set; }

        [XmlElement("LegalName")]
        public string? LegalName { get; set; }

        [XmlElement("Town")]
        public string? Town { get; set; }

        [XmlElement("Postcode")]
        public string? Postcode { get; set; }

        [XmlElement("Telephone")]
        public string? Telephone { get; set; }

        [XmlElement("Website")]
        public string? Website { get; set; }

        [XmlElement("Status")]
        public string? Status { get; set; }

        [XmlElement("Expiry")]
        public string? Expiry { get; set; }
    }
}
