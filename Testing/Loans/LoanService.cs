using Serilog;

namespace Loans;

public static class MessageBus
{
    public static void Publish(object message)
    {
        // publish message to message bus
    }
}

public class LoanService
{
    private readonly ILogger _logger;
    private readonly Database _database;

    public LoanService(ILogger logger, Database database)
    {
        _logger = logger;
        _database = database;
    }

    public bool TakeLoan(CustomerId customerId, decimal amount)
    {
        bool decision = false;
        _logger.Information($"Looking for customer with ID {customerId} in the database");
        var customer = _database.FindById(customerId);

        if (customer == null)
        {
            _logger.Information("Customer not found. Creating new.");
            customer = new Customer();
        }

        _logger.Information("Checking whether customer can take loan");
        if (customer.Status == "REGULAR")
        {
            if (amount < 7000)
            {
                decision = true;
                _database.SaveChanges();
                new Bik().Update(customer);
                return decision;
            }

            var currentLoan = new Bik().GetCurrentLoans(customerId);

            if (currentLoan + amount > SAFE_VALUE)
            {
                MessageBus.Publish(new ManualInterventionRequired(customerId, amount));
                return false;
            }
            else
            {
                decision = true;
                customer.Loan = customer.Loan + amount;
                _database.SaveChanges();
                new Bik().Update(customer);
                return decision;
            }
        }
        else if (customer.Status == "VIP")
        {
            if (amount < 15000)
            {
                decision = true;
                _database.SaveChanges();
                new Bik().Update(customer);
                return decision;
            }

            var currentLoan = new Bik().GetCurrentLoans(customerId);

            if (currentLoan + amount > SAFE_VALUE * (decimal)1.2)
            {
                MessageBus.Publish(new ManualInterventionRequired(customerId, amount));
                return false;
            }
            else
            {
                decision = true;
                customer.Loan = customer.Loan + amount;
                _database.SaveChanges();
                new Bik().Update(customer);
                return decision;
            }
        }

        _logger.Information($"Decision for customer: {customerId} is {decision}");
        return decision;
    }

    private decimal SAFE_VALUE = Configuration.GetSafeValue();
}

internal class Configuration
{
    public static decimal GetSafeValue()
    {
        return new decimal(40000.00);
    }

    public static DateTime GetPreferedEmailReceivalTimeFor(User reader)
    {
        throw new NotImplementedException();
    }
}

public class User
{
    public Guid Id { get; }
    public object Email { get; set; }

    public User(Guid id)
    {
        Id = id;
    }
}

public class ManualInterventionRequired
{
    public ManualInterventionRequired(CustomerId customerId, decimal amount)
    {
        throw new NotImplementedException();
    }
}

public class Bik
{
    public void Update(Customer customer)
    {
        throw new NotImplementedException();
    }

    public decimal GetCurrentLoans(CustomerId customerId)
    {
        throw new NotImplementedException();
    }
}

public class Database
{
    public Customer FindById(CustomerId id)
    {
        return new Customer();
    }

    public void SaveChanges()
    {
    }
}

public class CustomerId
{
    public Guid Value { get; set; }
}

public class Customer
{
    public string Status { get; set; }
    public decimal Loan { get; set; }
}