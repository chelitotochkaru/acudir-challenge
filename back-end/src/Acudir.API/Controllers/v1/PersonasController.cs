using Acudir.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acudir.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonasController : ControllerBase
    {
        #region Readonly Fields

        private readonly ILogger<PersonasController> _logger;
        private readonly IPersonasService _personaService;

        #endregion

        #region Constructor

        public PersonasController(ILogger<PersonasController> logger, IPersonasService personaService)
        {
            _logger = logger;
            _personaService = personaService;
        }

        #endregion


        #region Methods

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            return Ok(await _personaService.GetRandom());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _personaService.Delete(id);
            return new EmptyResult();
        }

        #endregion
    }
}

