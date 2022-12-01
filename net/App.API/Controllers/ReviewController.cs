using App.Data.Entities;
using App.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Mvc.HttpGet;

namespace App.API.Controllers
{
	public class ReviewController : BaseController
	{
		private readonly IReviewRepository reviewRepository;

		public ReviewController(IReviewRepository reviewRepository)
		{
			this.reviewRepository = reviewRepository;
		}

		[HttpGet("GetAll")]
		public async Task<ActionResult> GetAll()
		{
			return new OkObjectResult(await reviewRepository.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetSingle(int Id)
		{
			CancellationToken cancellation = CancellationToken.None;
			return new OkObjectResult(await reviewRepository.GetSingle(Id, cancellation));
		}

		[HttpPost("AddReview")]
		public async Task<IActionResult> AddReview([FromBody] Review review)
		{
			System.Diagnostics.Debug.WriteLine(review);
			reviewRepository.Add(review);
			return Ok();
		}

		[HttpPost("Update")]
		public async Task<IActionResult> Update([FromBody] Review review)
		{
			reviewRepository.Update(review);
			return Ok();
		}

		[HttpPost("Remove")]
		public async Task<IActionResult> Remove([FromBody] Review review)
		{
			reviewRepository.Remove(review);
			return Ok();
		}
	}
}