namespace Lab19WebApi.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public string Task { get; set; }
        public string Details { get; set; }
        public bool Finished { get; set; }
        public int TodolistId { get; set; }
        public Todolist Todolist { get; set; }
    }
}
