using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Models;

namespace Users
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(option =>
                option.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(opts =>
                { 
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc( mvcOtions=>
            {
                mvcOtions.EnableEndpointRouting = false;
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}

/*
 *    Как правильно подключиться при Npgsql смотрите в статье, метод указанный в книге не подходит так как
 * Npgsql имеет свои особенности работы
 * https://medium.com/@RobertKhou/asp-net-core-mvc-identity-using-postgresql-database-bc52255f67c4
 */
 
 /*
  *\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
  * РЕАЛИЗОВАНО НА ТЕКУЩИЙ МОМЕНТ
  *\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
  * 1)Решил реализовать идентификацию и авторизацию как отдельный проект, чтобы не сломать основной проект
  * 2)Настроил Core Identity
  * 3)Создал базу данных сгенерировав необходимые таблицы с использованием Identity & EntityFramework
  * 4)Создал контроллер по созданию нового пользователя
  * 5)Создал админку через c перечислением пользователей
  * 6)Настроил конфигурирование правил проверки паролей в файле Startup
  * 7)Реализовал средства администрирования добавление/удаление/редактирование
  * 8)Аутентификация пользователей
  * 9)Автоизация пользователей с помощью ролей
  *
  * 
  *
  * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
  * МОИ ПЛАНЫ
  * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
  * объеденить проекты вместе, убедивгись что не сломаю основной проект
  * при объеденинии проектов буду использовать паттерн "Репозиторий"
  */