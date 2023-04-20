namespace Test_task.Models
{
    public class User
    {
        public int Id { get; set; } // идентификатор
        public string FirstName { get; set; } = string.Empty; // имя
        public string LastName { get; set; } = string.Empty; // фамилия
        public string DateOfBirth { get; set; } = string.Empty; // дата рождения
        public string Email { get; set; } = string.Empty; // электронная почта
        public string PhoneNumber { get; set; } = string.Empty; // номер телефона
        public string Address { get; set; } = string.Empty; // адрес
    }
}
