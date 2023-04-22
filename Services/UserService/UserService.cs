namespace Test_task.Services.UserService
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers()
        {
            return UserData.users;
        }

        public User? GetOneUser(int id)
        {
            User user = UserData.users.Find(x => x.Id == id);

            if (user is null)
                return null;
            return user;
        }

        public User? AddUser(User user)
        {
            if (UserData.users.Exists(x => x.Id == user.Id))
                return null;

            UserData.users.Add(user);

            return user;
        }

        public List<User>? UpdateUser(int id, User data)
        {
            User user = UserData.users.Find(x => x.Id == id);

            if (user is null)
                return null;

            user.LastName = UpdateParameter(user.LastName, data.LastName);
            user.Email = UpdateParameter(user.Email, data.Email);
            user.PhoneNumber = UpdateParameter(user.PhoneNumber, data.PhoneNumber);
            user.Address = UpdateParameter(user.Address, data.Address);

            return UserData.users;
        }

        public List<User>? DeleteUser(int id)
        {
            User user = UserData.users.Find(x => x.Id == id);

            if (user is null)
                return null;

            UserData.users.Remove(user);

            return UserData.users;
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
