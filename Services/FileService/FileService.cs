using Newtonsoft.Json;
using Aspose.Cells;
using ClosedXML.Excel;

namespace Test_task.Services.FileService
{
    public class FileService : IFileService
    {
        public List<User> ImportUsersJson(string path)
        {
            if (File.Exists(path))
                UserData.users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(path));
            else
                return null;

            return UserData.users;
        }

        public byte[] ExportUsersJson(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(UserData.users));
            byte[] bytes = File.ReadAllBytes(path);

            return bytes;
        }

        public List<User> ImportUsersExcel(string path)
        {
            if (File.Exists(path))
            {
                UserData.users.Clear();

                var workbook = new Workbook(path);
                var worksheet = workbook.Worksheets[0];

                for (int i = 0; i < worksheet.Cells.Rows.Count; i++)
                {
                    User user = new User();

                    user.Id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                    user.FirstName = worksheet.Cells[i, 1].Value.ToString();
                    user.LastName = worksheet.Cells[i, 2].Value.ToString();
                    user.DateOfBirth = worksheet.Cells[i, 3].Value.ToString();
                    user.Email = worksheet.Cells[i, 4].Value.ToString();
                    user.PhoneNumber = worksheet.Cells[i, 5].Value.ToString();
                    user.Address = worksheet.Cells[i, 6].Value.ToString();

                    UserData.users.Add(user);
                }
            }
            else
                return null;

            return UserData.users;
        }

        public byte[] ExportUsersExcel(string path)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Sheet1");

            for (int i = 0; i < UserData.users.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = UserData.users[i].Id;
                worksheet.Cell(i + 1, 2).Value = UserData.users[i].FirstName;
                worksheet.Cell(i + 1, 3).Value = UserData.users[i].LastName;
                worksheet.Cell(i + 1, 4).Value = UserData.users[i].DateOfBirth;
                worksheet.Cell(i + 1, 5).Value = UserData.users[i].Email;
                worksheet.Cell(i + 1, 6).Value = UserData.users[i].PhoneNumber;
                worksheet.Cell(i + 1, 7).Value = UserData.users[i].Address;
            }

            workbook.SaveAs(path);

            byte[] bytes = File.ReadAllBytes(path);

            return bytes;
        }
    }
}
