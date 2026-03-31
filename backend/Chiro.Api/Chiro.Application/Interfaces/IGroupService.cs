using Chiro.Application.Dtos.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto?> GetGroupByIdAsync(Guid id);
        Task<GroupDto> UpdateGroupAsync(Guid id, GroupDto group);
        Task<GroupDto> CreateGroupAsync(GroupDto groupDto);
        Task<GroupDto> DeleteGroupAsync(Guid id);
    }
}
