namespace TrainScheduleApp;

public class ResultTrainSchedule
{
    public List<ResultTrainItem> Trains { get; set; }

    public ResultTrainSchedule()
    {
        Trains = new List<ResultTrainItem>();
    }
    
    public class ResultTrainItem
    {
        public string TrainName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}