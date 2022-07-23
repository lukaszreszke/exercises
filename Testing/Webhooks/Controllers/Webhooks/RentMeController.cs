using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webhooks.Infrastructure;

namespace Webhooks.Controllers.Webhooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentMeController : ControllerBase
    {
        private readonly IRentMeClient _rentMeClient;
        private readonly RentalIncomeDbContext _dbContext;

        public RentMeController(IRentMeClient rentMeClient, RentalIncomeDbContext dbContext)
        {
            _rentMeClient = rentMeClient;
            _dbContext = dbContext;
        }
        
        [HttpPost]
        public async Task Post([FromBody] ItemRented webhookData)
        {
            var id = webhookData.Id;
            var income = await _rentMeClient.GetIncomeFromRental(id);
            var rentalIncome = new RentalIncome(income.Income, webhookData.Since, webhookData.Until, id.ToString());

            _dbContext.Add(rentalIncome);
            await _dbContext.SaveChangesAsync();
        }
    }

    public interface IRentMeClient
    {
        Task<ItemIncome> GetIncomeFromRental(Guid id);
    }

    public record ItemIncome(Guid Id, decimal Income);
    public record ItemRented(Guid Id, DateTime Since, DateTime Until);
}
