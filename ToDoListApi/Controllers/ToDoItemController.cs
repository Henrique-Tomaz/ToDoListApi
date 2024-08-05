using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

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
                return NotFound("Não existe na base o id: " + id);
            }
            return Ok(task);
        }

        [HttpPost("[controller]/post")]
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(ToDoItemResponse))]
        public IActionResult CreateTask([FromBody] ToDoItemRequest task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            var response = _toDoItemService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), response);
        }

        [HttpPut("[controller]/put")]
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(ToDoItemResponse))]
        public IActionResult UpdateTask([FromQuery] int id, [FromBody] ToDoItemRequest task)
        {
            if (task == null || id == 0)
            {
                return BadRequest();
            }

            var existingTask = _toDoItemService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound("Não existe na base o id: " + id);
            }

            var response = _toDoItemService.UpdateTask(task, id);
            return NoContent();
        }

        [HttpDelete("[controller]/delete")]
        public IActionResult DeleteTask([FromQuery] int id)
        {
            var task = _toDoItemService.GetTaskById(id);
            if (task == null)
            {
                return NotFound("Não existe na base o id: " + id);
            }

            _toDoItemService.DeleteTask(id);
            return NoContent();
        }
    }
}
