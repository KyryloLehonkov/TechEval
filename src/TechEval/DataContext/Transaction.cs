using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechEval.DataContext
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransationId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; } 
        public string Status { get; set; }
    }

    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.Property(e => e.TransationId).HasMaxLength(50).IsRequired(); //probably can be unique
            builder.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.CurrencyCode).HasMaxLength(3).IsRequired();
            builder.Property(e => e.TransactionDate).IsRequired();
            builder.Property(e => e.Status).HasMaxLength(1).IsRequired();

            builder.HasKey(e => e.Id);
        }
    }

}
