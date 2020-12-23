using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}

//Свойства также декорированы атрибутом
//UIHint, который гарантирует, что элементы input,
//визуализируемые вспомогательной функцией дескриптора в
//представлении, будут иметь соответствующим образом
//установленные атрибуты type.