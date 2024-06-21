using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO_s;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
 
        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwner(int id)
        {
            if (!await _ownerRepository.CheckOwnerExists(id))
                return NotFound($"Owner with id = {id} doesn't exist!");
 
            var owner = await _ownerRepository.GetOwner(id);
 
            return Ok(owner);
        }
 
        [HttpPost]
        public async Task<IActionResult> CreateOwner(NewOwnerDto newOwnerDto)
        {
            foreach (var objectId in newOwnerDto.OwnerObjects)
            {
                if (!await _ownerRepository.CheckObjectExists(objectId))
                    return NotFound($"Object with id = {objectId} doesn't exist!");
            }
 
            var owner = await _ownerRepository.CreateOwner(newOwnerDto);
 
            return Created("api/owners", owner);
        }
    }
}

