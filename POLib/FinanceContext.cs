using Microsoft.EntityFrameworkCore;

namespace POLib.SECScraper
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EPSDiluted>()
                .HasKey(e => new { e.Ticker, e.QuarterEnd });
            modelBuilder.Entity<DailyStockPrice>()
                .HasKey(e => new { e.Ticker, e.Date });
        }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<DailyStockPrice>? DailyStockPrice { get; set; }
        public DbSet<ConsumerPriceIndex>? ConsumerPriceIndex { get; set; }
        public DbSet<EPSDiluted>? EPSDiluted { get; set; }
    }
}
