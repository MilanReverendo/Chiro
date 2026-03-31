using Chiro.Application.Dtos.Group;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IGroupMapper
    {
        public GroupDto MapToGroupDto(Group group);
        public Group MapFromGroupDto(GroupDto groupDto);
    }
}
