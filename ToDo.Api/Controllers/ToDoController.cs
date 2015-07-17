
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ToDo.Api.Controllers
{
    [RoutePrefix("api/todos")]
    public class ToDoController : ApiController
    {
        private readonly Models.Entities.ToDo _todo = new Models.Entities.ToDo();

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var doings = await _todo.Get();
                return Ok(doings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{todoId:guid}/{userId}")]
        public async Task<IHttpActionResult> Get(Guid todoId, Guid userId)
        {
            try
            {
                var todo = await _todo.Get(todoId, userId);
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IHttpActionResult> Get(Guid userId)
        {
            try
            {
                var doings = await _todo.Get(userId);
                return Ok(doings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{userId:guid}")]
        [Filters.HttpsRequire]
        public async Task<IHttpActionResult> Post(Guid userId, [FromBody]Models.Entities.ToDo todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    todo = await _todo.Post(userId, todo);
                    return Ok(todo);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{todoId:guid}")]
        public async Task<IHttpActionResult> Put(Guid todoId, [FromBody]Models.Entities.ToDo todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    todo = await _todo.Put(todoId, todo);
                    return Ok(todo);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{todoId:guid}")]
        public async Task<IHttpActionResult> Delete(Guid todoId)
        {
            try
            {
                await _todo.Delete(todoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
