using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreTest2.model;
using CoreTest2.data;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreTest2.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        // GET: api/blog
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return PostgresData.getPosts();
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Post result = PostgresData.getPost(id);
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        // POST api/blog
        [HttpPost]
        public IActionResult Post([FromBody]JToken value)
        {
            Post post = JsonConvert.DeserializeObject<Post>(value.ToString());
            PostgresData.insertPost(post);
            return Ok();
        }
    }
}
