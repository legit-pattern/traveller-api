using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api
{
    [Route("/")]
    public class TravellerController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult<List<string>> Post([FromBody] List<string> list)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }
    }
}