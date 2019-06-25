using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly StatelessServiceContext _context;

        private readonly IHttpContextAccessor _accessor;

        public ValuesController(StatelessServiceContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "NodeName:", _context.NodeContext.NodeName, "this is default values api" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
