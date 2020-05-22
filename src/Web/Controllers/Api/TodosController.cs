using System.Collections.Generic;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;
using AspNetCoreWebApiTemplate.Models.ResponseModels;
using AspNetCoreWebApiTemplate.Web.Controllers.Api;
using AspNetCoreWebApiTemplate.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AspNetCoreWebApiTemplate.Controllers.Api
{
    public class TodosController : BaseApiController
    {
        private readonly ITodoService _todoService;
        private readonly ITodoModelDtoConverter _converter;

        public TodosController(ITodoService todoService, ITodoModelDtoConverter converter)
        {
            _todoService = todoService;
            _converter = converter;
        }

        // GET: api/Todos
        /// <summary>
        /// Get Todos
        /// </summary>
        /// <returns>Todos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TodoResponseModel>> Get()
        {
            IEnumerable<TodoDto> todoDtos = _todoService.GetTodos();
            IEnumerable<TodoResponseModel> todoModels = _converter.Convert(todoDtos);
            return Ok(todoModels);
        }

        // GET api/Todos/5
        [HttpGet("{id}")]
        public string Get([FromRoute]int id)
        {
            return "value";
        }

        // POST api/Todos
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Todos/5
        [HttpPut("{id}")]
        public void Put([FromRoute]int id, [FromBody]string value)
        {
        }

        // DELETE api/Todos/5
        [HttpDelete("{id}")]
        public void Delete([FromRoute]int id)
        {
        }
    }
}
