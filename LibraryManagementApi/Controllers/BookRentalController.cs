using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Helpers_Extensions;
using LibraryManagementApi.Models;
using LibraryManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace LibraryManagementApi.Controllers
{
    [ApiController]
    [Route("api/bookrental")]
    [Authorize]
    public class BookRentalController : ControllerBase
    {
        private readonly IRentService _rentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BookRentalController(IRentService rentService, IMapper mapper, IUserService userService)
        {
            _rentService = rentService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentBooks([FromBody] BookRentalRequestDto rentalDto)
        {
            var hasRentalOverdue = bool.Parse(User.GetClaimValue("BookRentalOverdue"));
            if (hasRentalOverdue)
            {
                return BadRequest("User has Rental Overdue. Please return the overdue books before you rent again! ");
            }
            var result = await _rentService.RentBooks(int.Parse(Helpers.GetClaimValue(User, "UserId")), rentalDto.BookIds);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBooks([FromBody] BookRentalRequestDto rentalDto)
        {
            var result = await _rentService.ReturnBooks(int.Parse(Helpers.GetClaimValue(User, "UserId")), rentalDto.BookIds);

            if (result.IsSuccess)
            {
                var rentalOverdue = await _rentService.HasRentalOverdue(int.Parse(Helpers.GetClaimValue(User, "UserId")));

                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("overdue/{userId}")]
        public async Task<IActionResult> HasRentalOverdue(int userId)
        {
            var hasOverdue = await _rentService.HasRentalOverdue(userId);
            return Ok(hasOverdue);
        }

        [HttpGet("myRentedBooks")]
        public async Task<IActionResult> MyRentedBooks()
        {
            try
            {
                var userId = int.Parse(Helpers.GetClaimValue(User, "UserId"));
                var rentedBooks = await _rentService.GetCurrentlyRentedBooks(userId);
                return Ok(rentedBooks);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unhandled exception occured: {e.Message}");
                return BadRequest(e.Message);
            }
        }
      
    }
}
