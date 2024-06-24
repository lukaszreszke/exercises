namespace Acme.TemplateMethod._2;

public class SourceTwoDataProvider
{
    public List<SourceTwoData> GetData()
    {
        return new List<SourceTwoData>
        {
            new SourceTwoData
            {
                BandName = "Band Three",
                Rating = SourceTwoRating.Excellent
            },
            new SourceTwoData
            {
                BandName = "Band Four",
                Rating = SourceTwoRating.Good
            }
        };
    }
}