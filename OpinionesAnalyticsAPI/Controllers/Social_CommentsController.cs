using Microsoft.AspNetCore.Mvc;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;
using OpinionesAnalyticsAPI.DATA.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpinionesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Social_CommentsController : ControllerBase
    {
        private readonly ILoggerBase<Social_CommentsController> _Logger;
        public readonly ISocial_CommentsRepository _Social_CommentsRepository;

        public Social_CommentsController(ISocial_CommentsRepository Social_CommentsRepository)
        { 
            this._Social_CommentsRepository = Social_CommentsRepository;
        }

        // GET: api/<webReviewsController> 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _Social_CommentsRepository.GetReviewsAsync();
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _Logger.LogError("Error Fetching Address ");
                return BadRequest(result);
            }
        }


    }
}
