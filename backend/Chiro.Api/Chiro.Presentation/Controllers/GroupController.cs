using Microsoft.AspNetCore.Mvc;
using Chiro.Application.Interfaces;
using Chiro.Application.Dtos.Group;

namespace Chiro.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group is null)
                return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> PostGroup(GroupDto group)
        {
            var createdGroup = await _groupService.CreateGroupAsync(group);
            return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(Guid id, GroupDto group)
        {
            var updatedGroup = await _groupService.UpdateGroupAsync(id, group);
            if (updatedGroup is null)
                return NotFound();
            return Ok(updatedGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        { 
            await _groupService.DeleteGroupAsync(id);
            return Ok();
        }
    }
}
