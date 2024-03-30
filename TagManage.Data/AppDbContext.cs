using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagManage.Data.Entities;
using TagManage.Data.EntityMapping;

namespace TagManage.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option)
            : base(option)
        { 
        }

        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TagEntityMap());
        }

    }
}
