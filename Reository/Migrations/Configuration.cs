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
                new Reository.Models.Provinces { ProvinceName = "dolno�l�skie" },
                new Reository.Models.Provinces { ProvinceName = "kujawsko-pomorskie" },
                new Reository.Models.Provinces { ProvinceName = "lubelskie" },
                new Reository.Models.Provinces { ProvinceName = "lubuskie" },
                new Reository.Models.Provinces { ProvinceName = "��dzkie" },
                new Reository.Models.Provinces { ProvinceName = "ma�opolskie" },
                new Reository.Models.Provinces { ProvinceName = "mazowieckie" },
                new Reository.Models.Provinces { ProvinceName = "opolskie" },
                new Reository.Models.Provinces { ProvinceName = "podkarpackie" },
                new Reository.Models.Provinces { ProvinceName = "podlaskie" },
                new Reository.Models.Provinces { ProvinceName = "pomorskie" },
                new Reository.Models.Provinces { ProvinceName = "�l�skie" },
                new Reository.Models.Provinces { ProvinceName = "�wi�tokrzyskie" },
                new Reository.Models.Provinces { ProvinceName = "warmi�sko-mazurskie" },
                new Reository.Models.Provinces { ProvinceName = "wielkopolskie" },
                new Reository.Models.Provinces { ProvinceName = "zachodniopomorskie" });
        }
    }
}
