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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("SessionCapacityCheck", "Capacity BETWEEN 1 AND 25");
                tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });
            builder.HasMany(x => x.Bookings).WithOne(ms => ms.Session).HasForeignKey(ms => ms.SessionId);
            builder.HasOne(x => x.Trainer).WithMany(t => t.Sessions).HasForeignKey(x => x.TrainerId);
            builder.HasOne(x => x.Category).WithMany(c => c.Sessions).HasForeignKey(x => x.CateogryId);
        }
    }
}
