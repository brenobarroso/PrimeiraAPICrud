using Microsoft.AspNetCore.Mvc;
using MeuTodo.Models;
using System.Collections.Generic;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route ("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public List<Todo> Get()
        {
            return new List<Todo>();
        }
    }
}