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

            //context.Provinces.AddOrUpdate(
            //    p => p.ProvinceName,
            //    new Reository.Models.Provinces { ProvinceName = "dolnoœl¹skie" },
            //    new Reository.Models.Provinces { ProvinceName = "kujawsko-pomorskie" },
            //    new Reository.Models.Provinces { ProvinceName = "lubelskie" },
            //    new Reository.Models.Provinces { ProvinceName = "lubuskie" },
            //    new Reository.Models.Provinces { ProvinceName = "³ódzkie" },
            //    new Reository.Models.Provinces { ProvinceName = "ma³opolskie" },
            //    new Reository.Models.Provinces { ProvinceName = "mazowieckie" },
            //    new Reository.Models.Provinces { ProvinceName = "opolskie" },
            //    new Reository.Models.Provinces { ProvinceName = "podkarpackie" },
            //    new Reository.Models.Provinces { ProvinceName = "podlaskie" },
            //    new Reository.Models.Provinces { ProvinceName = "pomorskie" },
            //    new Reository.Models.Provinces { ProvinceName = "œl¹skie" },
            //    new Reository.Models.Provinces { ProvinceName = "œwiêtokrzyskie" },
            //    new Reository.Models.Provinces { ProvinceName = "warmiñsko-mazurskie" },
            //    new Reository.Models.Provinces { ProvinceName = "wielkopolskie" },
            //    new Reository.Models.Provinces { ProvinceName = "zachodniopomorskie" });

            //context.AgeRange.AddOrUpdate(
            //    p => p.AgeRangeName,
            //    new Models.AgeRange { AgeRangeName = "0-9" },
            //    new Models.AgeRange { AgeRangeName = "10-17" },
            //    new Models.AgeRange { AgeRangeName = "18-29" },
            //    new Models.AgeRange { AgeRangeName = "30-39" },
            //    new Models.AgeRange { AgeRangeName = "40-49" },
            //    new Models.AgeRange { AgeRangeName = "50-59" },
            //    new Models.AgeRange { AgeRangeName = "60->" });

            //context.Education.AddOrUpdate(
            //    p => p.EducationName,
            //    new Models.Education { EducationName = "podstawowe" },
            //    new Models.Education { EducationName = "zawodowe" },
            //    new Models.Education { EducationName = "œrednie" },
            //    new Models.Education { EducationName = "wy¿sze" },
            //    new Models.Education { EducationName = "w trakcie studiów" });
        }

    }
}
