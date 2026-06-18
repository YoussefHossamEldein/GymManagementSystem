using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.MemberViewModels;
using GymManagement.BLL.Services.Classes;
namespace GymManagement.PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberservice)
        {
            _memberService = memberservice;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await _memberService.GetAllMembersAsync(ct: ct);
            return View(members);
        }
        #region CreateMember
        [HttpGet]
        public IActionResult Create()
        {

        return View();
        }
        
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model,CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create) ,model);
            var result = await  _memberService.CreateMemberAsync(model, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Succesfully";
            }else
            {
                TempData["ErrorMessage"] = "Failed to Create Member";
            }
            return RedirectToAction(nameof(Index));

        }   
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);
            if(member == null)
            {

                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(member);
        }
        public async Task<IActionResult> HealthRecordDetails(int id,CancellationToken ct)
        {
            var result = await _memberService.GetHealthRecordDetailsAsync(id, ct);
            if(result == null)
            {
                TempData["ErrorMessage"] = "Healthrecord Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(result);

        }
        [HttpGet]
       public async Task<IActionResult> EditMember(int id,CancellationToken ct)
        {
            var memberToUpdate = await _memberService.GetMemberToUpdateAsync(id, ct);
            if(memberToUpdate == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> EditMember([FromRoute]int id,MemberToUpdateViewModel model,CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(nameof(EditMember), model);
            var result = await _memberService.UpdateMemberDetailsAsync(id, model, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Updated Succesfully";
           
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update member";
            }
                return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);
            if(member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id,CancellationToken ct)
        {
            var result = await _memberService.RemoveMemberAsync(id, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Member deleted successfully";
            }else
            {
                TempData["ErrorMessage"] = "Member failed to be deleted";

            }
            return   RedirectToAction(nameof(Index));
        }
            
        #endregion
    }
}
