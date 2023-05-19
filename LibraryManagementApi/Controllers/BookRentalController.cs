using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Helpers_Extensions;
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
        private readonly IMapper _mapper;

        public BookRentalController(IRentService rentService, IMapper mapper)
        {
            _rentService = rentService;
            _mapper = mapper;
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentBooks([FromBody] BookRentalRequestDto rentalDto)
        {
            var result = await _rentService.RentBooks(rentalDto.UserId, rentalDto.BookIds);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBooks([FromBody] BookRentalRequestDto rentalDto)
        {
            var result = await _rentService.ReturnBooks(rentalDto.UserId, rentalDto.BookIds);

            if (result.IsSuccess)
            {
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
                var userId = int.Parse(Helpers.GetClaimValue(User, "Id"));
                var rentedBooks = await _rentService.GetCurrentlyRentedBooks(userId);
                var model = _mapper.Map<IEnumerable<BookDto>>(rentedBooks);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
