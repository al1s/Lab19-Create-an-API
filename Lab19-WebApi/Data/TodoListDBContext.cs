using Lab19WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab19WebApi.Data
{
    public class TodoListDBContext : DbContext
    {
        /// <summary>
        /// Constructor for DB context
        /// </summary>
        /// <param name="options">Options for DB context</param>
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options) : base(options) { }
        /// <summary>
        /// Todos table
        /// </summary>
        public DbSet<Todo> Todos { get; set; }
        /// <summary>
        /// TodoLists table
        /// </summary>
        public DbSet<Todolist> Todolists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasData(
                new Todo { TodoId = 1, Task = "Make DB migration", Finished = false, TodolistId = 1 },
                new Todo { TodoId = 2, Task = "Implement controllers", Finished = false, TodolistId = 1  },
                new Todo { TodoId = 3, Task = "Write tests", Finished = false, TodolistId = 1  },
                new Todo { TodoId = 4, Task = "Play with kids", Finished = false, TodolistId = 2  }
            );

            modelBuilder.Entity<Todolist>().HasData(
                new Todolist { TodolistId = 1, Name = "Lab19" },
                new Todolist { TodolistId = 2, Name = "Family" }
            );
        }
    }
}
