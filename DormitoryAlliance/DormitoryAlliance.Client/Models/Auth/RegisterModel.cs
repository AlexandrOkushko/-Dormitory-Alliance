using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models.Auth
{
    public class RegisterModel
    {
        [Required, StringLength(30, ErrorMessage = "Не указано имя", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Не указана фамилия", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Не указан Email", MinimumLength = 3)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
