using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskQ_webapi.Data;
using TaskQ_webapi.Model;

namespace TaskQ_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbcontext _context;
        public TodoController(AppDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var todo = _context.Todolist.ToList();
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Create( Todolist todolist)
        {
            //if (todolist == null)
            //{
            //    return BadRequest("Model is NULL");
            //}

            _context.Todolist.Add(todolist);
            _context.SaveChanges();
            return Ok("Saved");
        }
    }
}
