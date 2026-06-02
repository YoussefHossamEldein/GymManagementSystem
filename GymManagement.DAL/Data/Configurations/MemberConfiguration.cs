using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Configurations
{
    internal class MemberConfiguration : GymUserConfiguration<Member> , IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.CreatedAt).HasColumnName("JoinDate").HasDefaultValueSql("GETDATE()");
            builder.HasOne(x => x.HealthRecord).WithOne(h => h.Member).HasForeignKey<HealthRecord>(h => h.MemberId);
        
            builder.HasMany(x => x.Bookings).WithOne(ms => ms.Member).HasForeignKey(ms => ms.MemberId);
            base.Configure(builder);
        }
    }
}
