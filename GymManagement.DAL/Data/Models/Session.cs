using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Models
{
    public class Session : BaseEntity
    {
        public string Description { get; set; }
        public int Capacity { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


        public Trainer Trainer { get; set; } = default!;
        public int TrainerId { get; set; }

        public Category Category { get; set; } = default!;
        public int CateogryId { get; set; }
        public ICollection<Bookings> Bookings { get; set; }
    }
}
