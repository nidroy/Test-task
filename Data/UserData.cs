namespace Test_task.Data
{
    public class UserData
    {
        /// <summary>
        /// список пользователей
        /// </summary>
        public static List<User> users = new List<User>
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
    }
}
