using Microsoft.AspNetCore.Mvc;
using Test_task.Models;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// список пользователей
        /// </summary>
        private static List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                DateOfBirth = "01.01.2021",
                Email = "ivan@mail.ru",
                PhoneNumber = "8 888 888 88 88",
                Address = "г. Волгоград, ул. Пушкина, дом 1"
            },
            new User
            {
                Id = 2,
                FirstName = "Петр",
                LastName = "Петров",
                DateOfBirth = "02.02.2022",
                Email = "petr@mail.ru",
                PhoneNumber = "8 999 999 99 99",
                Address = "г. Волгоград, ул. Ленина, дом 2"
            }
        };


        /// <summary>
        /// получить всех пользователей
        /// </summary>
        /// <returns>список пользователей</returns>
        [HttpGet("Users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(users);
        }

        /// <summary>
        /// получить одного пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <returns>пользователь</returns>
        [HttpGet("Users/{id}")]
        public async Task<ActionResult<User>> GetOneUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user == null)
                return NotFound("User not found!");
            return Ok(user);
        }

        /// <summary>
        /// добавить пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns>список пользователей</returns>
        [HttpPost("Users")]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            if (users.Exists(x => x.Id == user.Id))
                return BadRequest("User with this id exists!");

            users.Add(user);

            return Ok(users);
        }

        /// <summary>
        /// обновить пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <param name="updatedUser">обновленный пользователь</param>
        /// <returns>список пользователей</returns>
        [HttpPut("Users/{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User updatedUser)
        {
            User user = users.Find(x => x.Id == id);

            if (user == null)
                return NotFound("User not found!");

            user.FirstName = UpdateField(user.FirstName, updatedUser.FirstName);
            user.LastName = UpdateField(user.LastName, updatedUser.LastName);
            user.DateOfBirth = UpdateField(user.DateOfBirth, updatedUser.DateOfBirth);
            user.Email = UpdateField(user.Email, updatedUser.Email);
            user.PhoneNumber = UpdateField(user.PhoneNumber, updatedUser.PhoneNumber);
            user.Address = UpdateField(user.Address, updatedUser.Address);

            return Ok(users);
        }

        /// <summary>
        /// обновить поле
        /// </summary>
        /// <param name="userField">поле пользователя</param>
        /// <param name="newData">новые данные</param>
        /// <returns>данные поля</returns>
        private string UpdateField(string userField, string newData)
        {
            if (newData == "string" || newData == string.Empty)
                return userField;
            else
                return newData;
        }

        /// <summary>
        /// удалить пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <returns>список пользователей</returns>
        [HttpDelete("Users/{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user == null)
                return NotFound("User not found!");

            users.Remove(user);

            return Ok(users);
        }
    }
}
