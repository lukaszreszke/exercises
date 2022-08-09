using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Api.Tests;

public class AcceptanceTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private readonly HttpClient _httpClient;

    public AcceptanceTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task should_return_only_available_rooms()
    {
        var tomorrow = DateTime.UtcNow.AddDays(1);
        var dbContext =
            new RoomBookingAppDbContext(
                new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("in_memory_database_testing").Options);
        dbContext.Rooms.Add(new Room { Id = 1, Name = "Doh" });
        dbContext.Rooms.Add(new Room { Id = 2, Name = "Meh" });
        dbContext.SaveChanges();

        var bookRoomResponse = await _httpClient.PostAsync("RoomBooking",
            ConvertToHttpContent(new RoomBookingRequest
                { Date = tomorrow, Id = 1, Email = "hej@exampl.com", FullName = "Jacek Kowalski" }));
        bookRoomResponse.EnsureSuccessStatusCode();
        
        var availableRoomsResponse = await _httpClient.GetAsync($"RoomBooking/available?date={DateOnly.FromDateTime(tomorrow)}");
        var availableRooms = JsonConvert.DeserializeObject<List<Room>>(availableRoomsResponse.Content.ReadAsStringAsync().Result);
        availableRooms.ShouldNotBeEmpty();
        availableRooms.Select(x => x.Id).First().ShouldBe(2);
    }

    private static HttpContent ConvertToHttpContent<T>(T data)
    {
        var jsonQuery = JsonConvert.SerializeObject(data);
        HttpContent httpContent = new StringContent(jsonQuery, Encoding.UTF8);
        httpContent.Headers.Remove("content-type");
        httpContent.Headers.Add("content-type", "application/json; charset=utf-8");

        return httpContent;
    }
}