using Lab19WebApi.Data;
using Lab19WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab19WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly TodoListDBContext _context;

        public ToDoListController(TodoListDBContext context)
        {
            _context = context;
        }

        // GET: api/ToDoList
        [HttpGet]
        public IEnumerable<Todolist> GetTodolists()
        {
            return _context.Todolists;
        }

        // GET: api/ToDoList/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodolist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todolist = await _context.Todolists.FindAsync(id);

            if (todolist == null)
            {
                return NotFound();
            }

            todolist.Todos = await _context.Todos.Where(t => t.TodolistId == id).ToListAsync();
            return Ok(todolist);
        }

        // PUT: api/ToDoList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodolist([FromRoute] int id, [FromBody] Todolist todolist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todolist.TodolistId)
            {
                return BadRequest();
            }
            _context.Update(todolist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodolistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoList
        [HttpPost]
        public async Task<IActionResult> PostTodolist([FromBody] Todolist todolist)
        {
            if (todolist.TodolistId != 0)
            {
                ModelState.AddModelError("Error", "Cannot insert explicit TodolistId value");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Todolists.Add(todolist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodolist", new { id = todolist.TodolistId }, todolist);
        }

        // DELETE: api/ToDoList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodolist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todolist = await _context.Todolists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }

            todolist.Todos = await _context.Todos.Where(t => t.TodolistId == id).ToListAsync();
            foreach (var todo in todolist.Todos)
            {
                _context.Todos.Remove(todo);
            }
            _context.Todolists.Remove(todolist);
            await _context.SaveChangesAsync();

            return Ok(todolist);
        }

        private bool TodolistExists(int id)
        {
            return _context.Todolists.Any(e => e.TodolistId == id);
        }
    }
}