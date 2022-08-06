using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly CalendarDbContext _calendarDbContext;

        public BasicController(CalendarDbContext calendarDbContext)
        {
            _calendarDbContext = calendarDbContext;
        }
        
        // GET: api/Basic
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Basic/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            _calendarDbContext.Add(new Calendar(){ Name = "test"});
            _calendarDbContext.SaveChanges();
            return "value";
        }

        // POST: api/Basic
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Basic/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Basic/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
