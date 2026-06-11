using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModels.MemberViewModels;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> MemberRepository;
        public MemberService(IGenericRepository<Member> memberrepository)
        {
            MemberRepository = memberrepository;
        }
        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct)
        {
            var members = await MemberRepository.GetAllAsync(ct:ct);
            if (!members.Any())
                return [];
            //List<MemberViewModel> MemberViewModels = new List<MemberViewModel>();
            //foreach(var member in members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Name = member.Name,
            //        Email = member.Email,
            //        Gender = member.Gender.ToString(),
            //        Id = member.Id,
            //        Phone = member.Phone,
            //        Photo = member.Photo
            //    };
            //    MemberViewModels.Add(memberViewModel);
            //}
            //return MemberViewModels;
            var membersViewModel = members.Select(m => new MemberViewModel()
            {
                Name = m.Name,
                Email = m.Email,
                Gender = m.Gender.ToString(),
                Id = m.Id,
                Phone = m.Phone,
                Photo = m.Photo
            });
            return membersViewModel;
        }
    }
}
