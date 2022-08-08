using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace RoomBookingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingRequestProcessor _roomBookingProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor)
        {
            _roomBookingProcessor = roomBookingProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = _roomBookingProcessor.BookRoom(request);
            if(result.Flag == Core.Enums.BookingResultFlag.Success)
            {
                return Ok(result);
            }

            ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No Rooms Available For Given Date");

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(_roomBookingProcessor.Rooms());
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(DateTime date)
        {
            return Ok(_roomBookingProcessor.AvailableRooms(date));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            return Ok(_roomBookingProcessor.Room(id));
        }
    }

    public static class HttpContextExtensions
    {
        public static string GetUserId(this IHttpContextAccessor context)
        {
           return context.HttpContext?.User.FindFirstValue("id_sub");
        }
    }
    
    public static class StringExtensions
    {
        public static bool HasValue(this string @string) => !string.IsNullOrEmpty(@string);
    }
}