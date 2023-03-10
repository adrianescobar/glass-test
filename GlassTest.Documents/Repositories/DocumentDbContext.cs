using System.Reflection;
using GlassTest.Documents.Models;
using Microsoft.EntityFrameworkCore;

namespace GlassTest.Documents.Repositories
{
    public class DocumentDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DocumentDbContext))));
        }
    }
}