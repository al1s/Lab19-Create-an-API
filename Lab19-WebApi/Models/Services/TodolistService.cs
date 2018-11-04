using Lab19WebApi.Data;
using Lab19WebApi.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab19WebApi.Models.Services
{
    public class TodolistService : ITodolist
    {
        private readonly TodoListDBContext _context;
        public TodolistService(TodoListDBContext context)
        {
            _context = context;
        }
        public async Task DeleteTodolist(Todolist todolist)
        {
            _context.Todolists.Remove(todolist);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Todolist>> GetTodolist()
        {
            return await _context.Todolists.ToListAsync();
        }

        public async Task<Todolist> GetTodolist(int id)
        {
            return await _context.Todolists.FindAsync(id);
        }

        public async Task PostTodolist(Todolist todolist)
        {
            _context.Todolists.Add(todolist);
            await _context.SaveChangesAsync();
        }

        public void PutTodolist(Todolist todolist)
        {
            _context.Update(todolist);
        }
        public async Task Commit()
        {
                await _context.SaveChangesAsync();
        }

        public bool TodolistExists(int id)
        {
            return _context.Todolists.Any(e => e.TodolistId == id);
        }
        public async Task<ICollection<Todo>> GetTodos(int id)
        {
            return await _context.Todos.Where(t => t.TodolistId == id).ToListAsync();
        }
        public async Task DeleteTodos(ICollection<Todo> todos)
        {
            foreach (var todo in todos)
            {
                _context.Todos.Remove(todo);
            }
            await _context.SaveChangesAsync();
        }
    }
}
