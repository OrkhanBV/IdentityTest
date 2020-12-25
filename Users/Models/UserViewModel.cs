using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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

    
    /*
     * Класс RoleEditModel представляет роль и детали о пользователях в системе,
     * категоризированные по членству в роли. Класс RoleModificationModel представляет
     * набор изменений роли.
     */
    
    
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        /*public AppUser Members { get; set; }
        public AppUser NonMembers { get; set; }*/
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}

/*
* Свойства также декорированы атрибутом UIHint, который гарантирует,
* что элементы input, визуализируемые вспомогательной функцией дескриптора в
* представлении, будут иметь соответствующим образом
* установленные атрибуты type.
*/

