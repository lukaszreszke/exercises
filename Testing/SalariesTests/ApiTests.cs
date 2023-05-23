using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
                new CreateEmployeeRequest() { FirstName = "Jan", LastName = "Kowalski", InMarketSince = "2020-01-01" });
            _factory.EnsureSuccessStatusCode(response);
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

            context.Employees.Add(new Employee("John", "Doe", DateTime.UtcNow.AddYears(-5)));
            context.Employees.Add(new Employee("Jane", "Doe", DateTime.UtcNow.AddYears(-3)));
            await context.SaveChangesAsync();

            var response = await client.GetAsync("SalaryManagement/Employees");
            _factory.EnsureSuccessStatusCode(response);
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

            context.Employees.Add(new Employee("John", "Doe", DateTime.UtcNow.AddYears(-4), 1));
            context.Employees.Add(new Employee("Jane", "Doe", DateTime.UtcNow.AddYears(-1), 3));
            context.Benefits.Add(new Benefit(300, "Multisport"));
            await context.SaveChangesAsync();

            var benefit = context.Benefits.First();
            context.Employees.ToList().ForEach(employee => employee.Benefits.Add(benefit));
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task should_create_employee_with_initial_salary()
        {
            var client = _factory.HttpClient;

            var response = await client.PostAsJsonAsync("/SalaryManagement/CreateEmployee",
                new CreateEmployeeRequest()
                    { FirstName = "Jan", LastName = "Kowalski", InMarketSince = "2019-01-01", Salary = "5,000" });
            _factory.EnsureSuccessStatusCode(response);
            var employeeId = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            var employee = _factory.GetSalariesContext().Employees.Find(employeeId);

            Assert.NotNull(employee);
            Assert.Equal("Jan", employee.FirstName);
            Assert.Equal("Kowalski", employee.LastName);
            Assert.Equal(decimal.Parse("5,000"), employee.Salary.Amount);
        }

        [Fact]
        public async Task connection_test()
        {
            // Arrange
            var client = _factory.HttpClient;

            // Act
            var response = await client.GetAsync("/SalaryManagement");

            // Assert
            _factory.EnsureSuccessStatusCode(response);
            Assert.True(_factory.GetSalariesContext().Employees.ToList().Count == 1);
        }

        [Theory]
        [InlineData(1499, 1)]
        [InlineData(1499, 2)]
        [InlineData(1499, 3)]
        [InlineData(2500, 1)]
        [InlineData(2500, 2)]
        [InlineData(2500, 3)]
        [InlineData(5000, 2)]
        public async Task assign_benefit_to_an_employee(decimal benefitValue, int grade)
        {
            var client = _factory.HttpClient;
            var response = await client.PostAsJsonAsync("/SalaryManagement/CreateEmployee",
                new CreateEmployeeRequest
                {
                    FirstName = "Jan", LastName = "Kowalski", InMarketSince = "2020-01-01"
                });
            _factory.EnsureSuccessStatusCode(response);
            var context = _factory.GetSalariesContext();
            var employeeId = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            var employee = context.Employees.Find(employeeId);
            for (int i = 1; i < grade; i++)
            {
                employee.Promote();
            }
            var benefit = new Benefit(benefitValue, "Multisport");
            context.Benefits.Add(benefit);
            await context.SaveChangesAsync();

            var addBenefitResponse = await client.PostAsJsonAsync("SalaryManagement/AddBenefitToEmployee",
                new { EmployeeId = employee.Id, BenefitId = benefit.Id });
            _factory.EnsureSuccessStatusCode(addBenefitResponse);

            var result = await _factory.GetSalariesContext().Employees.Include(x => x.Benefits)
                .FirstOrDefaultAsync(x => x.Id == employeeId);

            Assert.Single(result.Benefits);
            var theBenefit = result.Benefits.First();
            Assert.Equal(theBenefit.Id, benefit.Id);
        }

        [Theory]
        [InlineData(10000, 3)]
        public async Task cannot_assign_benefit_to_non_eligible_employee(decimal benefitValue, int grade)
        {
            var client = _factory.HttpClient;
            var response = await client.PostAsJsonAsync("/SalaryManagement/CreateEmployee",
                new CreateEmployeeRequest
                {
                    FirstName = "Jan", LastName = "Kowalski", InMarketSince = "2020-01-01"
                });
            _factory.EnsureSuccessStatusCode(response);
            var context = _factory.GetSalariesContext();
            var employeeId = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            var employee = context.Employees.Find(employeeId);
            for (int i = 1; i < grade; i++)
            {
                employee.Promote();
            }
            var benefit = new Benefit(benefitValue, "Multisport");
            context.Benefits.Add(benefit);
            await context.SaveChangesAsync();
        
            
            var addBenefitResponse = await client.PostAsJsonAsync("SalaryManagement/AddBenefitToEmployee",
                new { EmployeeId = employee.Id, BenefitId = benefit.Id });
            Assert.Equal(HttpStatusCode.BadRequest, addBenefitResponse.StatusCode);
        }
        
        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync() => _resetDatabase();
    }
}