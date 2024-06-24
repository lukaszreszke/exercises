namespace Acme.TemplateMethod._2;

public class SourceOneDataProcessor : DataProcessor
{
    private List<SourceOneData> _data;
    private readonly SourceOneDataProvider _dataProvider;

    public SourceOneDataProcessor(SourceOneDataProvider dataProvider, DesiredDataContext context) : base(context)
    {
        _dataProvider = dataProvider;
    }

    protected override void FetchData()
    {
        _data = _dataProvider.GetData();
    }

    protected override void ProcessData()
    {
        _result = _data.Select(d => new DesiredDataStructure
        {
            BandName = d.BandName,
            Rating = d.Rating,
            AvailableDates = d.AvailableDates.Select(x => new AvailableDate
            {
                Date = x
            }).ToList()
        }).ToList();
    }
}