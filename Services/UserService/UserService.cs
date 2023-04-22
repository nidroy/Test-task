using Newtonsoft.Json;

namespace Test_task.Services.UserService
{
    public class UserService : IUserService
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
        public List<User> GetAllUsers()
        {
            return users;
        }

        /// <summary>
        /// получить одного пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <returns>пользователь</returns>
        public User? GetOneUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;
            return user;
        }

        /// <summary>
        /// добавить нового пользователя
        /// </summary>
        /// <param name="user">новый пользователь</param>
        /// <returns>новый пользователь</returns>
        public User? AddUser(User user)
        {
            if (users.Exists(x => x.Id == user.Id))
                return null;

            users.Add(user);

            return user;
        }

        /// <summary>
        /// обновить пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <param name="data">новые данные пользователя</param>
        /// <returns>список пользователей</returns>
        public List<User>? UpdateUser(int id, User data)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;

            user.FirstName = UpdateParameter(user.FirstName, data.FirstName);
            user.LastName = UpdateParameter(user.LastName, data.LastName);
            user.DateOfBirth = UpdateParameter(user.DateOfBirth, data.DateOfBirth);
            user.Email = UpdateParameter(user.Email, data.Email);
            user.PhoneNumber = UpdateParameter(user.PhoneNumber, data.PhoneNumber);
            user.Address = UpdateParameter(user.Address, data.Address);

            return users;
        }

        /// <summary>
        /// удалить пользователя
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <returns>список пользователей</returns>
        public List<User>? DeleteUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;

            users.Remove(user);

            return users;
        }

        /// <summary>
        /// импортировать пользователей
        /// </summary>
        /// <param name="path">путь к файлу .json</param>
        /// <returns>список пользователей</returns>
        public List<User> ImportUsers(string path)
        {
            if (File.Exists(path))
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(path));
            else
                return null;

            return users;
        }

        /// <summary>
        /// экспортировать пользователей
        /// </summary>
        /// <param name="path">путь к файлу .json</param>
        /// <returns>массив байтов</returns>
        public byte[] ExportUsers(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(users));
            byte[] bytes = File.ReadAllBytes(path);

            return bytes;
        }


        /// <summary>
        /// обновить параметр пользователя
        /// </summary>
        /// <param name="parameter">текущий параметр пользователя</param>
        /// <param name="data">новые данные для параметра</param>
        /// <returns>обновленный параметр пользователя</returns>
        private string UpdateParameter(string parameter, string data)
        {
            if (data == "string" || data == string.Empty)
                return parameter;
            else
                return data;
        }
    }
}
