using GymManagement.BLL.ViewModels.MemberViewModels;
using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services.Interfaces
{
    public interface IMemberService
    {
       Task <IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);
       Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
       Task<MemberViewModel?> GetMemberDetailsByIdAsync(int id, CancellationToken ct = default);
       Task<HealthRecordViewModel?> GetHealthRecordDetailsAsync(int id, CancellationToken ct = default);

        Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int id, CancellationToken ct);
        Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct);

        Task<bool> RemoveMemberAsync(int id, CancellationToken ct);
        
    }
}
