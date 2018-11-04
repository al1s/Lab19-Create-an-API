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
        ToDoListController _controller;
        ITodolist _serviceTodolist;

        /// <summary>
        /// Constructor to feed controller with mock service provider
        /// </summary>
        public TodolistTest()
        {
            _serviceTodolist = new TodolistServiceMock();
            _controller = new ToDoListController(_serviceTodolist);
        }
        /// <summary>
        /// Can get Ok result for GET /api/todolist
        /// </summary>
        [Fact]
        public void GetReturnsOkResult()
        {
            var okResult = _controller.GetTodolists().Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can get all items for GET /api/todolist
        /// </summary>
        [Fact]
        public void GetReturnsAllItemsAsync()
        {
            OkObjectResult okResult = _controller.GetTodolists().Result as OkObjectResult;
            List<Todolist> items = Assert.IsType<List<Todolist>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }
        /// <summary>
        /// Can get Not Found for unknown id for GET /api/todolist/{id}
        /// </summary>
        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFound()
        {
            int nonExistingId = -1;
            var NotFoundResult = _controller.GetTodolist(nonExistingId).Result;
            Assert.IsType<NotFoundResult>(NotFoundResult);
        }
        /// <summary>
        /// Can get Ok for existing id for GET /api/todolist/{id}
        /// </summary>
        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            int existingId = 1;
            var okResult = _controller.GetTodolist(existingId).Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can get correct item for existing id for GET /api/todolist/{id}
        /// </summary>
        [Fact]
        public void GetById_ExistingIdPassed_ReturnsCorrectValue()
        {
            int existingId = 1;
            OkObjectResult okResult = _controller.GetTodolist(existingId).Result as OkObjectResult;
            Todolist item = Assert.IsType<Todolist>(okResult.Value);
            Assert.Equal(existingId, item.TodolistId);
        }
        /// <summary>
        /// Can get Bad request result if invalid object passed to POST /api/todolist/
        /// </summary>
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            Todolist incorrectTodolist = new Todolist()
            {
            };
            _controller.ModelState.AddModelError("Name", "The Name field is required.");
            var badRequestResult = _controller.PostTodolist(incorrectTodolist).Result;
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }
        /// <summary>
        /// Can get Created at action result if valid object passed to POST /api/todolist/
        /// </summary>
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedAtAction()
        {
            Todolist correctTodolist = new Todolist()
            {
                Name = "New list",
            };
            var createdAtActionResult = _controller.PostTodolist(correctTodolist).Result;
            Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        }
        /// <summary>
        /// Can get value for created item if valid object passed to POST /api/todolist/
        /// </summary>
        [Fact]
        public void Add_ValidObjectPassed_ReturnsRespWithCreatedItem()
        {
            Todolist correctTodolist = new Todolist()
            {
                Name = "New task",
            };
            CreatedAtActionResult createdAtActionResult = _controller.PostTodolist(correctTodolist).Result as CreatedAtActionResult;
            Todolist item = Assert.IsType<Todolist>(createdAtActionResult.Value);
            Assert.Equal(correctTodolist.Name, item.Name);
        }
        /// <summary>
        /// Can get Bad request result if object data inconsistent with route data passed to PUT /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Update_InconsistentObjectPassed_ReturnsBadRequest()
        {
            Todolist incorrectTodolist = new Todolist()
            {
                TodolistId = 1,
            };
            var badRequestResult = _controller.PutTodolist(2, incorrectTodolist).Result;
            Assert.IsType<BadRequestResult>(badRequestResult);
        }
        /// <summary>
        /// Can get Bad request result if object data invalid for PUT /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            Todolist incorrectTodolist = new Todolist()
            {
                TodolistId = 1,
            };
            _controller.ModelState.AddModelError("Name", "The Name field is required.");
            var badRequestResult = _controller.PutTodolist(1, incorrectTodolist).Result;
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }
        /// <summary>
        /// Can get No content request result if object data valid for PUT /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Update_ValidObjectPassed_ReturnsNoContentResult()
        {
            Todolist incorrectTodolist = new Todolist()
            {
                TodolistId = 1,
                Name = "Update list",
            };
            var noContentResult = _controller.PutTodolist(1, incorrectTodolist).Result;
            Assert.IsType<NoContentResult>(noContentResult);
        }

        /// <summary>
        /// Can get Bad request result if invalid object passed to DELETE /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Delete_NonExistingObjectPassed_ReturnsNotFound()
        {
            int nonExistingId = -1;
            var notFoundResult = _controller.DeleteTodolist(nonExistingId).Result;
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        /// <summary>
        /// Can get Created at action result if valid object passed to DELETE /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Delete_ValidObjectPassed_ReturnsOkResult()
        {
            int existingId = 1;
            var okResult = _controller.DeleteTodolist(existingId).Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Can't get value for deleted item for DELETE /api/todolist/{id}
        /// </summary>
        [Fact]
        public void Delete_ValidObjectPassed_ItemsDeleted()
        {
            int idToDelele = 1;
            var okResult = _controller.DeleteTodolist(idToDelele).Result;
            var notFoundResult = _controller.GetTodolist(idToDelele).Result;
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

    }
}
