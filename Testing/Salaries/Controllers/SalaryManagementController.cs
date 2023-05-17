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

    public SalaryManagementController(SalariesContext salariesContext, EmployeeService employeeService)
    {
        _salariesContext = salariesContext;
        _employeeService = employeeService;
    }

    [HttpPost("createEmployee")]
    public IActionResult CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var employee = _employeeService.CreateEmployee(request.FirstName, request.LastName);
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
        var employee = new Employee("Jan", "Kowalski");
        _salariesContext.Employees.Add(employee);
        _salariesContext.Salaries.Add(new Salary());
        _salariesContext.SaveChanges();
        
        return Ok(_salariesContext.Salaries.ToList());
    }
}