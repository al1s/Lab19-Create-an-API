using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab19WebApi.Models
{
    [JsonObject(IsReference = true)]
    public class Todolist
    {
        public int TodolistId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
