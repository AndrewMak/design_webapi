using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ToDo.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly Models.Entities.User _user = new Models.Entities.User();

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var users = await _user.Get();
                return Ok(users);
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
                var user = await _user.Get(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Entities.User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user = await _user.Post(user);
                    return Ok(user);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<IHttpActionResult> Put(Guid userId, [FromBody]Models.Entities.User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user = await _user.Put(userId, user);
                    return Ok(user);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        public async Task<IHttpActionResult> Delete(Guid userId)
        {
            try
            {
                await _user.Delete(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
