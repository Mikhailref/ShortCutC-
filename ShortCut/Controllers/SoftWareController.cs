using Microsoft.AspNetCore.Mvc;
using ShortCut.Models.Interfaces;
using ShortCut.Utilities.DataBase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortCut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftWareController : ControllerBase
    {
        // GET: api/<SoftWareController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SoftWareController>/5
        [HttpGet("{id}")]
        public ISoftWare Get(int id)
        {
            ISoftWare softWare=DatabaseSingleton.Instance.FetchSoftWareFromDatabase(id);
            return softWare;
        }

        // POST api/<SoftWareController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SoftWareController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SoftWareController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
