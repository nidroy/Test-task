namespace Test_task.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetOneUser(int id);
        List<User> AddUser(User user);
        List<User> UpdateUser(int id, User data);
        List<User> DeleteUser(int id);
    }
}
