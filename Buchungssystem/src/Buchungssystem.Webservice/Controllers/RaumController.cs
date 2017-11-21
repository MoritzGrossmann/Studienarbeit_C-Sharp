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
    [Route("api/Room")]
    public class RaumController : Controller
    {
        // GET: api/Room
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return new List<Room>();
        }

        // GET: api/Room/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Room
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Room/5
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
