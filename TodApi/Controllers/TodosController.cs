using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoData _data;
        public TodosController(ITodoData data)
        {
            _data = data;
            
        }

        private int GetUserId()
        {
            var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdText);
        }


        // GET: api/<TodosController>
        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get()
        {
            
            var output = await _data.GetAllAssigned(GetUserId());

            return Ok(output);  
        }

        // GET api/<TodosController>/5
        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoModel>> Get(int todoId)
        {
            var output = await _data.GetOneAssigned(GetUserId(), todoId);

            return Ok(output);  
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
        {
            var output = await _data.Create(GetUserId(), task);

            return Ok(output);
        }

        // PUT api/<TodosController>/5
        [HttpPut("{todoId}")]
        public async Task<ActionResult> Put(int todoId, [FromBody] string task)
        {
            await _data.UpdateTask(GetUserId(), todoId, task);

            return Ok();
        }

        [HttpPut("{todoId}/Complete")]
        public async Task<IActionResult> Complete(int todoId)
        {
            await _data.CompleteTodo(GetUserId(), todoId);

            return Ok();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int todoId)
        {
            await _data.Delete(GetUserId(), todoId);

            return Ok();
        }
    }
}
