using Microsoft.AspNetCore.Mvc;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("GetOneUser/{id}")]
        public async Task<ActionResult<User>> GetOneUser(int id)
        {
            var result = _userService.GetOneUser(id);

            if (result is null)
                return NotFound("User not found!");
            return Ok(result);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            var result = _userService.AddUser(user);

            if (result is null)
                return BadRequest("User with this id exists!");
            return Ok(result);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User data)
        {
            var result = _userService.UpdateUser(id, data);

            if (result is null)
                return NotFound("User not found!");
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);

            if (result is null)
                return NotFound("User not found!");
            return Ok(result);
        }
    }
}
