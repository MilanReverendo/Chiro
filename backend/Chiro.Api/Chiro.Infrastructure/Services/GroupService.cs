using Chiro.Application.Interfaces;
using Chiro.Application.Dtos.Group;
using Chiro.Domain.Entities;
using Chiro.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupMapper _mapper;
        private readonly ChiroDbContext _context;

        public GroupService(IGroupMapper mapper, ChiroDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupDto> UpdateGroupAsync(Guid id, GroupDto groupDto)
        {
            var existingGroup = await _context.Groups
        .Include(g => g.Leaders) // Load existing leaders
        .FirstOrDefaultAsync(g => g.Id == id);

            if (existingGroup == null) throw new Exception("Group not found");

            existingGroup.Name = groupDto.Name;
            existingGroup.Description = groupDto.Description;

            // Logic to sync leaders:
            // 1. Get the IDs from the DTO
            var leaderIds = groupDto.Leaders?.Select(l => l.Id).ToList() ?? new List<Guid>();

            // 2. Fetch the actual User entities from the DB
            var selectedLeaders = await _context.Users
                .Where(u => leaderIds.Contains(u.Id))
                .ToListAsync();

            // 3. Update the navigation property
            existingGroup.Leaders = selectedLeaders;

            await _context.SaveChangesAsync();
            return _mapper.MapToGroupDto(existingGroup);
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync()
        {
            var groupEntities = await _context.Groups.Include(g => g.Leaders).ToListAsync();
            var groupDtos = groupEntities.Select(g => _mapper.MapToGroupDto(g));

            return groupDtos;
        }

        public async Task<GroupDto?> GetGroupByIdAsync(Guid id)
        {
            GroupDto group = _mapper.MapToGroupDto(await _context.Groups.FirstOrDefaultAsync(g => g.Id == id));
            return group;
        }

        public async Task<GroupDto> CreateGroupAsync(GroupDto groupDto)
        {
            Group group = _mapper.MapFromGroupDto(groupDto);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return _mapper.MapToGroupDto(group);
        }

        public async Task<GroupDto> DeleteGroupAsync(Guid id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
            {
                throw new Exception("Group not found");
            } else
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return _mapper.MapToGroupDto(group);
            }
        }
    }
}
