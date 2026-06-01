using GymManagement.DAL.Configurations;
using GymManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.DbContexts
{
    public class GymDbContext : DbContext
    {

        public GymDbContext(DbContextOptions<GymDbContext> options):base(options)
        {
            
        }
        public DbSet<Plan> Plans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
        }
    }
}
