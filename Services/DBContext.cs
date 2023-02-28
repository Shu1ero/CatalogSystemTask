using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Model;

namespace Services
{
    public class DBContext: DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Folder>().HasData(
                new Folder
                {
                    Id= 1,
                    Name = "Creating Digital Images"
                },
                new Folder
                {
                    Id = 2,
                    Name = "Resources",
                    ParentFolderId = 1,
                },
                new Folder
                {
                    Id = 3,
                        Name = "Evidence",
                    ParentFolderId = 1,
                },
                new Folder
                {
                    Id = 4,
                    Name = "Graphic Products",
                    ParentFolderId = 1,
                },
                new Folder
                {
                    Id = 5,
                    Name = "Primary Sources",
                    ParentFolderId = 2,
                },
                new Folder
                {
                    Id = 6,
                    Name = "Secondary Sources",
                    ParentFolderId = 2,
                },
                new Folder
                {
                    Id = 7,
                    Name = "Process",
                    ParentFolderId = 4,
                },
                new Folder
                {
                    Id = 8,
                    Name = "Final Product",
                    ParentFolderId = 4,
                }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Data Source=localhost;Initial Catalog=CatalogSystem;Integrated Security=True;TrustServerCertificate=true");
        }
    }
}
