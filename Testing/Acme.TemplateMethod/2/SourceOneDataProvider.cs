namespace Acme.TemplateMethod._2;

public class SourceOneDataProvider
{
    public List<SourceOneData> GetData()
    {
        return new List<SourceOneData>
        {
            new SourceOneData
            {
                BandName = "Band One",
                Rating = 5,
                AvailableDates = new List<DateTime>
                {
                    new DateTime(2021, 1, 1),
                    new DateTime(2021, 1, 2),
                    new DateTime(2021, 1, 3)
                }
            },
            new SourceOneData
            {
                BandName = "Band Two",
                Rating = 4,
                AvailableDates = new List<DateTime>
                {
                    new DateTime(2021, 1, 4),
                    new DateTime(2021, 1, 5),
                    new DateTime(2021, 1, 6)
                }
            }
        };
    }
}