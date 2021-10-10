using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using DormitoryAlliance.Client.Areas.Identity.Data;

namespace DormitoryAlliance.Client.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            DormitoryAllianceDbContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DormitoryAllianceDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Dormitories.Any())
            {
                context.Dormitories.AddRange(
                    new Dormitory
                    {
                        Address = "Студенческий переулок 2, Тел. 704-10-55",
                        Floors = 4
                    },
                    new Dormitory
                    {
                        Address = "Студенческий переулок 4, Тел. 704-10-52",
                        Floors = 4
                    },
                    new Dormitory
                    {
                        Address = "Студенческий переулок 6 (иностранные студенты), Тел. 704-10-56",
                        Floors = 5
                    },
                    new Dormitory
                    {
                        Address = "Студенческий переулок 8, Тел. 704-10-53",
                        Floors = 5
                    },
                    new Dormitory
                    {
                        Address = "Студенческий переулок 10, Тел. 704-02-17",
                        Floors = 9
                    },
                    new Dormitory
                    {
                        Address = "ул. Тимуровцев 5а, Тел. 738-04-15",
                        Floors = 9
                    }
                );
                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Group { Name = "AAA-11-21" },
                    new Group { Name = "AAB-11-21" },
                    new Group { Name = "ABA-11-21" },
                    new Group { Name = "ABB-11-21" },
                    new Group { Name = "BAA-11-21" },
                    new Group { Name = "BAB-11-21" },
                    new Group { Name = "BBA-11-21" },
                    new Group { Name = "BBB-11-21" }
                );
                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                const int count = 30;
                var rnd = new System.Random();

                string[] names = { "Артём", "София", "Мария", "Полина", "Полина", "Иван", "Софья", "Пётр", "Мирослав", "Руслан", "Тимур", "Константин", "Матвей", "Макар", "Арсений", "Александр", "Алексей", "Злата", "Ника", "Адам", "Анна", "Эмир", "Александр", "Даниил", "Марк", "Виктория", "Алексей", "Семён", "Аврора", "Герман" };
                string[] surnames = { "Злобин", "Колесникова", "Крылова", "Михайлова", "Филиппова", "Антонов", "Васильева", "Романов", "Родин", "Баранов", "Чернов", "Степанов", "Борисов", "Евдокимов", "Мальцев", "Смирнов", "Белов", "Виноградова", "Литвинова", "Долгов", "Ефремова", "Кузнецов", "Орехов", "Бочаров", "Барсуков", "Дмитриева", "Соколов", "Васильев", "Шевелева", "Кириллов" };
                string[] patronymics = { "Максимович", "Георгиевна", "Ярославовна", "Фёдоровна", "Матвеевна", "Максимович", "Алексеевна", "Дмитриевич", "Иванович", "Михайлович", "Евгеньевич", "Арсентьевич", "Николаевич", "Матвеевич", "Григорьевич", "Михайлович", "Львович", "Николаевна", "Марковна", "Маркович", "Денисовна", "Тимурович", "Матвеевич", "Михайлович", "Иванович", "Денисовна", "Александрович", "Андреевич", "Робертовна", "Даниилович" };

                Student[] students = Enumerable.Range(1, count).Select(_ => new Student
                {
                    Name = names[rnd.Next(names.Length)],
                    Surname = surnames[rnd.Next(surnames.Length)],
                    Patronymic = patronymics[rnd.Next(patronymics.Length)],
                    GroupId = rnd.Next(1, context.Groups.Count() + 1),
                    DormitoryId = rnd.Next(1, context.Dormitories.Count() + 1),
                    Room = 100 + rnd.Next(1, 16),
                    Course = rnd.Next(1, 7)
                }).ToArray();

                context.Students.AddRange(students);

                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "Default" }
                );
                
                context.SaveChanges();
            }
        }
    }
}