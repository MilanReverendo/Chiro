using Chiro.Application.Dtos;
using Chiro.Application.Interfaces;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserShortDto MapToUserShortDto(User user)
        {
            if (user == null) return null!;

            return new UserShortDto             {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsGroupLeader = user.IsGroupLeader,
                GroupId = user.GroupId
            };
        }

        public IEnumerable<UserShortDto> MapToUserShortDtoList(IEnumerable<User> users)
        {
            if (users == null) return new List<UserShortDto>();

            return users.Select(user => MapToUserShortDto(user)).ToList();
        }
    }
}
