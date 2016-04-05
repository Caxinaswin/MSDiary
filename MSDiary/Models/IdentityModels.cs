using System.Collections;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MSDiary.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("User_Id");
        }

        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<TipoDespesa> TipoDespesas { get; set; }
        public DbSet<TipoPagamento> TipoPagamentos { get; set; }
        public DbSet<TipoRendimento> TipoRendimentos { get; set; }
        public DbSet<Rendimento> Rendimentos { get; set; }
        public DbSet<Saldo> Saldo { get; set; }
        public DbSet<SaldoUtilizador> SaldoUtilizadores { get; set; }
        public IEnumerable IdentityUsers { get; internal set; }
    }
}