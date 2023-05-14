using System.Xml.Linq;

namespace XML1;

public class Mapper
{
    public XElement Map(string data)
    {
        return new XElement("Calendar",
            XElement.Parse(data).Elements("Event").Select(evt => new XElement("Appointment",
                new XElement("Patient",
                    new XElement("Name", evt.Element("Pet").Element("Owner").Value),
                    new XElement("Pet",
                        new XElement("Name", evt.Element("Pet").Element("Name").Value),
                        new XElement("Type", evt.Element("Pet").Element("Type").Value),
                        new XElement("Age", evt.Element("Pet").Element("Age").Value)
                    )
                ),
                new XElement("DateTime", $"{evt.Element("Date").Value} {evt.Element("Time").Value}"),
                new XElement("Description", evt.Element("Title").Value)
            ))
        );
    }
}
