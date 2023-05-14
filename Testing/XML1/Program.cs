using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using XML1;

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
                                        <Owner>John Doe</Owner>
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
                                        <Owner>Jane Doe</Owner>
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

        XElement calendar2 = XElement.Parse(source2);

        XElement mergedCalendar = new Merge().Perform(source1, calendar2); 

        Console.WriteLine(mergedCalendar.ToString());
    }
}

public class Merge
{
    public XElement Perform(string source1, XElement source2)
    {
        return new XElement("Calendar", new Mapper().Map(source1).Elements(), source2.Elements());
    }
}
