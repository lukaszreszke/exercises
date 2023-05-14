using System.Xml.Linq;
using Xunit.Abstractions;

namespace XML1Test;

public class MergeTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MergeTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Merge()
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

        string expected = @"
                            <Calendar>
                              <Appointment>
                                <Patient>
                                  <Name>John Doe</Name>
                                  <Pet>
                                    <Name>Buddy</Name>
                                    <Type>Dog</Type>
                                    <Age>3</Age>
                                  </Pet>
                                </Patient>
                                <DateTime>2023-06-15 10:00 AM</DateTime>
                                <Description>Annual Checkup</Description>
                              </Appointment>
                              <Appointment>
                                <Patient>
                                  <Name>Jane Doe</Name>
                                  <Pet>
                                    <Name>Fluffy</Name>
                                    <Type>Cat</Type>
                                    <Age>5</Age>
                                  </Pet>
                                </Patient>
                                <DateTime>2023-06-25 2:00 PM</DateTime>
                                <Description>Dental Cleaning</Description>
                              </Appointment>
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
                            </Calendar>
                            ";


        _testOutputHelper.WriteLine(new Merge().Perform(source1, XElement.Parse(source2)).ToString());
        Assert.True(XNode.DeepEquals(XElement.Parse(expected), new Merge().Perform(source1, XElement.Parse(source2))));
    }
}

public class OrderedTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public OrderedTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Order()
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

        string expected = @"
                            <Calendar>
                              <Appointment>
                                <Patient>
                                  <Name>John Doe</Name>
                                  <Pet>
                                    <Name>Buddy</Name>
                                    <Type>Dog</Type>
                                    <Age>3</Age>
                                  </Pet>
                                </Patient>
                                <DateTime>2023-06-15 10:00 AM</DateTime>
                                <Description>Annual Checkup</Description>
                              </Appointment>
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
                              <Appointment>
                                <Patient>
                                  <Name>Jane Doe</Name>
                                  <Pet>
                                    <Name>Fluffy</Name>
                                    <Type>Cat</Type>
                                    <Age>5</Age>
                                  </Pet>
                                </Patient>
                                <DateTime>2023-06-25 2:00 PM</DateTime>
                                <Description>Dental Cleaning</Description>
                              </Appointment>
                            </Calendar>
                            ";


        _testOutputHelper.WriteLine(new Ordered(new Merge().Perform(source1, XElement.Parse(source2))).Perform().ToString());
        Assert.True(XNode.DeepEquals(XElement.Parse(expected), new Ordered(new Merge().Perform(source1, XElement.Parse(source2))).Perform()));
    }
}

public class Ordered
{
    private readonly XElement _data;

    public Ordered(XElement data)
    {
        _data = data;
    }
    
    public XElement Perform()
    {
        IEnumerable<XElement> appointments = _data.Elements("Appointment");
        var sortedAppointments = appointments.OrderBy(app =>
        {
            string dateTime = app.Element("DateTime").Value;
            return DateTime.Parse(dateTime);
        });

        return new XElement("Calendar", sortedAppointments);
    }
}