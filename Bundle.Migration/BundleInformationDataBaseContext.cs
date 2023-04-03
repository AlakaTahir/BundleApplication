using Bundle.Project.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bundle.Migration
{
    public class BundleInformationDataBaseContext :DbContext
    {
        public BundleInformationDataBaseContext(DbContextOptions<BundleInformationDataBaseContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> BundleInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("BundleInfos");
        }
    }
}
