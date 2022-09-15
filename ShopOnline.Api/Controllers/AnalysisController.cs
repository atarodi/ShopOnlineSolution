using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisRepository AnalysisRepository;

        public AnalysisController(IAnalysisRepository analysisRepository)
        {
            this.AnalysisRepository = analysisRepository;
        }

        [HttpGet]
        [Route("GetRandomData/{count}")]
        public async Task<ActionResult<IEnumerable<int>>> GetItems(int count)
        {
            try
            {
                var cartItems =  this.AnalysisRepository.GetRandomData(count);

                if (cartItems == null)
                {
                    return NoContent();
                }

                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
