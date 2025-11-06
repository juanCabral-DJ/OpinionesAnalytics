using Microsoft.AspNetCore.Mvc;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;
using OpinionesAnalyticsAPI.DATA.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpinionesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class webReviewsController : ControllerBase
    {
        private readonly ILoggerBase<webReviewsController> _Logger;
        public readonly IWebReviewsRepository _webReviewsRepository;

        public webReviewsController(IWebReviewsRepository webReviewsRepository)
        { 
            this._webReviewsRepository = webReviewsRepository;
        }

        // GET: api/<webReviewsController> 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _webReviewsRepository.GetReviewsAsync();
            
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
