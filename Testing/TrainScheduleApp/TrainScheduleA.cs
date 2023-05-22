namespace TrainScheduleApp;

public class TrainScheduleA
{
    public List<TrainModelA> Trains { get; set; }
}

public class TrainModelA
{
    public string TrainNumber { get; set; }
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}