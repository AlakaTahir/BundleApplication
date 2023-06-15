using Bundle.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bundle.Migrations
{
    public class BundleInformationDataBaseContext: DbContext
    {
        public BundleInformationDataBaseContext(DbContextOptions<BundleInformationDataBaseContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> BundleInfos { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("BundleInfos");
            modelBuilder.Entity<UserBalance>().ToTable("UserBalances");
            modelBuilder.Entity<TransactionHistory>().ToTable("TransactionHistories");
        }
    }

}

