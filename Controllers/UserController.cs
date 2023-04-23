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
        private readonly IFileService _fileService;


        public UserController(ILogger<UserController> logger, IUserService userService, IFileService fileService)
        {
            _logger = logger;
            _userService = userService;
            _fileService = fileService;
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

        [HttpPost("ImportUsersJson")]
        public async Task<ActionResult<List<User>>> ImportUsersJson()
        {
            string path = "Data/Users.json";
            var result = _fileService.ImportUsersJson(path);

            if (result is null)
            {
                _logger.LogError("Error 404. The 'Users.json' file was not found.");
                return NotFound("The 'users.json' file was not found!");
            }

            _logger.LogInformation("Users have been successfully imported from the 'Users.json' file");
            return Ok(result);
        }

        [HttpGet("ExportUsersJson")]
        public async Task<IActionResult> ExportUsersJson()
        {
            string path = "Data/Users.json";

            var result = _fileService.ExportUsersJson(path);

            _logger.LogInformation("Users have been successfully exported to the 'Users.json' file");
            return File(result, "text/json", "Users.json");
        }

        [HttpPost("ImportUsersExcel")]
        public async Task<ActionResult<List<User>>> ImportUsersExcel()
        {
            string path = "Data/Users.xlsx";
            var result = _fileService.ImportUsersExcel(path);

            if (result is null)
            {
                _logger.LogError("Error 404. The 'Users.xlsx' file was not found.");
                return NotFound("The 'users.xlsx' file was not found!");
            }

            _logger.LogInformation("Users have been successfully imported from the 'Users.xlsx' file");
            return Ok(result);
        }

        [HttpGet("ExportUsersExcel")]
        public async Task<IActionResult> ExportUsersExcel()
        {
            string path = "Data/Users.xlsx";
            var result = _fileService.ExportUsersExcel(path);

            _logger.LogInformation("Users have been successfully exported to the 'Users.xlsx' file");
            return File(result, "text/xlsx", "Users.xlsx");
        }
    }
}
