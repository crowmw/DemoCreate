using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repository.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class DCContext : IdentityDbContext
    {
        public DCContext()
            : base("DefaultConnection")
        {
        }

        public static DCContext Create()
        {
            return new DCContext();
        }

        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Vote> Vote { get; set; }
        public DbSet<Vote_User> Vote_User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}