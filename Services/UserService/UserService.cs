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


        public List<User> GetAllUsers()
        {
            return users;
        }

        public User? GetOneUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;
            return user;
        }

        public List<User>? AddUser(User user)
        {
            if (users.Exists(x => x.Id == user.Id))
                return null;

            users.Add(user);

            return users;
        }

        public List<User>? UpdateUser(int id, User data)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;

            user.FirstName = UpdateField(user.FirstName, data.FirstName);
            user.LastName = UpdateField(user.LastName, data.LastName);
            user.DateOfBirth = UpdateField(user.DateOfBirth, data.DateOfBirth);
            user.Email = UpdateField(user.Email, data.Email);
            user.PhoneNumber = UpdateField(user.PhoneNumber, data.PhoneNumber);
            user.Address = UpdateField(user.Address, data.Address);

            return users;
        }

        public List<User>? DeleteUser(int id)
        {
            User user = users.Find(x => x.Id == id);

            if (user is null)
                return null;

            users.Remove(user);

            return users;
        }


        private string UpdateField(string userField, string data)
        {
            if (data == "string" || data == string.Empty)
                return userField;
            else
                return data;
        }
    }
}
