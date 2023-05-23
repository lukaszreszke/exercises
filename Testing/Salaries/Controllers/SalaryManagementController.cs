using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Salaries.Application;
using Salaries.Domain;
using Salaries.Infra;

namespace Salaries.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryManagementController : ControllerBase
{
    private readonly SalariesContext _salariesContext;
    private readonly EmployeeService _employeeService;
    private readonly BenefitService _benefitService;

    public SalaryManagementController(SalariesContext salariesContext, EmployeeService employeeService, BenefitService benefitService)
    {
        _salariesContext = salariesContext;
        _employeeService = employeeService;
        _benefitService = benefitService;
    }

    [HttpPost("createEmployee")]
    public IActionResult CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var employee = _employeeService.CreateEmployee(
            request.FirstName,
            request.LastName,
            DateTime.Parse(request.InMarketSince).ToUniversalTime(),
            !string.IsNullOrEmpty(request.Salary) ? new Money(request.Salary) : null);
        return Ok(employee.Id);
    }

    [HttpGet("employees")]
    public IActionResult GetEmployees()
    {
        // TODO: FIXME
        return Ok();
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.Parse("2017-01-01"));
        _salariesContext.Employees.Add(employee);
        _salariesContext.SaveChanges();
        
        return Ok(_salariesContext.Employees.ToList());
    }
}