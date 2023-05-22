namespace TrainScheduleApp;

public static class SampleData
{
    public static void CopyMe()
    {
        TrainScheduleA();

        TrainScheduleB();
    }

    private static void TrainScheduleB()
    {
        TrainScheduleB trainScheduleB = new TrainScheduleB();
        trainScheduleB.Trains = new List<TrainB>
        {
            new TrainB
            {
                TrainId = 1,
                TrainName = "Express 1",
                Origin = "City X",
                Destination = "City Y",
                Departure = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm"),
                Arrival = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm")
            },
            new TrainB
            {
                TrainId = 2,
                TrainName = "Express 2",
                Origin = "City A",
                Destination = "City B",
                Departure = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm"),
                Arrival = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH:mm")
            },
            new TrainB
            {
                TrainId = 3,
                Origin = "City C",
                Destination = "City D",
                Departure = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH:mm"),
                Arrival = DateTime.Now.AddHours(4).ToString("yyyy-MM-dd HH:mm")
            },
            new TrainB
            {
                TrainId = 4,
                Origin = "City E",
                Destination = "City F",
                Departure = DateTime.Now.AddHours(4).ToString("yyyy-MM-dd HH:mm"),
                Arrival = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm")
            },
            new TrainB
            {
                TrainId = 5,
                TrainName = "Express 5",
                Origin = "City G",
                Destination = "City H",
                Departure = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm"),
                Arrival = DateTime.Now.AddHours(6).ToString("yyyy-MM-dd HH:mm")
            }
        };
    }

    private static void TrainScheduleA()
    {
        TrainScheduleA trainScheduleA = new TrainScheduleA();
        trainScheduleA.Trains = new List<TrainModelA>
        {
            new TrainModelA
            {
                TrainNumber = "T123",
                DepartureStation = "Station A",
                ArrivalStation = "Station B",
                DepartureTime = DateTime.Now.AddHours(1),
                ArrivalTime = DateTime.Now.AddHours(2)
            },
            new TrainModelA
            {
                TrainNumber = "T456",
                DepartureStation = "Station C",
                ArrivalStation = "Station D",
                DepartureTime = DateTime.Now.AddHours(2),
                ArrivalTime = DateTime.Now.AddHours(3)
            },
            new TrainModelA
            {
                TrainNumber = "T789",
                DepartureStation = "Station E",
                ArrivalStation = "Station F",
                DepartureTime = DateTime.Now.AddHours(3),
                ArrivalTime = DateTime.Now.AddHours(4)
            },
            new TrainModelA
            {
                TrainNumber = "T101",
                DepartureStation = "Station G",
                ArrivalStation = "Station H",
                DepartureTime = DateTime.Now.AddHours(4),
                ArrivalTime = DateTime.Now.AddHours(5)
            },
            new TrainModelA
            {
                TrainNumber = "T112",
                DepartureStation = "Station I",
                ArrivalStation = "Station J",
                DepartureTime = DateTime.Now.AddHours(5),
                ArrivalTime = DateTime.Now.AddHours(6)
            }
        };
    }
}