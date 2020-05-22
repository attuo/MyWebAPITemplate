using System.Collections.Generic;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;
using AspNetCoreWebApiTemplate.Mappings.ToModel;
using AspNetCoreWebApiTemplate.Models.ResponseModels;
using AspNetCoreWebApiTemplate.Web.Controllers.Api;
using Microsoft.AspNetCore.Mvc;


namespace AspNetCoreWebApiTemplate.Controllers.Api
{
    public class TodosController : BaseApiController
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/Todos
        /// <summary>
        /// Get Todos
        /// </summary>
        /// <returns>Todos</returns>
        [HttpGet]
        public IEnumerable<TodoResponseModel> Get()
        {
            return _todoService.GetTodos().ToResponseModels();
        }

        // GET api/Todos/5
        [HttpGet("{id}")]
        public string Get(int id)
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Todos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
