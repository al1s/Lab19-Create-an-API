using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lab19WebApi.Models
{
    [JsonObject(IsReference = true)]
    public class Todolist
    {
        public int TodolistId { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
