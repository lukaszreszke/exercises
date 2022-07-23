using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webhooks.Controllers.Webhooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcmeStoreController : ControllerBase
    {
        // GET: api/AcmeStore
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AcmeStore/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AcmeStore
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AcmeStore/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/AcmeStore/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
