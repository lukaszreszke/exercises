using System.Globalization;

namespace XML1Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        string value1 = "1.0";
        string value2 = "1.00";
        var value3 = 1.0m;
        var value4 = 1.00m;

        Assert.Equal(decimal.Parse(value1, CultureInfo.InvariantCulture), decimal.Parse(value2, CultureInfo.InvariantCulture));
        Assert.Equal(value3, value4);
        Assert.Equal(decimal.Parse("1,0", CultureInfo.CurrentCulture), decimal.Parse("1.0", CultureInfo.CurrentCulture));
    }
}