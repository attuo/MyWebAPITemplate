using Microsoft.AspNetCore.Mvc;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;

namespace MyWebAPITemplate.Source.Web.Controllers;

/// <summary>
/// Endpoints for Todos.
/// </summary>
public class TodosController : BaseApiController
{
    private readonly ITodoService _todoService;
    private readonly ITodoModelDtoMapper _todoMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodosController"/> class.
    /// </summary>
    /// <param name="todoService"><see cref="ITodoService"/>.</param>
    /// <param name="todoMapper"><see cref="ITodoModelDtoMapper"/>.</param>
    public TodosController(ITodoService todoService, ITodoModelDtoMapper todoMapper)
    {
        _todoService = todoService;
        _todoMapper = todoMapper;
    }

    /// <summary>
    /// Get all todos.
    /// </summary>
    /// <example>
    /// Sample request:
    ///     GET api/Todos.
    /// </example>
    /// <returns>All todos.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoResponseModel>>> Get()
    {
        IEnumerable<TodoDto> todoDtos = await _todoService.GetTodos();
        IEnumerable<TodoResponseModel> todoModels = _todoMapper.Map(todoDtos);

        return Ok(todoModels);
    }

    /// <summary>
    /// Get todo with given id.
    /// </summary>
    /// <example>
    /// Sample request:
    ///     GET api/Todos/11111111-1111-1111-1111-111111111111.
    /// </example>
    /// <param name="id">Todo id.</param>
    /// <returns>Found todo.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponseModel>> Get([FromRoute] Guid id)
    {
        TodoDto todoDto = await _todoService.GetTodo(id);
        TodoResponseModel todoModel = _todoMapper.Map(todoDto);

        return Ok(todoModel);
    }

    /// <summary>
    /// Create new todo.
    /// </summary>
    /// <example>
    /// Sample request:
    ///     POST api/Todos
    ///     {
    ///         "id": "11111111-1111-1111-1111-111111111111"
    ///         "isDone": true"
    ///         ...
    ///     }.
    /// </example>
    /// <param name="model">Creatable todo.</param>
    /// <returns>Created todo.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TodoResponseModel>> Create([FromBody] TodoRequestModel model)
    {
        TodoDto newTodoDto = _todoMapper.Map(model);
        TodoDto createdTodoDto = await _todoService.CreateTodo(newTodoDto);
        TodoResponseModel createdTodoModel = _todoMapper.Map(createdTodoDto);

        return Ok(createdTodoModel);
    }

    /// <summary>
    /// Update existing todo.
    /// </summary>
    /// <example>
    /// Sample request:
    ///     PUT api/Todos/5
    ///     {
    ///         "id": "11111111-1111-1111-1111-111111111111"
    ///         "isDone": true"
    ///         ...
    ///     }.
    /// </example>
    /// <param name="id">Existing todo Id.</param>
    /// <param name="model">Existing todo with updated values.</param>
    /// <returns>Updated todo.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponseModel>> Update([FromRoute] Guid id, [FromBody] TodoRequestModel model)
    {
        TodoDto updatableTodoDto = _todoMapper.Map(model);
        TodoDto updatedTodoDto = await _todoService.UpdateTodo(id, updatableTodoDto);
        TodoResponseModel updatedTodoModel = _todoMapper.Map(updatedTodoDto);

        return Ok(updatedTodoModel);
    }

    /// <summary>
    /// Delete existing todo.
    /// </summary>
    /// <example>
    /// Sample request:
    ///     DELETE api/Todos/11111111-1111-1111-1111-111111111111.
    /// </example>
    /// <param name="id">Existing todo ids.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _todoService.DeleteTodo(id);
        return NoContent();
    }
}