using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

namespace ToDoListApi.Controllers
{
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        [HttpGet("[controller]/get")]
        public IActionResult GetTasks()
        {
            var tasks = _toDoItemService.GetTasks();
            return Ok(tasks);
        }

        [HttpGet("[controller]/getbyid")]
        public IActionResult GetTask([FromQuery] int id)
        {
            var task = _toDoItemService.GetTaskById(id);
            if (task == null)
            {
                return NotFound("Não existe na base id: " + id);
            }
            return Ok(task);
        }

        [HttpPost("[controller]/post")]
        public IActionResult CreateTask([FromBody] ToDoItem task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            _toDoItemService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("[controller]/put")]
        public IActionResult UpdateTask([FromQuery] int id, [FromBody] ToDoItem task)
        {
            if (task == null || task.Id != id)
            {
                return BadRequest();
            }

            var existingTask = _toDoItemService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound("Não existe na base id: " + id);
            }

            _toDoItemService.UpdateTask(task);
            return NoContent();
        }

        [HttpDelete("[controller]/delete")]
        public IActionResult DeleteTask([FromQuery] int id)
        {
            var task = _toDoItemService.GetTaskById(id);
            if (task == null)
            {
                return NotFound("Não existe na base id: " + id);
            }

            _toDoItemService.DeleteTask(id);
            return NoContent();
        }
    }
}
