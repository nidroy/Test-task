using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserController(ILogger<UserController> logger, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            // список пользователей
            var result = _userService.GetAllUsers();
            _logger.LogInformation("Successfully received a list of users.");

            // краткая информация о пользователях
            dynamic users = new JArray();

            foreach (var user in result)
            {
                users.Add(new JObject(
                new JProperty("Id", user.Id),
                new JProperty("FirstName", user.FirstName),
                new JProperty("LastName", user.LastName)
                ));
            }

            _logger.LogInformation("Brief information has been successfully extracted from the list of users.");
            return Content(users.ToString());
        }

        [HttpGet("GetOneUser/{id}")]
        public async Task<ActionResult<User>> GetOneUser(int id)
        {
            var result = _userService.GetOneUser(id);

            if (result is null)
            {
                _logger.LogError("Error 404. The user with the selected ID was not found.");
                return NotFound("User not found!");
            }

            _logger.LogInformation("The user with the selected ID has been successfully received.");
            return Ok(result);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<int>> AddUser(User user)
        {
            var result = _userService.AddUser(user);

            if (result is null)
            {
                _logger.LogError("Error 400. Request error. The user with the selected ID exists.");
                return BadRequest("User with this id exists!");
            }

            _logger.LogInformation("The user was successfully added to the list.");
            return Ok(result.Id);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User data)
        {
            var result = _userService.UpdateUser(id, data);

            if (result is null)
            {
                _logger.LogError("Error 404. The user with the selected ID was not found.");
                return NotFound("User not found!");
            }

            _logger.LogInformation("The user with the selected ID has been successfully updated.");
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);

            if (result is null)
            {
                _logger.LogError("Error 404. The user with the selected ID was not found.");
                return NotFound("User not found!");
            }

            _logger.LogInformation("The user with the selected ID has been successfully deleted.");
            return Ok(result);
        }

        [HttpPost("ImportUsers")]
        public async Task<ActionResult<List<User>>> ImportUsers()
        {
            string path = "users.json";
            var result = _userService.ImportUsers(path);

            if (result is null)
            {
                _logger.LogError("Error 404. The 'users.json' file was not found.");
                return NotFound("The 'users.json' file was not found!");
            }

            _logger.LogInformation("Users have been successfully imported from the 'users.json' file");
            return Ok(result);
        }

        [HttpGet("ExportUsers")]
        public async Task<IActionResult> ExportUsers()
        {
            string path = "users.json";
            var result = _userService.ExportUsers(path);

            Console.WriteLine(path);

            _logger.LogInformation("Users have been successfully exported to the 'users.json' file");
            return File(result, "users/json");
        }
    }
}
