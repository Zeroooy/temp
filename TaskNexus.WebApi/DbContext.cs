using Microsoft.EntityFrameworkCore;

using models;
namespace NetworkOS
{
    public class ApplicationContext : DbContext
    {
        private const string DataBasePath = "Data Source=Network OS.db";

        public DbSet<LibraryEntity> Libraries { get; set; } = null!;

        public DbSet<BookEntity> Books { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DataBasePath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryEntity>()
                .HasMany(e => e.Books)
                .WithOne(e => e.Library)
                .HasForeignKey(e => e.LibraryEntityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}