using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab19WebApi.Models
{
    public class Todolist
    {
        public int TodolistId { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
