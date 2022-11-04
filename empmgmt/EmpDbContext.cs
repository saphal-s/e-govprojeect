using Microsoft.EntityFrameworkCore;

using empmgmt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace empmgmt
{
    public class EmpDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {

        }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<PositionModel> Positions { get; set; }
        public DbSet<LeaveModel> Leaves { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectModel>().ToTable("Project");
            builder.Entity<DepartmentModel>().ToTable("Department");
            builder.Entity<PositionModel>().ToTable("Position");
            builder.Entity<LeaveModel>().ToTable("Leave");

            base.OnModelCreating(builder);
        }

       // public DbSet<empmgmt.Models.DepartmentViewModel>? DepartmentViewModel { get; set; }

      //  public DbSet<empmgmt.Models.UserModel>? UserModel { get; set; }

    }
}

