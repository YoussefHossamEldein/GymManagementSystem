using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Models
{
    public class Member : GymUser
    {
        public string Photo { get; set; }
        //JoinDate will be CreatedAt of BaseEntity.
        public HealthRecord HealthRecord { get; set; } = default!;//navigation property.

        public ICollection<Bookings> Bookings { get; set; }

        public ICollection<Membership> Membership { get; set; }
    }
}
