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
    }
}