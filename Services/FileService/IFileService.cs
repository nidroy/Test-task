namespace Test_task.Services.FileService
{
    public interface IFileService
    {
        List<User> ImportUsersJson(string path);

        byte[] ExportUsersJson(string path);

        List<User> ImportUsersExcel(string path);

        byte[] ExportUsersExcel(string path);
    }
}
