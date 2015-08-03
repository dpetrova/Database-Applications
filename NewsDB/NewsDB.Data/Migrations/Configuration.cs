using NewsDB.Models;

namespace NewsDB.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    public sealed class Configuration : DbMigrationsConfiguration<NewsDB.Data.NewsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NewsDB.Data.NewsDBContext";
        }

        protected override void Seed(NewsDBContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
            if (context.Newses.Any())
            {
                return;
            }

            //fill few students
            News socceer = new News()
            {
                Content = @"Брьондби /Дания/ и Берое Стара Загора завършиха 0:0 в срещата-реванш от втория квалификационен кръг на турнира по футбол Лига Европа."
            };

            News voleyball = new News()
            {
                Content = "Франция спечели за първи път в историята Световната лига по волейбол."
            };

            News basketball = new News()
            {
                Content = @"Баскетболистът на Хюстън Джеймс Хардън беше избран за най-полезен играч (MVP) на миналия сезон в анкетата на асоциацията на играчите в НБА, съобщава ESPN."
            };

            News sumo = new News()
            {
                Content = "Българинът Даниел Иванов записа шестата си победа в 12-ия кръг на турнира по сумо в Нагоя."
            };

            News velo = new News()
            {
                Content = @"Французинът Ромен Барде спечели 18-тия етап от колоездачната обиколка на Франция, а британецът Крис Фрум запази жълтата фланелка на лидер в главното индивидуално класиране три дни преди финала на Тур дьо Франс в Париж."
            };

            News swimming = new News()
            {
                Content = "Столицата на Обединените арабски емирства Абу Даби ще бъде домакин на световното първенство по плуване през 2020 година."
            };

            context.Newses.AddOrUpdate(socceer, voleyball, basketball, sumo, velo, swimming);

            context.SaveChanges();
        }
    }
}
