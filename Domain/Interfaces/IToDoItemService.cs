using DataAccess.Entities;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IToDoItemService
    {
        IEnumerable<ToDoItem> GetTasks();
        ToDoItem GetTaskById(int id);
        ToDoItemResponse CreateTask(ToDoItemRequest task);
        ToDoItemResponse UpdateTask(ToDoItemRequest task, int id);
        void DeleteTask(int id);
    }
}
