using Doccure.ReviewService.Dtos.ReviewDtos;
using Doccure.ReviewService.Services.ReviewServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.ReviewService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> ReviewList()
        {
            var values = await _reviewService.GetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            await _reviewService.CreateAsync(createReviewDto);
            return Ok("Seccessfull added");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteReview(string id)
        {
            await _reviewService.DeleteAsync(id);
            return Ok("Seccessfull deletted");
        }

        [HttpGet("GetReview")]
        public async Task<IActionResult> GetReview(string id)
        {
            var value = await _reviewService.GetByIdAsync(id);
            return Ok(value);
        }
    }
}
