﻿using Microsoft.AspNetCore.Mvc;
using TodoLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        // GET: api/<TodosController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            return Ok();
            throw new NotImplementedException();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoModel> Get(int id)
        {
           throw new NotImplementedException();
        }

        // POST api/<TodosController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/Complete")]
        public IActionResult Complete(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
