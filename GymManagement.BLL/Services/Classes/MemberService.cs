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
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IGenericRepository<Plan> _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
        private readonly IGenericRepository<Bookings> _bookingRepository;
        public MemberService(IGenericRepository<Member> memberrepository, 
            IGenericRepository<Membership> membershipRepository,
            IGenericRepository<Plan> planRepository,
            IGenericRepository<HealthRecord> healthrecordRepository,
            IGenericRepository<Bookings> bookingRepository)
        {
            MemberRepository = memberrepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
            _healthRecordRepository = healthrecordRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct)
        {
            var emailExist =await MemberRepository.AnyAsync(x => x.Email == model.Email);
            var phoneExist = await MemberRepository.AnyAsync(x => x.Phone == model.Phone);
            if (emailExist || phoneExist)
                return false;
            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = new Address
                {
                    BuildingNo = model.BuildingNumber,
                    City = model.City,
                    Street = model.City
                },
                HealthRecord = new HealthRecord
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    Note = model.HealthRecordViewModel.Note

                }

            };
            var result = await MemberRepository.AddAsync(member);
            return result > 0;
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

        public async Task<HealthRecordViewModel?> GetHealthRecordDetailsAsync(int id, CancellationToken ct = default)
        {
     
            var healthRecord = await _healthRecordRepository.FirstOrDefaultAsync(x => x.MemberId == id);
            if (healthRecord == null)
                return null;
            var model = new HealthRecordViewModel
            {
                Height = healthRecord.Height,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Note,
                Weight = healthRecord.Weight
            };
            return model;
        }

        public async Task<MemberViewModel?> GetMemberDetailsByIdAsync(int id, CancellationToken ct)
        {
            var member = await MemberRepository.GetByIdAsync(id,ct);
            if (member == null)
                return null;
            var model = new MemberViewModel
            {
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString(),
                Address = $"{member.Address.BuildingNo} - {member.Address.Street} = {member.Address.City}"


            };
            var activeMembership = await _membershipRepository.FirstOrDefaultAsync(x => x.MemberId == id && x.EndDate > DateTime.Now);
            if(activeMembership is not null)
            {
                var activePlan = await _planRepository.GetByIdAsync(activeMembership.PlanId, ct);
                model.PlanName = activePlan.Name;
                model.MembershipStartDate = activeMembership.CreatedAt.ToString();
                model.MembershipEndDate = activeMembership.EndDate.ToString();
            }
            return model;
            
        }

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int id, CancellationToken ct)
        {
            var member = await MemberRepository.GetByIdAsync(id, ct);
            if (member == null)
                return null;
            return new MemberToUpdateViewModel
            {
                Phone = member.Phone,
                Street = member.Address.Street,
                City = member.Address.City,
                BuildingNumber = member.Address.BuildingNo,
                Email = member.Email,
                Name = member.Name,
                Photo = member.Photo
            };
        }

        public async Task<bool> RemoveMemberAsync(int id, CancellationToken ct)
        {
            var member = await MemberRepository.GetByIdAsync(id, ct);
            if (member == null)
                return false;
            var hasFutureBookings = await _bookingRepository.AnyAsync(x => x.MemberId == id && x.Session.StartDate > DateTime.Now);
            if (hasFutureBookings)
                return false;
            var result = await MemberRepository.DeleteAsync(member);
            return result > 0;
        }

        public async Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct)
        {
            var member = await MemberRepository.GetByIdAsync(id);
            if (member == null)
                return false;
            var emailExist = await MemberRepository.AnyAsync(m => m.Email == model.Email && m.Id != id);
            var phoneExist = await MemberRepository.AnyAsync(m => m.Phone == model.Phone && m.Id != id);
            if (emailExist || phoneExist)
                return false;
            //member.Name = model.Name;
            member.Address.Street = model.Street;
            member.Address.City = model.City;
            member.Address.BuildingNo = model.BuildingNumber;
            member.Phone = model.Phone;
            //member.Photo = model.Photo;
            member.Email = model.Email;
            member.UpdatedAt = DateTime.Now;
           var result = await MemberRepository.UpdateAsync(member);
            return result > 0;

        }
    }
}
