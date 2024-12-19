using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/parameters")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private ILogger<ParameterController> _logger;

        public ParameterController(ILogger<ParameterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
