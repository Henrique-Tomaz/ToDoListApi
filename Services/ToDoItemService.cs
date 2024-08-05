using DataAccess.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ToDoItemContext _toDoItemContext;
        public ToDoItemService(ToDoItemContext toDoItemContext)
        {
            _toDoItemContext = toDoItemContext;
        }
        public IEnumerable<ToDoItem> GetTasks()
        {
            
            return _toDoItemContext.ToDoItems.ToList();
        }

        public ToDoItem GetTaskById(int id)
        {
            var _task = _toDoItemContext.ToDoItems.FirstOrDefault(i=> i.Id == id);
            return _task;
        }

        public ToDoItemResponse CreateTask(ToDoItemRequest task)
        {
            ToDoItemResponse response = new ToDoItemResponse();
            ToDoItem toDoItem = new ToDoItem() { 
                Title = task.Title,
                Description = task.Description,
                Completed = task.Completed };
            _toDoItemContext.ToDoItems.Add(toDoItem);
            _toDoItemContext.SaveChanges();

            response.Id = toDoItem.Id;
            response.Title = toDoItem.Title;
            response.Description = toDoItem.Description;
            response.Completed = toDoItem.Completed;
            return response;
        }

        public ToDoItemResponse UpdateTask(ToDoItemRequest task, int id)
        {
            ToDoItemResponse response = new ToDoItemResponse();
            var existingTask = _toDoItemContext.ToDoItems.FirstOrDefault(t => t.Id == id);
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Completed = true;
            _toDoItemContext.ToDoItems.Update(existingTask);
            _toDoItemContext.SaveChanges();

            response.Id = id;
            response.Title = existingTask.Title;
            response.Description = existingTask.Description;
            response.Completed = existingTask.Completed;
            return response;
        }

        public void DeleteTask(int id)
        {
            var task = _toDoItemContext.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _toDoItemContext.ToDoItems.Remove(task);
                _toDoItemContext.SaveChangesAsync();
            }
        }
    }
}
