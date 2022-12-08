using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace App.Test.Controllers
{
    public class ReservationControllerTest
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationControllerTest()
        {
            _reservationRepository = A.Fake<IReservationRepository>();
        }

        [Fact]
        public async void ReservationController_AddReservation_ReturnOk()
        {
            //Arrange
            ReservationController controller = new(_reservationRepository);
            Reservation reservation = new();

            //Act
            IActionResult result = await controller.AddReservation(reservation);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void ReservationController_AddReservationWithInvalidMovieId_ReturnBadRequest()
        {
            //Arrange
            Mock<IReservationRepository> reservationRepository = new();
            Reservation reservation = new() { MovieId = 2 };
            reservationRepository.Setup(r => r.Add(reservation)).Throws(new Microsoft.EntityFrameworkCore.DbUpdateException());

            ReservationController controller = new(reservationRepository.Object);

            //Act
            IActionResult result = await controller.AddReservation(reservation);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}