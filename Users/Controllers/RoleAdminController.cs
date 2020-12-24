using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.Models;
using System.Collections.Generic;

namespace Users.Controllers
{
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }

        public ViewResult Index() => View(roleManager.Roles);
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return View("Index", roleManager.Roles);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

        }
    }
}

/*
 * Управление ролями производится с использованием класса RoleManager<T>, где
 * Т —тип, предназначенный для представления ролей (встроенный класс IdentityRole
 * в текущем приложении). Конструктор RoleAdminController объявляет зависимость
 * от RoleManager<IdentityRole>, распознаваемую посредством внедрения зависимостей
 * при создании объекта контроллера.
 * В классе RoleManager<T> определены методы и свойства, перечисленные ниже
 * 
 * CreateAsync(role) Создает новую роль
 * DeleteAsync(role) Удаляет указанную роль
 * FindByldAsync(id) Находит роль по ее идентификатору
 * FindByNameAsync(name) Находит роль по ее имени
 * RoleExistsAsync(name) Возвращает true, если роль с указанным именем существует
 * UpdateAsync(role) Сохраняет изменения в указанной роли
 * Roles Возвращает перечисление ролей, которые были определены
 */