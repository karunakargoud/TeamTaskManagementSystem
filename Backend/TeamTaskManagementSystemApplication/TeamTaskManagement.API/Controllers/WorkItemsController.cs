using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Services;

namespace TeamTaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemService _service;

        public WorkItemsController(IWorkItemService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin,Manager,User")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound("Work item not found");

            return Ok(result);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(WorkItemDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }
        [Authorize(Roles = "Admin,Manager,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            WorkItemDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound("Work item not found");

            return Ok(result);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound("Work item not found");

            return Ok("Work item deleted successfully");
        }
    }
}
