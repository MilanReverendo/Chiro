using Chiro.Application.Dtos;
using Chiro.Application.Dtos.Group;
using Chiro.Application.Interfaces;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Mappers
{
    public class GroupMapper : IGroupMapper
    {
        public GroupDto MapToGroupDto(Group group)
        {
            if (group == null) return null!;

            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Leaders = group.Leaders?.Select(u => new UserShortDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsGroupLeader = u.IsGroupLeader
                }).ToList()
            };
        }

        public Group MapFromGroupDto(GroupDto groupDto)
        {
            if (groupDto == null) return null!;

            return new Group
            {
                Id = groupDto.Id,
                Name = groupDto.Name,
                Description = groupDto.Description,
                Leaders = new List<User>()
            };
        }
    }
}
