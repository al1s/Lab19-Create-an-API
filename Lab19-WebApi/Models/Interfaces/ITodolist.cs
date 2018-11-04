using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Interfaces
{
    public interface ITodolist
    {
        Task<ICollection<Todolist>> GetTodolist();
        Task<Todolist> GetTodolist(int id);
        void PutTodolist(Todolist todolist);
        Task PostTodolist(Todolist todolist);
        Task DeleteTodolist(Todolist todolist);
        Task Commit();
        bool TodolistExists(int id);
        Task<ICollection<Todo>> GetTodos(int id);
        Task DeleteTodos(ICollection<Todo> todos);
    }
}
