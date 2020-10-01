using Microsoft.EntityFrameworkCore;

namespace TechEval.DataContext
{
    public class Db : DbContext
    {

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ImportLog> ImportLogs { get; set; }

        public Db(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new ImportLogConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
