using System.Xml.Serialization;

[XmlRoot(ElementName = "Travelerinformation")]
public class Travelerinformation
{

    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "email")]
    public string Email { get; set; }

    [XmlElement(ElementName = "adderes")]
    public string Adderes { get; set; }

    [XmlElement(ElementName = "createdat")]
    public DateTime Createdat { get; set; }
}

[XmlRoot(ElementName = "travelers")]
public class Travelers
{

    [XmlElement(ElementName = "Travelerinformation")]
    public List<Travelerinformation> Travelerinformation { get; set; }
}

[XmlRoot(ElementName = "TravelerinformationResponse")]
public class TravelerinformationResponse
{

    [XmlElement(ElementName = "page")]
    public int Page { get; set; }

    [XmlElement(ElementName = "per_page")]
    public int PerPage { get; set; }

    [XmlElement(ElementName = "totalrecord")]
    public int Totalrecord { get; set; }

    [XmlElement(ElementName = "total_pages")]
    public int TotalPages { get; set; }

    [XmlElement(ElementName = "travelers")]
    public Travelers Travelers { get; set; }

    [XmlAttribute(AttributeName = "xsd")]
    public string Xsd { get; set; }

    [XmlAttribute(AttributeName = "xsi")]
    public string Xsi { get; set; }

    [XmlText]
    public string Text { get; set; }
}

