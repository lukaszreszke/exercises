namespace Acme.CarRental;

using System;
using System.Collections.Generic;
using System.Text;

public class CarRentalManager
{
    public List<string> Cars = new List<string>();
    public List<DateTime> RentalStartDates = new List<DateTime>();
    public List<int> RentalDurations = new List<int>();
    public List<string> Customers = new List<string>();
    public List<string> CustomerEmails = new List<string>();
    public List<double> RentalCharges = new List<double>();

    public void RentCar(string carModel, string customer, string email, int rentalDays)
    {
        if (string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(customer) || string.IsNullOrEmpty(email))
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

        Cars.Add(carModel);
        RentalStartDates.Add(DateTime.Now);
        RentalDurations.Add(rentalDays);
        Customers.Add(customer);
        CustomerEmails.Add(email);
        RentalCharges.Add(100 * rentalDays); // Initial charge is zero
    }

    public void ReturnCar(string carModel)
    {
        for (int i = 0; i < Cars.Count; i++)
        {
            if (Cars[i] == carModel)
            {
                DateTime dueDate = RentalStartDates[i].AddDays(RentalDurations[i]);
                if (DateTime.Now > dueDate)
                {
                    TimeSpan lateDuration = DateTime.Now - dueDate;
                    double lateCharge = lateDuration.Days * 20;
                    RentalCharges[i] += lateCharge;
                }

                Cars.RemoveAt(i);
                RentalStartDates.RemoveAt(i);
                RentalDurations.RemoveAt(i);
                Customers.RemoveAt(i);
                CustomerEmails.RemoveAt(i);
                RentalCharges.RemoveAt(i);
                break;
            }
        }
    }

    public string GenerateStatement(string customer)
    {
        double totalCharge = 0;
        StringBuilder result = new StringBuilder($"Rental Record for {customer}\n");

        for (int i = 0; i < Customers.Count; i++)
        {
            if (Customers[i] == customer)
            {
                result.Append($"\tCar: {Cars[i]}, Due Date: {RentalStartDates[i].AddDays(RentalDurations[i])}, Charge: {RentalCharges[i]}\n");
                totalCharge += RentalCharges[i];
            }
        }

        result.Append($"Total charge: {totalCharge}\n");
        return result.ToString();
    }

    public void NotifyOverdueRentals()
    {
        for (int i = 0; i < Cars.Count; i++)
        {
            DateTime dueDate = RentalStartDates[i].AddDays(RentalDurations[i]);
            if (DateTime.Now > dueDate)
            {
                Console.WriteLine($"Sending overdue notice for car {Cars[i]} to {CustomerEmails[i]}");
            }
        }
    }
}

public class Clock : IClock
{
    public DateTime Now => DateTime.Now;
}

public interface IClock
{
    public DateTime Now => DateTime.Now;
}
