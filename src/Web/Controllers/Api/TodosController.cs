using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Controllers.Api;
using MyWebAPITemplate.Web.Interfaces;
using MyWebAPITemplate.Web.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MyWebAPITemplate.Controllers.Api
{
    public class TodosController : BaseApiController
    {
        private readonly ITodoService _todoService;
        private readonly ITodoModelDtoConverter _todoConverter;

        public TodosController(ITodoService todoService, ITodoModelDtoConverter todoConverter)
        {
            _todoService = todoService;
            _todoConverter = todoConverter;
        }

        // GET: api/Todos
        /// <summary>
        /// Get Todos
        /// </summary>
        /// <returns>Todos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoResponseModel>>> Get()
        {
            IEnumerable<TodoDto> todoDtos = await _todoService.GetTodos();
            IEnumerable<TodoResponseModel> todoModels = _todoConverter.Convert(todoDtos);

            return Ok(todoModels);
        }

        // GET api/Todos/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoResponseModel>> Get([FromRoute] Guid id)
        {
            TodoDto todoDto = await _todoService.GetTodo(id);
            if (todoDto == null) return NotFound(id);
            TodoResponseModel todoModel = _todoConverter.Convert(todoDto);

            return Ok(todoModel);
        }

        // POST api/Todos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoResponseModel>> Post([FromBody] TodoRequestModel model)
        {
            TodoDto newTodoDto = _todoConverter.Convert(model);
            TodoDto createdTodoDto = await _todoService.CreateTodo(newTodoDto);
            TodoResponseModel createdTodoModel = _todoConverter.Convert(createdTodoDto);

            return Ok(createdTodoModel);
        }

        // PUT api/Todos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoResponseModel>> Put([FromRoute] Guid id, [FromBody] TodoRequestModel model)
        {
            TodoDto updatableTodoDto = _todoConverter.Convert(model);
            TodoDto updatedTodoDto = await _todoService.UpdateTodo(id, updatableTodoDto);
            if (updatedTodoDto == null) return NotFound(id);
            TodoResponseModel updatedTodoModel = _todoConverter.Convert(updatedTodoDto);

            return Ok(updatedTodoModel);
        }

        // DELETE api/Todos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            bool? result = await _todoService.DeleteTodo(id);
            if (result == null) return NotFound(id);

            return NoContent();
        }
    }
}
