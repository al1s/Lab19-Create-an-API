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
    public class ToDoListController : ControllerBase
    {
        private readonly ITodolist _todolist;

        public ToDoListController(ITodolist todolist)
        {
            _todolist = todolist;
        }

        // GET: api/ToDoList
        [HttpGet]
        public async Task<IActionResult> GetTodolists()
        {
            var result = await _todolist.GetTodolist();
            return Ok(result);
        }

        // GET: api/ToDoList/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodolist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todolist = await _todolist.GetTodolist(id);

            if (todolist == null)
            {
                return NotFound();
            }

            todolist.Todos = await _todolist.GetTodos(id);
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
            _todolist.PutTodolist(todolist);
            try
            {
                await _todolist.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_todolist.TodolistExists(id))
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

            await _todolist.PostTodolist(todolist);

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

            var todolist = await _todolist.GetTodolist(id);

            if (todolist == null)
            {
                return NotFound();
            }

            todolist.Todos = await _todolist.GetTodos(id);
            await _todolist.DeleteTodos(todolist.Todos);
            await _todolist.DeleteTodolist(todolist);

            return Ok(todolist);
        }
    }
}