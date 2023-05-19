using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [ApiController]
    [Route("api/bookrental")]
    [Authorize]
    public class BookRentalController : ControllerBase
    {
        private readonly IRentService _rentService;

        public BookRentalController(IRentService rentService)
        {
            _rentService = rentService;
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
    }
}
