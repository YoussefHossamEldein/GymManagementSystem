using Microsoft.AspNetCore.Mvc;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagement.DAL.Repositories.Classes;
namespace GymManagementSystem.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanRepository planRepository;
        public PlansController(IPlanRepository repository)
        {
            planRepository = repository;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planRepository.GetAllAsync(ct : ct);
            return View(plans);
        }
        public async Task<IActionResult> Details(int id,CancellationToken ct)
        {
            var plan = await planRepository.GetByIdAsync(id, ct);
            if (plan == null)
                return RedirectToAction("Plan Not Found");
            return View(plan);
        }
    }
}
