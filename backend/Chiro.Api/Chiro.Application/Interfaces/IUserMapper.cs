using Chiro.Application.Dtos;
using Chiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Application.Interfaces
{
    public interface IUserMapper
    {
        UserShortDto MapToUserShortDto(User user);
        IEnumerable<UserShortDto> MapToUserShortDtoList(IEnumerable<User> users);
    }
}
