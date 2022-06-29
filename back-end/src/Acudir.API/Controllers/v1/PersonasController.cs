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

        #endregion

        #region Constructor

        public PersonasController(ILogger<PersonasController> logger)
        {
            _logger = logger;
        }

        #endregion


        #region Methods

        [HttpGet]
        public IActionResult Get()
        {
            return new EmptyResult();
        }

        #endregion
    }
}

