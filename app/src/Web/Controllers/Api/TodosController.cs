using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Web.Controllers.Api;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Controllers.Api
{
    /// <summary>
    /// Endpoints for Todos
    /// </summary>
    public class TodosController : BaseApiController
    {
        private readonly ITodoService _todoService;
        private readonly ITodoModelDtoMapper _todoMapper;

        public TodosController(ITodoService todoService, ITodoModelDtoMapper todoMapper)
        {
            _todoService = todoService;
            _todoMapper = todoMapper;
        }

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET api/Todos
        /// </remarks>
        /// <returns>All todos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoResponseModel>>> Get()
        {
            IEnumerable<TodoDto> todoDtos = await _todoService.GetTodos();
            IEnumerable<TodoResponseModel> todoModels = _todoMapper.Map(todoDtos);

            return Ok(todoModels);
        }

        /// <summary>
        /// Get todo with given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET api/Todos/11111111-1111-1111-1111-111111111111
        /// </remarks>
        /// <param name="id">Todo id</param>
        /// <returns>Found todo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoResponseModel>> Get([FromRoute] Guid id)
        {
            TodoDto todoDto = await _todoService.GetTodo(id);
            if (todoDto == null) return NotFound(id);
            TodoResponseModel todoModel = _todoMapper.Map(todoDto);

            return Ok(todoModel);
        }

        // 
        /// <summary>
        /// Create new todo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST api/Todos
        ///     {
        ///         "id": "11111111-1111-1111-1111-111111111111"
        ///         "isDone": true"
        ///         ...
        ///     }
        /// </remarks>
        /// <param name="model">Creatable todo</param>
        /// <returns>Created todo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoResponseModel>> Post([FromBody] TodoRequestModel model)
        {
            TodoDto newTodoDto = _todoMapper.Map(model);
            TodoDto createdTodoDto = await _todoService.CreateTodo(newTodoDto);
            TodoResponseModel createdTodoModel = _todoMapper.Map(createdTodoDto);

            return Ok(createdTodoModel);
        }

        /// <summary>
        /// Update existing todo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     PUT api/Todos/5
        ///     {
        ///         "id": "11111111-1111-1111-1111-111111111111"
        ///         "isDone": true"
        ///         ...
        ///     }
        /// </remarks>
        /// <param name="id">Existing todo Id</param>
        /// <param name="model">Existing todo with updated values</param>
        /// <returns>Updated todo</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoResponseModel>> Put([FromRoute] Guid id, [FromBody] TodoRequestModel model)
        {
            TodoDto updatableTodoDto = _todoMapper.Map(model);
            TodoDto updatedTodoDto = await _todoService.UpdateTodo(id, updatableTodoDto);
            if (updatedTodoDto == null) return NotFound(id);
            TodoResponseModel updatedTodoModel = _todoMapper.Map(updatedTodoDto);

            return Ok(updatedTodoModel);
        }

        /// <summary>
        /// Delete existing todo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE api/Todos/11111111-1111-1111-1111-111111111111
        /// </remarks>
        /// <param name="id">Existing todo ids</param>
        /// <returns>No content</returns>
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
