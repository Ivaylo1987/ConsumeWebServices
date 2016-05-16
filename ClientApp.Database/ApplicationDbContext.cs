namespace ClientApp.Database
{
    using ClientApp.Database.Migrations;
    using ClientApp.DataModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public IDbSet<WebToken> WebTokens { get; set; }
        public IDbSet<EmployeeArrival> EmployeeArrivals { get; set; }
    }
}
