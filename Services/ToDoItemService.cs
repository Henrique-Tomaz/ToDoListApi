using DataAccess.Entities;
using Domain.Interfaces;
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

        public void CreateTask(ToDoItem task)
        {
            _toDoItemContext.ToDoItems.Add(task);
            _toDoItemContext.SaveChangesAsync();
        }

        public void UpdateTask(ToDoItem task)
        {
            var existingTask = _toDoItemContext.ToDoItems.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Completed = true;
                _toDoItemContext.ToDoItems.Update(existingTask);
                _toDoItemContext.SaveChangesAsync();
            }
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
