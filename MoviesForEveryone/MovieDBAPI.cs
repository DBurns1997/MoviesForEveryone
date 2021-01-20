using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesForEveryone
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieDBAPI : ControllerBase
    {
        // GET: api/<MovieDBAPI>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovieDBAPI>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovieDBAPI>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MovieDBAPI>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieDBAPI>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
