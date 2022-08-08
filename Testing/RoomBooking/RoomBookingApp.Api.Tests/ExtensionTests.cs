using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RoomBookingApp.Api.Controllers;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Domain;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Api.Tests;

public class ExtensionTests
{
    [Fact]
    public void returns_true_when_string_has_value()
    {
        Assert.True("hej".HasValue());
    }

    [Fact]
    public void returns_false_when_string_has_no_value()
    {
        Assert.False("".HasValue());
    }

    [Fact]
    public void returns_false_when_object_is_null()
    {
        string? s = null;
        Assert.False(s.HasValue());
    }

    [Fact]
    public void returns_user_id_when_claim_is_set()
    {
        var stub = new Mock<IHttpContextAccessor>();
        var claimsIdentity = new List<ClaimsIdentity>()
            { new ClaimsIdentity(new Claim[] { new Claim("id_sub", "1") }) };
        stub.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal(claimsIdentity));

        Assert.Equal("1",  stub.Object.GetUserId());
    }

    [Fact]
    public void returns_null_when_claim_is_not_set()
    {
        var stub = new Mock<IHttpContextAccessor>();
        var claimsIdentity = new List<ClaimsIdentity>()
            { new ClaimsIdentity(new Claim[] { }) };
        stub.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal(claimsIdentity));

        Assert.Null(stub.Object.GetUserId());
    }

    [Fact]
    public void returns_all_booking_ids()
    {
        var room = new Room() { RoomBookings = new List<RoomBooking>() };
        Enumerable.Range(1, 5).ToList().ForEach(i =>
        {
            room.RoomBookings.Add(new RoomBooking
                { Date = DateTime.Today, Email = "email@wp.pl", FullName = "Test", Id = i });
        });

        room.AllRoomBookings().Length.ShouldBe(5);
        room.AllRoomBookings().ShouldBe(new int[] {1,2,3,4,5} );
    }

    [Fact]
    public void returns_expected_page_data()
    {
        new TestPagedQuery(skip: 10, take: 15).GetPageData().ShouldBe(new PagedData(10, 15));
    }

    [Fact]
    public void returns_rooms_without_booking()
    {
        var rooms = Enumerable.Range(1, 5).ToList().Select((_, index) =>
        {
            var room = new Room
            {
                RoomBookings = new List<RoomBooking>()
            };

            if (index % 2 == 0)
                room.RoomBookings.Add(new RoomBooking() { Id = index });

            return room;
        }).ToList();

        var result = rooms.WithoutBooking();
        result.Count.ShouldBe(3);
        result.SelectMany(x => x.RoomBookings.Select(y => y.Id)).ShouldBe(new[] { 0, 2, 4 });
    }

    [Fact]
    public void adds_service_into_collection()
    {
        var sut = new ServiceCollection();
            
        sut.RegisterStuff();

        Assert.Contains(sut, x => x.ServiceType == typeof(IRoomBookingRequestProcessor));
    }
}

public static class ServicesExtensions
{
    public static void RegisterStuff(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();
    }
}

public static class RoomExtensions
{
    public static List<Room> WithoutBooking(this List<Room> rooms)
    {
        return rooms.Where(x => x.RoomBookings.Count > 0).ToList();
    }
}

public class TestPagedQuery : IPagedQuery
{
    public TestPagedQuery(int? skip, int? take)
    {
        Skip = skip;
        Take = take;
    }

    public int? Skip { get; }
    public int? Take { get; }
}

public record PagedData(int Offset, int Next);

public static class PagedQueryHelper
{
    public static PagedData GetPageData(this IPagedQuery query)
    {
        var skip = query.Skip ?? 0;

        var take = query.Take.HasValue && query.Take != 0 ? query.Take.Value : int.MaxValue;

        return new PagedData(skip, take);
    }
}

public interface IPagedQuery
{
    int? Skip { get; }
    int? Take { get; }
}