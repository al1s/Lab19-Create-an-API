using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Interfaces
{
    public interface ITodo
    {
        /// <summary>
        /// Get all available todos 
        /// </summary>
        /// <returns>JSON object with todo lists</returns>
        Task<ICollection<Todo>> GetTodo();
        /// <summary>
        /// Get one todo 
        /// </summary>
        /// <param name="id">Identifier of the todo</param>
        /// <returns>JSON object with requested todo</returns>
        Task<Todo> GetTodo(int id);
        /// <summary>
        /// Update existing todo 
        /// </summary>
        /// <param name="todo"></param>
        void PutTodo(Todo todo);
        /// <summary>
        /// Create new todo
        /// </summary>
        /// <param name="todo">Todo to be created</param>
        /// <returns>Async task</returns>
        Task PostTodo(Todo todo);
        /// <summary>
        /// Delete todo 
        /// </summary>
        /// <param name="todo">Todo to be deleted</param>
        /// <returns>Async task</returns>
        Task DeleteTodo(Todo todo);
        /// <summary>
        /// Save any changes in data explicitly
        /// </summary>
        /// <returns>Async task</returns>
        Task Commit();
        /// <summary>
        /// Check whether todo with a given id exists
        /// </summary>
        /// <param name="id">Id of the todo</param>
        /// <returns>Boolean, true - exists, false - no.</returns>
        bool TodoExists(int id);
    }
}
