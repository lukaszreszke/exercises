namespace Acme.CarRental;

using System;
using System.Collections.Generic;
using System.Text;

public class CarRentalManager
{
    private List<Reservation> Reservations = new();
    public List<int> CarIds => Reservations.ConvertAll(reservation => reservation.CarId);

    public int RentCar(int carModel, string customer, string email, int rentalDays, DateTime rentSince = default)
    {
        if (rentSince == default)
        {
            rentSince = DateTime.Now;
        }
        
        ValidateInput(customer, email, rentalDays);
        ValidateReservation(carModel, rentSince);
        return AddReservation(carModel, customer, email, rentalDays, rentSince);
    }

    private void ValidateReservation(int carModel, DateTime rentSince)
    {
        if (Reservations.Any(r => r.CarId == carModel && (r.RentalStartDate.Date == rentSince.Date ||
                                                          r.RentalStartDate.Date.AddDays(r.RentalDurationDays) >
                                                          rentSince.Date)))
        {
            throw new ArgumentException("Car is already rented for the given date.");
        }
    }

    private int AddReservation(int carModel, string customer, string email, int rentalDays, DateTime rentSince)
    {
        var nextId = Reservations.Any() ? Reservations.Select(x => x.Id).Max() + 1 : 1;
        Reservations.Add(new Reservation(nextId, carModel, rentSince, rentalDays, customer, email, rentalDays * 100));
        return nextId;
    }

    private static void ValidateInput(string customer, string email, int rentalDays)
    {
        if (string.IsNullOrEmpty(customer) || string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Car model, customer, and email cannot be null or empty.");
        }

        if (rentalDays <= 0)
        {
            throw new ArgumentException("Rental days must be greater than zero.");
        }

        if (!email.Contains("@"))
        {
            throw new ArgumentException("Invalid email address.");
        }
    }

    public void ReturnCar(int reservationId)
    {
        HandleDueDate(reservationId);
        RemoveReservation(reservationId);
    }

    private void RemoveReservation(int carId)
    {
        Reservations.Remove(Reservations.Find(i => i.CarId == carId));
    }

    private void HandleDueDate(int carId)
    {
        DateTime dueDate = Reservations.Find(i => i.CarId == carId).RentalStartDate.AddDays(Reservations.Find(i => i.CarId == carId).RentalDurationDays); 
        if (DateTime.Now > dueDate)
        {
            TimeSpan lateDuration = DateTime.Now - dueDate;
            double lateCharge = lateDuration.Days * 20;
            Reservations.Find(i => i.CarId == carId).RentalCharge += lateCharge;
        }
    }

    public string GenerateStatement(string customer)
    {
        double totalCharge = 0;
        StringBuilder result = new StringBuilder($"Rental Record for {customer}\n");

        Reservations.ForEach(reservation =>
        {
            if (reservation.CustomerName == customer)
            {
                result.Append(
                    $"\tCar: {reservation.CarId}, Due Date: {reservation.RentalStartDate.AddDays(reservation.RentalDurationDays)}, Charge: {reservation.RentalCharge}\n");
                totalCharge += reservation.RentalCharge;
            }
        });

        result.Append($"Total charge: {totalCharge}\n");
        return result.ToString();
    }

    public void NotifyOverdueRentals()
    {
        Reservations.ForEach(reservation =>
        {
            DateTime dueDate = reservation.RentalStartDate.AddDays(reservation.RentalDurationDays);
            if (DateTime.Now > dueDate)
            {
                Console.WriteLine($"Sending overdue notice for car {reservation.CarId} to {reservation.CustomerEmail}");
            }
        });
    }
    
    public List<Car> GetAvailableCars()
    {
        return new List<Car>
        {
            new(2000, "Model S", "Tesla", 200),
            new(3000, "Model 3", "Tesla", 300),
            new(1000, "Model X", "Tesla", 100),
            new(4000, "Model Y", "Tesla", 400),
        };
    }
}

public class Car
{
    public Car(int id, string model, string brand, double dailyRate)
    {
        Id = id;
        Model = model;
        Brand = brand;
        DailyRate = dailyRate;
    }

    public int Id { get; }
    public string Model { get; }
    public string Brand { get; }
    public double DailyRate { get; }
}


public class Clock : IClock
{
    public DateTime Now => DateTime.Now;
}

public interface IClock
{
    public DateTime Now => DateTime.Now;
}