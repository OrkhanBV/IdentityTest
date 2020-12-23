using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            return View(details);
        }
    }
}

/*

Атрибуты, которые были применены в контроллере
Account. Контроллеры, управляющие пользовательскими учетными записями, 
содержат функциональность, которая должна быть доступна только аутентифицированным 
пользователям, подобную сбросу пароля. С этой целью к классу контроллера
был применен атрибут Authorize, а к индивидуальным методам действий — атрибут
AllowAnonymous. В итоге доступ к методам действий по умолчанию ограничивается
аутентифицированными пользователями, но пользователям, не прошедшим аутентификацию, 
разрешено входить в приложение. Кроме того, применяется описанный в
главе 24 атрибут ValidateAntiForgeryToken, который работает в сочетании со 
вспомогательной функцией дескриптора для элемента form в целях противодействия 
подделке межсайтовых запросов.

*/