using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Interfaces
{
    public interface ITodolist
    {
        Task<IEnumerable<Todolist>> GetTodolist();
        Task<IActionResult> GetTodolist(int id);
        Task<IActionResult> PutTodolist(int id, Todolist todo);
        Task<IActionResult> PostTodolist(Todolist todo);
        Task<IActionResult> DeleteTodolist(int id);
        Task<bool> TodolistExists(int id);
    }
}
