using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

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
            if (ModelState.IsValid) {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null) {
                    await signInManager. SignOutAsync () ;
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(
                            user, details.Password, false, false);
                    if (result.Succeeded) {
                        return Redirect(returnUrl ?? "/") ;
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email),
                    "Invalid user or password") ;
            }
            return View(details);
        }
    }
}

/*
* Атрибуты, которые были применены в контроллере
* Account. Контроллеры, управляющие пользовательскими учетными записями, 
* содержат функциональность, которая должна быть доступна только аутентифицированным 
* пользователям, подобную сбросу пароля. С этой целью к классу контроллера
* был применен атрибут Authorize, а к индивидуальным методам действий — атрибут
* AllowAnonymous. В итоге доступ к методам действий по умолчанию ограничивается
* аутентифицированными пользователями, но пользователям, не прошедшим аутентификацию, 
* разрешено входить в приложение. Кроме того, применяется описанный в
* главе 24 атрибут ValidateAntiForgeryToken, который работает в сочетании со 
* вспомогательной функцией дескриптора для элемента form в целях противодействия 
* подделке межсайтовых запросов.
*/

/*
* Метод FindByEmailAsync ()
* находит пользовательскую учетную запись, используя
* адрес электронной почты, который применялся при ее создании. Есть также 
* альтернативные методы поиска по идентификатору, по имени и по входу. 
* Адрес электронной почты используется для входа из-за того, что такой 
* подход принят в большинстве веб-приложений, доступных через Интернет, 
* и он набирает популярность также вкорпоративных приложениях.
*/

/*
 * Если учетная запись с указанным пользователем адресом электронной почты с
 * уществует, тогда производится аутентификация с применением класса
 * SignInManager<AppUser>, для которого добавляется аргумент конструктора,
 * распознаваемый с помощью внедрения зависимостей. Класс SignlnManager используется
 * для выполнения двух шагов аутентификации:
 */