using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace TechEval.DataContext
{
    public class ImportLog
    {
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string RecordSource { get; set; }
        public string Reason { get; set; }
    }

    public class ImportLogConfiguration : IEntityTypeConfiguration<ImportLog>
    {
        public void Configure(EntityTypeBuilder<ImportLog> builder)
        {
            builder.Property(c => c.ImportDate).HasDefaultValueSql("getutcdate()");
        }
    }
}
    