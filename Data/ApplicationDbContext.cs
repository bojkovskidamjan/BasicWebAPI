using BasicWebAPI.Model;
using Microsoft.EntityFrameworkCore;


namespace BasicWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>()
                .HasMany(c => c.Contacts)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Contact>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Country>()
                .HasMany(c => c.Contacts)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Contact>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Company>().HasData(
              new Company { CompanyId = 1, CompanyName = "Aspekt" },
              new Company { CompanyId = 2, CompanyName = "Endava" }
           );

            builder.Entity<Contact>().HasData(
                new Contact { ContactId = 1, ContactName = "Ivan", CompanyId = 1, CountryId = 1 },
                new Contact { ContactId = 2, ContactName = "Martin", CompanyId = 2, CountryId = 2 }
            );

            builder.Entity<Country>().HasData(
                new Country { CountryId = 1, CountryName = "Macedonia" },
                new Country { CountryId = 2, CountryName = "Italy" }
                );


                
        }
    }
}