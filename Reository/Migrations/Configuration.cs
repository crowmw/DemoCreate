namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.DCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Repository.Models.DCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Provinces.AddOrUpdate(
                p => p.ProvinceName,
                new Models.Provinces { ProvinceName = "dolnoslaskie" },
                new Models.Provinces { ProvinceName = "kujawsko-pomorskie" },
                new Models.Provinces { ProvinceName = "lubelskie" },
                new Models.Provinces { ProvinceName = "lubuskie" },
                new Models.Provinces { ProvinceName = "lodzkie" },
                new Models.Provinces { ProvinceName = "malopolskie" },
                new Models.Provinces { ProvinceName = "mazowieckie" },
                new Models.Provinces { ProvinceName = "opolskie" },
                new Models.Provinces { ProvinceName = "podkarpackie" },
                new Models.Provinces { ProvinceName = "podlaskie" },
                new Models.Provinces { ProvinceName = "pomorskie" },
                new Models.Provinces { ProvinceName = "slaskie" },
                new Models.Provinces { ProvinceName = "swietokrzyskie" },
                new Models.Provinces { ProvinceName = "warminsko-mazurskie" },
                new Models.Provinces { ProvinceName = "wielkopolskie" },
                new Models.Provinces { ProvinceName = "zachodniopomorskie" });

            context.AgeRange.AddOrUpdate(
                p => p.AgeRangeName,
                new Models.AgeRange { AgeRangeName = "0-9" },
                new Models.AgeRange { AgeRangeName = "10-17" },
                new Models.AgeRange { AgeRangeName = "18-29" },
                new Models.AgeRange { AgeRangeName = "30-39" },
                new Models.AgeRange { AgeRangeName = "40-49" },
                new Models.AgeRange { AgeRangeName = "50-59" },
                new Models.AgeRange { AgeRangeName = "60->" });

            context.Education.AddOrUpdate(
                p => p.EducationName,
                new Models.Education { EducationName = "podstawowe" },
                new Models.Education { EducationName = "zawodowe" },
                new Models.Education { EducationName = "srednie" },
                new Models.Education { EducationName = "wyzsze" },
                new Models.Education { EducationName = "w trakcie studiow" });
        }

    }
}
