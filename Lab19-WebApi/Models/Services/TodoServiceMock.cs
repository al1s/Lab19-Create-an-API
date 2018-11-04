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
    public class TodoServiceMock : ITodo
    {
        private readonly List<Todo> _todo;

        public TodoServiceMock()
        {
            _todo = new List<Todo>()
            {
                new Todo() { TodoId = 1, Task = "To Work", TodolistId = 2 },
                new Todo() { TodoId = 2, Task = "To get Home", TodolistId = 2  },
                new Todo() { TodoId = 3, Task = "Have a free time", TodolistId = 2  },
                new Todo() { TodoId = 4, Task = "Build Web Api", TodolistId = 1  },
                new Todo() { TodoId = 5, Task = "Test Web Api", TodolistId = 1  },
            };
        }
        public async Task DeleteTodo(Todo todo)
        {
            await Task.Run(() => _todo.Remove(todo));
        }

        public async Task<ICollection<Todo>> GetTodo()
        {
            return await Task.Run(() => _todo.ToList());
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await Task.Run(() => _todo.Where(t => t.TodoId == id).FirstOrDefault());
        }

        public async Task PostTodo(Todo todo)
        {
            todo.TodoId = _todo.Max(t => t.TodoId) + 1;
            _todo.Add(todo);
        }

        public void PutTodo(Todo todo)
        {
            Todo _existingTodo = _todo.First(t => t.TodoId == todo.TodoId);
            _existingTodo.Task = todo.Task;
            _existingTodo.Finished = todo.Finished;
            _existingTodo.TodolistId = todo.TodolistId;
        }
        public async Task Commit() { }

        public bool TodoExists(int id)
        {
            return _todo.Any(e => e.TodoId == id);
        }
    }
}
