using GymManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Price).HasPrecision(10, 2);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("PlanCheckConstraint", "DurationDays BETWEEN 1 AND 365");
            });
        }
    }
}
