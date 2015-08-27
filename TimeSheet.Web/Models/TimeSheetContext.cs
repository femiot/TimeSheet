using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TimeSheet.Web.Models.Mapping;

namespace TimeSheet.Web.Models
{
    public partial class TimeSheetContext : DbContext
    {
        static TimeSheetContext()
        {
            Database.SetInitializer<TimeSheetContext>(null);
        }

        public TimeSheetContext()
            : base("Name=TimeSheetContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<tb_Task> tb_Task { get; set; }
        public DbSet<tb_User_Task> tb_User_Task { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new tb_TaskMap());
            modelBuilder.Configurations.Add(new tb_User_TaskMap());
        }
    }
}
