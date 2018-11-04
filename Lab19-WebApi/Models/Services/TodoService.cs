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
    public class TodoService : ITodo
    {
        private readonly TodoListDBContext _context;
        public TodoService(TodoListDBContext context)
        {
            _context = context;
        }
        public async Task DeleteTodo(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Todo>> GetTodo()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task PostTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public void PutTodo(Todo todo)
        {
            _context.Update(todo);
        }
        public async Task Commit()
        {
                await _context.SaveChangesAsync();
        }

        public bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}
