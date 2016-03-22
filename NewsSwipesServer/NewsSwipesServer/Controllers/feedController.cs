using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSwipesServer.Models;

namespace NewsSwipesServer.Controllers
{
    public class feedController : ApiController
    {
        // GET api/feed
        public Article Get()
        {
            return new Article() { Url = "Url", Heading = "Heading", ImageUrl = "ImageUrl"};
        }

        // GET api/feed/5
        public string Get(int id)
        {
            return "feed";
        }

        // POST api/feed
        public void Post([FromBody]string value)
        {
        }

        // PUT api/feed/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/feed/5
        public void Delete(int id)
        {
        }
    }
}
