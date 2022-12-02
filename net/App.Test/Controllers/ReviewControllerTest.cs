using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Xunit;

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
	}
}