using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        string source1 = @"<Calendar>
                                <Event>
                                    <Title>Annual Checkup</Title>
                                    <Date>2023-06-15</Date>
                                    <Time>10:00 AM</Time>
                                    <Pet>
                                        <Name>Buddy</Name>
                                        <Type>Dog</Type>
                                        <Age>3</Age>
                                    </Pet>
                                </Event>
                                <Event>
                                    <Title>Dental Cleaning</Title>
                                    <Date>2023-06-25</Date>
                                    <Time>2:00 PM</Time>
                                    <Pet>
                                        <Name>Fluffy</Name>
                                        <Type>Cat</Type>
                                        <Age>5</Age>
                                    </Pet>
                                </Event>
                            </Calendar>";

        string source2 = @"<Calendar>
                                <Appointment>
                                    <Patient>
                                        <Name>John Doe</Name>
                                        <Pet>
                                            <Name>Rex</Name>
                                            <Type>Dog</Type>
                                            <Age>2</Age>
                                        </Pet>
                                    </Patient>
                                    <DateTime>2023-06-18 11:00 AM</DateTime>
                                    <Description>Follow-up appointment</Description>
                                </Appointment>
                                <Appointment>
                                    <Patient>
                                        <Name>Jane Smith</Name>
                                        <Pet>
                                            <Name>Whiskers</Name>
                                            <Type>Cat</Type>
                                            <Age>4</Age>
                                        </Pet>
                                    </Patient>
                                    <DateTime>2023-06-22 3:00 PM</DateTime>
                                    <Description>New patient exam</Description>
                                </Appointment>
                            </Calendar>";

        XElement calendar1 = XElement.Parse(source1);
        XElement calendar2 = XElement.Parse(source2);

        XElement transformedCalendar1 = new XElement("Calendar",
            calendar1.Elements("Event").Select(evt => new XElement("Appointment",
                new XElement("Patient",
                    new XElement("Name", evt.Element("Pet").Element("Name").Value),
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

        XElement mergedCalendar = new XElement("Calendar", transformedCalendar1.Elements(), calendar2.Elements());

        Console.WriteLine(mergedCalendar.ToString());
    }
}