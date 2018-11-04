using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Interfaces
{
    public interface ITodolist
    {
        /// <summary>
        /// Get all available todo lists
        /// </summary>
        /// <returns>JSON object with todo lists</returns>
        Task<ICollection<Todolist>> GetTodolist();
        /// <summary>
        /// Get one todo list
        /// </summary>
        /// <param name="id">Identifier of the todo list</param>
        /// <returns>JSON object with requested todo list</returns>
        Task<Todolist> GetTodolist(int id);
        /// <summary>
        /// Update existing todo list
        /// </summary>
        /// <param name="todolist"></param>
        void PutTodolist(Todolist todolist);
        /// <summary>
        /// Create new todolist
        /// </summary>
        /// <param name="todolist">Todo list to be created</param>
        /// <returns>Async task</returns>
        Task PostTodolist(Todolist todolist);
        /// <summary>
        /// Delete todo list and all associated todos
        /// </summary>
        /// <param name="todolist">Todo list to be deleted</param>
        /// <returns>Async task</returns>
        Task DeleteTodolist(Todolist todolist);
        /// <summary>
        /// Save any changes in data explicitly
        /// </summary>
        /// <returns>Async task</returns>
        Task Commit();
        /// <summary>
        /// Check whether todo list with a given id exists
        /// </summary>
        /// <param name="id">Id of the list</param>
        /// <returns>Boolean, true - exists, false - no.</returns>
        bool TodolistExists(int id);
        /// <summary>
        /// Get all todos for a list
        /// </summary>
        /// <param name="id">Id of the list</param>
        /// <returns>Collection of todos</returns>
        Task<ICollection<Todo>> GetTodos(int id);
        /// <summary>
        /// Delete todos of a list
        /// </summary>
        /// <param name="todos">Collection of todos</param>
        /// <returns>Async taks</returns>
        Task DeleteTodos(ICollection<Todo> todos);
    }
}
