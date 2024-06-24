namespace Acme.TemplateMethod._2;

public class SourceTwoDataProcessor : DataProcessor
{
    private List<SourceTwoData> _data;
    private readonly SourceTwoDataProvider _dataProvider;

    public SourceTwoDataProcessor(SourceTwoDataProvider dataProvider, DesiredDataContext context) : base(context)
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
            Rating = (int)d.Rating,
            AvailableDates = new List<AvailableDate>() { new() { Date = d.Date } }
        }).ToList();
    }
}