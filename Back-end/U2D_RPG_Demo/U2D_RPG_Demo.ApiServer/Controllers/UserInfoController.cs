using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase {
        private readonly DataContext _dataContext;
        private readonly IUserInfoRepository _userInfoRepo;

        public UserInfoController(DataContext dataContext, IUserInfoRepository userInfoRepo) {
            _userInfoRepo = userInfoRepo;
            _dataContext = dataContext;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellation) {
            var userInfoModel = await _userInfoRepo.GetAllAsync(cancellation);
            return Ok(userInfoModel.Select(u => u.ToUserInfoDTO()));
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id, CancellationToken cancellation) {
            var userInfoModel = await _userInfoRepo.GetByIdAsync(id, cancellation);
            if (userInfoModel == null || userInfoModel.HasDelete != 0)
                return NotFound();
            return Ok(userInfoModel.ToUserInfoDTO());    
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserInfoRequestDTO createDTO, CancellationToken cancellation) {
            var userInfoModel = createDTO.ToUserInfoFromCreateDTO();
            await _userInfoRepo.CreateUserAsync(userInfoModel, cancellation);
            return CreatedAtAction(nameof(GetById), new { id = userInfoModel.Uid }, userInfoModel.ToUserInfoDTO());
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdataUserInfoRequestDTO UpdataDTO, CancellationToken cancellation) {
            var userInfoModel = await _userInfoRepo.UpdateUserInfoAsync(id, UpdataDTO, cancellation);
            if (userInfoModel == null)
                return NotFound();
            return Ok(userInfoModel.ToUserInfoDTO());
        }

        [HttpPut]
        [Route("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete([FromRoute] int id, [FromBody] SoftDeleteUserInfoRequestDTO softDeleteDTO, CancellationToken cancellation) {
            var userInfoModel = await _userInfoRepo.SoftDeleteUserInfoAsync(id, softDeleteDTO, cancellation);
            if (userInfoModel == null)
                return NotFound();
            return Ok(userInfoModel.ToUserInfoDTO());
        }

        [HttpDelete]
        [Route("hard-delete/{id}")]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellation) {
            var userInfoModel = await _userInfoRepo.HardDeleteUserInfoAsync(id, cancellation);
            if (userInfoModel == null)   
                return NotFound();
            return NoContent();
        }
    }
}


