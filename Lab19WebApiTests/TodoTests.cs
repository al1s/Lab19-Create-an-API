using Lab19WebApi.Controllers;
using Lab19WebApi.Models.Interfaces;
using Lab19WebApi.Models.Services;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Lab19WebApi.Models;

namespace Lab19WebApiTests
{
    public class TodolistTest
    {
        ToDoController _controller;
        ITodo _service;

        /// <summary>
        /// Constructor to feed controller with mock service provider
        /// </summary>
        public TodolistTest()
        {
            _service = new TodolistServiceMock();
            _controller = new ToDoListController(_service);
        }
        /// <summary>
        /// Can get Ok result for GET /api/todo
        /// </summary>
        [Fact]
        public void GetReturnsOkResult()
        {
            var okResult = _controller.GetTodo().Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can get all items for GET /api/todo
        /// </summary>
        [Fact]
        public void GetReturnsAllItemsAsync()
        {
            OkObjectResult okResult = _controller.GetTodo().Result as OkObjectResult;
            List<Todo> items = Assert.IsType<List<Todo>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }
        /// <summary>
        /// Can get Not Found for unknown id for GET /api/todo/{id}
        /// </summary>
        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFound()
        {
            int nonExistingId = -1;
            var NotFoundResult = _controller.GetTodo(nonExistingId).Result;
            Assert.IsType<NotFoundResult>(NotFoundResult);
        }
        /// <summary>
        /// Can get Ok for existing id for GET /api/todo/{id}
        /// </summary>
        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            int existingId = 1;
            var okResult = _controller.GetTodo(existingId).Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can get correct item for existing id for GET /api/todo/{id}
        /// </summary>
        [Fact]
        public void GetById_ExistingIdPassed_ReturnsCorrectValue()
        {
            int existingId = 1;
            OkObjectResult okResult = _controller.GetTodo(existingId).Result as OkObjectResult;
            Todo item = Assert.IsType<Todo>(okResult.Value);
            Assert.Equal(existingId, item.TodoId);
        }
        /// <summary>
        /// Can get Bad request result if invalid object passed to POST /api/todo/
        /// </summary>
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            Todo incorrectTodo = new Todo()
            {
                Finished = true,
                Details = "Taks name is missing"
            };
            _controller.ModelState.AddModelError("Task", "The Task field is required.");
            var badRequestResult = _controller.PostTodo(incorrectTodo).Result;
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }
        /// <summary>
        /// Can get Created at action result if valid object passed to POST /api/todo/
        /// </summary>
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedAtAction()
        {
            Todo correctTodo = new Todo()
            {
                Task = "New task",
                Finished = true,
                Details = "Fully qualified task"
            };
            var createdAtActionResult = _controller.PostTodo(correctTodo).Result;
            Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        }
        /// <summary>
        /// Can get value for created item if valid object passed to POST /api/todo/
        /// </summary>
        [Fact]
        public void Add_ValidObjectPassed_ReturnsRespWithCreatedItem()
        {
            Todo correctTodo = new Todo()
            {
                Task = "New task",
                Finished = true,
                Details = "Fully qualified task",
                TodolistId = 1
            };
            CreatedAtActionResult createdAtActionResult = _controller.PostTodo(correctTodo).Result as CreatedAtActionResult;
            Todo item = Assert.IsType<Todo>(createdAtActionResult.Value);
            Assert.Equal(correctTodo.Task, item.Task);
        }
        /// <summary>
        /// Can get Bad request result if object data inconsistent with route data passed to PUT /api/todo/{id}
        /// </summary>
        [Fact]
        public void Update_InconsistentObjectPassed_ReturnsBadRequest()
        {
            Todo incorrectTodo = new Todo()
            {
                TodoId = 1,
                Task = "Update taks",
                Finished = true,
                Details = "TaksId inconsistent"
            };
            var badRequestResult = _controller.PutTodo(2, incorrectTodo).Result;
            Assert.IsType<BadRequestResult>(badRequestResult);
        }
        /// <summary>
        /// Can get Bad request result if object data invalid for PUT /api/todo/{id}
        /// </summary>
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            Todo incorrectTodo = new Todo()
            {
                TodoId = 1,
                Finished = true,
                Details = "Taks name is missing"
            };
            _controller.ModelState.AddModelError("Task", "The Task field is required.");
            var badRequestResult = _controller.PutTodo(1, incorrectTodo).Result;
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }
        /// <summary>
        /// Can get No content request result if object data valid for PUT /api/todo/{id}
        /// </summary>
        [Fact]
        public void Update_ValidObjectPassed_ReturnsNoContentResult()
        {
            Todo incorrectTodo = new Todo()
            {
                TodoId = 1,
                Task = "Update task",
                Finished = true,
                Details = "Taks name is missing"
            };
            var noContentResult = _controller.PutTodo(1, incorrectTodo).Result;
            Assert.IsType<NoContentResult>(noContentResult);
        }

        /// <summary>
        /// Can get Bad request result if invalid object passed to DELETE /api/todo/{id}
        /// </summary>
        [Fact]
        public void Delete_NonExistingObjectPassed_ReturnsNotFound()
        {
            int nonExistingId = -1;
            var notFoundResult = _controller.DeleteTodo(nonExistingId).Result;
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        /// <summary>
        /// Can get Created at action result if valid object passed to DELETE /api/todo/{id}
        /// </summary>
        [Fact]
        public void Delete_ValidObjectPassed_ReturnsOkResult()
        {
            int existingId = 1;
            var okResult = _controller.DeleteTodo(existingId).Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can't get value for deleted item for DELETE /api/todo/{id}
        /// </summary>
        [Fact]
        public void Delelte_ValidObjectPassed_ItemDeleted()
        {
            int idToDelele = 1;
            var okResult = _controller.DeleteTodo(idToDelele).Result;
            var notFoundResult = _controller.GetTodo(idToDelele).Result;
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

    }
}
