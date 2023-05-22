namespace TrainScheduleApp;

public class TrainScheduleB
{
    public List<TrainB> Trains { get; set; }

    public TrainScheduleB()
    {
        Trains = new List<TrainB>();
    }
}

public class TrainB
{
    public int TrainId { get; set; }
    public string? TrainName { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
}