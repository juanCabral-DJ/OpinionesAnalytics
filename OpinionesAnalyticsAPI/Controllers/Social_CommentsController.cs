using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpinionesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Social_CommentsController : ControllerBase
    {
        private readonly ILoggerBase<Social_CommentsController> _Logger;
        public readonly ISocial_CommentsRepository _Social_CommentsRepository;

        public Social_CommentsController(ISocial_CommentsRepository Social_CommentsRepository, ILoggerBase<Social_CommentsController> logger)
        {
            this._Social_CommentsRepository = Social_CommentsRepository;
            _Logger = logger;
        }

        // GET: api/<Social_CommentsController>
        [HttpGet]  
        public async Task<IActionResult> Get()
        {
            var result = await _Social_CommentsRepository.GetSocial_CommentsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                _Logger.LogError("Error Fetching social comments ");
                return BadRequest(result);
            }
        }
    }
}
