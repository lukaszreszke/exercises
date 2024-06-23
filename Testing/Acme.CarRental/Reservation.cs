namespace Acme.CarRental;

internal class Reservation
{
    public Reservation(int id, int carId, DateTime rentalStartDate, int rentalDurationDays, string customerName,
        string customerEmail, double rentalCharge)
    {
        Id = id;
        CarId = carId;
        RentalStartDate = rentalStartDate;
        RentalDurationDays = rentalDurationDays;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        RentalCharge = rentalCharge;
    }

    public int Id { get; }
    public int CarId { get; }
    public DateTime RentalStartDate { get; }
    public int RentalDurationDays { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public double RentalCharge { get; set;  }
}