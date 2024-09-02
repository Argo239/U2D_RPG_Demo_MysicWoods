using Microsoft.AspNetCore.Mvc;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;
using U2D_RPG_Demo.ApiServer.Repository;

namespace U2D_RPG_Demo.ApiServer.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class PlayerAttributeController : ControllerBase {
        private readonly DataContext _dataContext;
        private readonly IPlayerAttributeRepository _playerAttributeRepo;

        public PlayerAttributeController(DataContext dataContext, IPlayerAttributeRepository playerAttributeRepo) {
            _dataContext = dataContext;
            _playerAttributeRepo = playerAttributeRepo;
        }

        [HttpGet("getById/{uid}")]
        public async Task<IActionResult> GetAttributeById([FromRoute]int uid, CancellationToken cancellation) {
            var playerAttributeModel = await _playerAttributeRepo.GetAttributesByIdAsync(uid, cancellation);  
            return playerAttributeModel == null ? NotFound() : Ok(playerAttributeModel);
        }

        [HttpPost("create/{uid}")]
        public async Task<IActionResult> CreateAttribute([FromRoute] int uid, [FromBody] CreatePlayerAttributeRequestDTO createDTO, CancellationToken cancellation) {
            var playerAttributeModel = createDTO.ToCreatePlayerAttributeDTO(uid);
            await _playerAttributeRepo.CreateAttributesAsync(playerAttributeModel, cancellation);
            return CreatedAtAction(nameof(GetAttributeById), new { id = playerAttributeModel.PAID }, playerAttributeModel.ToPlayerAttributeDTO());
        }

        [HttpPut("update/{uid}")]
        public async Task<IActionResult> UpdateAttribute([FromRoute] int uid, [FromBody] UpdatePlayerAttributeRequestDTO updateDTO, CancellationToken cancellation) {
            var playerAttributeModel = await _playerAttributeRepo.UpdateAttributesAsync(uid, updateDTO, cancellation);
            if(playerAttributeModel == null)
                return NotFound();
            return Ok(playerAttributeModel.ToPlayerAttributeDTO());    
        }
    }
}
