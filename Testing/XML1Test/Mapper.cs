using System.Xml.Linq;
using XML1;
using Xunit.Abstractions;

namespace XML1Test;

public class MapperTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MapperTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void MapsDataCorrectly()
    {
        var mapper = new Mapper();
        string input = @"<Calendar>
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
        
        string output = @"<Calendar>
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
                            </Calendar>";
        
        _testOutputHelper.WriteLine(mapper.Map(input).ToString());
        Assert.True(XNode.DeepEquals(XElement.Parse(output), mapper.Map(input)));
    }
}