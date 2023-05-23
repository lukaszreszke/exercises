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
    
    [HttpPost("AddBenefitToEmployee")]
    public IActionResult AddBenefitToEmployee([FromBody] AddBenefitToEmployeeRequest request)
    {
        _benefitService.AddBenefitForEmployee(request.EmployeeId, request.BenefitId);
        return Ok();
    }


    [HttpGet("employees")]
    public IActionResult GetEmployees()
    {
        var employees = _salariesContext.Employees.ToList();
        return Ok(employees);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var employee = new Employee("Jan", "Kowalski", DateTime.Parse("2017-01-01").ToUniversalTime());
        _salariesContext.Employees.Add(employee);
        _salariesContext.SaveChanges();
        
        return Ok(_salariesContext.Employees.ToList());
    }
}

public class AddBenefitToEmployeeRequest
{
    public int EmployeeId { get; set; }
    public int BenefitId { get; set; }
}