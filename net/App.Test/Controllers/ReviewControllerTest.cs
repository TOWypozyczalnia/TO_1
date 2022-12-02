using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

namespace App.Test.Controllers
{
	public class ReviewControllerTest
	{
		private readonly IReviewRepository _reviewRepository;

		public ReviewControllerTest()
		{
			_reviewRepository = A.Fake<IReviewRepository>();
		}

		[Fact]
		public async void ReviewController_AddReview_ReturnOk()
		{
			//Arrange
			ReviewController controller = new(_reviewRepository);
			Review review = new();

			//Act
			IActionResult result = await controller.AddReview(review);

			//Assert
			Assert.IsType<OkResult>(result);
		}

		[Fact]
		public async void ReviewController_AddReviewWithInvalidMovieId_ReturnBadRequest()
		{
			//Arrange
			Mock<IReviewRepository> reviewRepository = new();
			Review review = new() { MovieId = 2 };
			reviewRepository.Setup(r => r.Add(review)).Throws(new Microsoft.EntityFrameworkCore.DbUpdateException());

			ReviewController controller = new(reviewRepository.Object);
			
			//Act
			IActionResult result = await controller.AddReview(review);

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}
	}
}