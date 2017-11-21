using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Buchungssystem.Repository;

namespace Buchungssystem.Webservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Raum")]
    public class RaumController : Controller
    {
        // GET: api/Raum
        [HttpGet]
        public IEnumerable<Raum> Get()
        {
            return new List<Raum>();
        }

        // GET: api/Raum/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Raum
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Raum/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
