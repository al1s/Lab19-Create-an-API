using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Interfaces
{
    public interface ITodo
    {
        Task<ICollection<Todo>> GetTodo();
        Task<Todo> GetTodo(int id);
        void PutTodo(Todo todo);
        Task PostTodo(Todo todo);
        Task DeleteTodo(Todo todo);
        Task Commit();
        bool TodoExists(int id);
    }
}
