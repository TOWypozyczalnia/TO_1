using App.Data.Entities;
using App.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewController(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] Review review)
    {
        _reviewRepository.Add(review);

        return Ok();
    }
}