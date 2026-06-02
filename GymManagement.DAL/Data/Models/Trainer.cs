using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Models
{
    public class Trainer : GymUser
    {
        public Speciality Speciality { get; set; }
        //HireDate will be CreatedAt of BaseEntity.
        public ICollection<Session> Sessions { get; set; }
    }
}
