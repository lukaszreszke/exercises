using Microsoft.AspNetCore.Mvc;

namespace ContractsManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractsController : ControllerBase
{
    private readonly ContractsContext _contractsContext;

    public ContractsController(ContractsContext contractsContext)
    {
        _contractsContext = contractsContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_contractsContext.Contracts.ToList());
    }

    [HttpPost]
    public IActionResult Post()
    {
        _contractsContext.Contracts.Add(new Contract());
        _contractsContext.SaveChanges();
        return Ok(_contractsContext.Contracts.ToList());
    }
}
