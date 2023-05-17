using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Salaries.Controllers;
using Salaries.Domain;

namespace SalariesTests
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [Collection("SharedTestCollection")]
    public class ApiTests : IAsyncLifetime
    {
        private readonly ApiFactory _factory;
        private readonly Func<Task> _resetDatabase;

        public ApiTests(ApiFactory factory)
        {
            _factory = factory;
            _resetDatabase = factory.ResetDatabaseAsync;
        }

        [Fact]
        public async Task should_create_an_employee()
        {
            var client = _factory.HttpClient;

            var response = await client.PostAsJsonAsync("/SalaryManagement/CreateEmployee",
                new CreateEmployeeRequest() { FirstName = "Jan", LastName = "Kowalski" });
            response.EnsureSuccessStatusCode();
            var employeeId = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            var employee = _factory.GetSalariesContext().Employees.Find(employeeId);

            Assert.NotNull(employee);
            Assert.Equal("Jan", employee.FirstName);
            Assert.Equal("Kowalski", employee.LastName);
        }

        [Fact]
        public async Task should_return_all_employees()
        {
            var client = _factory.HttpClient;
            var context = _factory.GetSalariesContext();

            context.Employees.Add(new Employee("John", "Doe"));
            context.Employees.Add(new Employee("Jane", "Doe"));
            await context.SaveChangesAsync();

            var response = await client.GetAsync("SalaryManagement/Employees");
            response.EnsureSuccessStatusCode();
            var employees =
                JsonConvert.DeserializeObject<List<EmployeeResponse>>(await response.Content.ReadAsStringAsync());

            employees.Should().HaveCount(2).And.SatisfyRespectively(
                john =>
                {
                    john.FirstName.Should().Be("John");
                    john.LastName.Should().Be("Doe");
                },
                jane =>
                {
                    jane.FirstName.Should().Be("Jane");
                    jane.LastName.Should().Be("Doe");
                });
        }

        [Fact]
        public async Task employee_can_have_benefits()
        {
            var context = _factory.GetSalariesContext();

            context.Employees.Add(new Employee("John", "Doe", 1));
            context.Employees.Add(new Employee("Jane", "Doe", 3));
            context.Benefits.Add(new Benefit(300, "Multisport"));
            await context.SaveChangesAsync();

            var benefit = context.Benefits.First();
            context.Employees.ToList().ForEach(employee => employee.Benefits.Add(benefit));
            await context.SaveChangesAsync();
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync() => _resetDatabase();
    }
}