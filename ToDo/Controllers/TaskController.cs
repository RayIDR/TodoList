using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<Task>> GetAll() => 
            _taskService.Get();

        [HttpGet("{id}", Name = "GetTask")]
        public ActionResult<Task> Get(string id)
        {
            var task = _taskService.Get(id);

            if(task == null)
                return NotFound();

            return task;
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            _taskService.Add(task);
            return CreatedAtAction(nameof(Create), new { id = task.id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Task taskIn)
        {

            var existingTask = _taskService.Get(id);
            if (existingTask is null)
                return NotFound();

            _taskService.Update(id, taskIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var task = _taskService.Get(id);

            if (task is null)
                return NotFound();
            
            _taskService.Delete(id);

            return NoContent();
        }
    }
}