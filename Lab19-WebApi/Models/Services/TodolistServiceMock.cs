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
    /// <summary>
    /// Mock service to test Todo lists
    /// </summary>
    public class TodolistServiceMock : ITodolist
    {
        private readonly List<Todolist> _todolist;
        private readonly List<Todo> _todo;

        public TodolistServiceMock()
        {
            _todolist = new List<Todolist>()
            {
                new Todolist() { Name = "Lab", TodolistId = 1 },
                new Todolist() { Name = "Life", TodolistId = 2 },
            };
            _todo = new List<Todo>()
            {
                new Todo() { TodoId = 1, Task = "To Work", TodolistId = 2 },
                new Todo() { TodoId = 2, Task = "To get Home", TodolistId = 2  },
                new Todo() { TodoId = 3, Task = "Have a free time", TodolistId = 2  },
                new Todo() { TodoId = 4, Task = "Build Web Api", TodolistId = 1  },
                new Todo() { TodoId = 5, Task = "Test Web Api", TodolistId = 1  },
            };
        }
        public async Task DeleteTodolist(Todolist todolist)
        {
            await Task.Run(() => _todolist.Remove(todolist));
        }

        public async Task<ICollection<Todolist>> GetTodolist()
        {
            return await Task.Run(() => _todolist.ToList());
        }

        public async Task<Todolist> GetTodolist(int id)
        {
            return await Task.Run(() => _todolist.Where(t => t.TodolistId == id).FirstOrDefault());
        }

        public async Task PostTodolist(Todolist todolist)
        {
            todolist.TodolistId = _todolist.Max(t => t.TodolistId) + 1;
            _todolist.Add(todolist);
        }

        public void PutTodolist(Todolist todolist)
        {
            try
            {
                Todolist _existingTodolist = _todolist.First(t => t.TodolistId == todolist.TodolistId);
                _existingTodolist.Name = todolist.Name;
            }
            catch (Exception) { }
        }
        public async Task Commit() { }

        public bool TodolistExists(int id)
        {
            return _todolist.Any(e => e.TodolistId == id);
        }

        public Task<ICollection<Todo>> GetTodos(int id)
        {
            return Task.Run(() => (ICollection<Todo>)_todo.Where(t => t.TodolistId == id).ToList());
        }
        public async Task DeleteTodos(ICollection<Todo> todos)
        {
            foreach (Todo todo in todos)
            {
                await Task.Run(() => _todo.Remove(todo));
            }
        }
    }
}
