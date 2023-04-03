using Microsoft.EntityFrameworkCore;

namespace RegisterUser.DAL.Models
{
    public partial class RegisterUserContext : DbContext
    {
        public RegisterUserContext()
        {
        }

        public RegisterUserContext(DbContextOptions<RegisterUserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
