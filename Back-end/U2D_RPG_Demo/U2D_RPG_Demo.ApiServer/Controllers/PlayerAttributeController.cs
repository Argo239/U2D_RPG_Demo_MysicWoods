using Microsoft.AspNetCore.Mvc;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class PlayerAttributeController : ControllerBase {
        private readonly DataContext _dataContext;

        public PlayerAttributeController(DataContext dataContext) {
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeById([FromRoute]int id, CancellationToken cancellation) {
            var result = await _dataContext.PlayerAttributes.FindAsync(id, cancellation);
            if (result == null) 
                return NotFound();
            return Ok(result.ToPlayerAttributeDto());  
        } 
    }
}
