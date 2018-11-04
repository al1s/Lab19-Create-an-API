using Lab19WebApi.Data;
using Lab19WebApi.Models;
using Lab19WebApi.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab19WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        //private readonly TodoListDBContext _context;
        private readonly ITodo _todos;


        public ToDoController(ITodo todos)
        {
            _todos = todos;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<IActionResult> GetTodo()
        {
            var result = await _todos.GetTodo();
            return Ok(result);
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _todos.GetTodo(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo([FromRoute] int id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.TodoId)
            {
                return BadRequest();
            }

            _todos.PutTodo(todo);

            try
            {
                await _todos.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_todos.TodoExists(id))
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

        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            if (todo.TodoId != 0)
            {
                ModelState.AddModelError("Error", "Cannot insert explicit TodoId value");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _todos.PostTodo(todo);

            return CreatedAtAction("GetTodo", new { id = todo.TodoId }, todo);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _todos.GetTodo(id);

            if (todo == null)
            {
                return NotFound();
            }

            //_context.Todos.Remove(todo);
            //await _context.SaveChangesAsync();
            await _todos.DeleteTodo(todo);
            return Ok(todo);
        }
    }
}