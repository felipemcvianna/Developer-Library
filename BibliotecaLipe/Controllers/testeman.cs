using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaLipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testeman : ControllerBase
    {
        // GET: api/<testeman>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<testeman>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<testeman>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<testeman>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<testeman>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
