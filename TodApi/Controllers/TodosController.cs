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
        private readonly ILogger<TodosController> _logger;

        public TodosController(ITodoData data, ILogger<TodosController> logger)
        {
            _data = data;
            _logger = logger;
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
            _logger.LogInformation("GET: api/Todos");

            try
            {
                var output = await _data.GetAllAssigned(GetUserId());
                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Get call to api/Todos failed.");
                return BadRequest();
            }

            
        }

        // GET api/<TodosController>/5
        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoModel>> Get(int todoId)
        {
            _logger.LogInformation("GET: api/Todos/{TodoId}", todoId);
            try
            {
                var output = await _data.GetOneAssigned(GetUserId(), todoId);

                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Get call to {ApiPath} failed. The Id was {TodoId}", $"api/Todos/Id", todoId);
                return BadRequest();
            }         
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
        {
            _logger.LogInformation("POST: api/Todos (Task: {Task}", task);

            try
            {
                var output = await _data.Create(GetUserId(), task);

                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The POST call to api/Todos failed. Task value was {Task}", task);
                return BadRequest();
            }
        }

        // PUT api/<TodosController>/5
        [HttpPut("{todoId}")]
        public async Task<ActionResult> Put(int todoId, [FromBody] string task)
        {
            _logger.LogInformation("PUT: api/Todos/{TodoId} (Task: {Task})", todoId, task);

            try
            {
                await _data.UpdateTask(GetUserId(), todoId, task);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The PUT call to api/Todos/{TodoId} failed. Task value was {Task}", todoId, task);
                return BadRequest();
            }
        }

        [HttpPut("{todoId}/Complete")]
        public async Task<IActionResult> Complete(int todoId)
        {
            _logger.LogInformation("PUT: api/Todos/{TodoId}/Complete", todoId);

            try
            {
                await _data.CompleteTodo(GetUserId(), todoId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The PUT call to api/Todos/{TodoId}/Complete failed.", todoId);
                return BadRequest();
            }
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int todoId)
        {
            _logger.LogInformation("DELETE: api/Todos/{TodoId}/Complete", todoId);
            try
            {
                await _data.Delete(GetUserId(), todoId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The DELETE call to api/Todos/{TodoId} failed.", todoId);
                return BadRequest();
            }
        }
    }
}
