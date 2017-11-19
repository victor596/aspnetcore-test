using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorPages.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static List<Tuple<int, string>> _data =
            new List<Tuple<int, string>>
            {
                new Tuple<int, string>(1,"value1"),
                 new Tuple<int, string>(2,"value2"),
                  new Tuple<int, string>(3,"value3"),
                   new Tuple<int, string>(4,"value4"),
                    new Tuple<int, string>(5,"value5"),
                     new Tuple<int, string>(6,"value6"),
            };
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return (from p in _data select p.Item2);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _data.FirstOrDefault(c=> c.Item1==id)?.Item2;
        }
        [HttpGet("/getid")]
        public string GetId(ValueRequest request)
        {
            return Get(request.ReqId);
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class ValueRequest
    {
        public int ReqId { get; set; }
    }
}
