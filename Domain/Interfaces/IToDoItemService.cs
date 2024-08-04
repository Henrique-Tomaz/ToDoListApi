using DataAccess.Entities;

namespace Domain.Interfaces
{
    public interface IToDoItemService
    {
        IEnumerable<ToDoItem> GetTasks();
        ToDoItem GetTaskById(int id);
        void CreateTask(ToDoItem task);
        void UpdateTask(ToDoItem task);
        void DeleteTask(int id);
    }
}
