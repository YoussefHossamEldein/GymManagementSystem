using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Models
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight  { get; set; }

        public string? Note { get; set; }

        public string BloodType { get; set; } = default!;

        public Member Member { get; set; } = default!;// navigation property 
        public int MemberId { get; set; }   //foreign Key.
    }
}
