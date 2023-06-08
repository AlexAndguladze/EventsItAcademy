using EventsItAcademy.Application.CustomExcepitons;
using EventsItAcademy.Application.Events;
using EventsItAcademy.Application.Events.Repositories;
using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Events.Responses;
using EventsItAcademy.Domain.Events;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace EventsItAcademy.Application.Tests.Events
{
    public class EventServiceTests
    {
        public EventServiceTests()
        {

        }
        [Fact]
        public async Task AddEvent_WhenDataIsCorrect_ShouldReturnId()
        {
            int expectedId = 1;
            var mockRepo = new Mock<IEventRepository>(MockBehavior.Strict);
            var eventService = new EventService(mockRepo.Object);
            //var mockEvent = new Mock<Event>
            var token = new CancellationToken();

            mockRepo.Setup(x => x.AddAsync(token, It.IsAny<Event>())).ReturnsAsync(1);

            var eventRequestModel = GetEventRequestModel();
            var result = await eventService.AddAsync(token, eventRequestModel, new Guid().ToString());

            Assert.Equal(result, expectedId);
            Assert.IsType<int>(result);
        }
        [Fact]
        public async Task GetEvent_WhenDataIsCorret_ShouldReturnEventResponseModel()
        {

            var mockRepo = new Mock<IEventRepository>(MockBehavior.Strict);
            var eventService = new EventService(mockRepo.Object);

            var eventId = 1;
            var userId = new Guid().ToString();
            var token = new CancellationToken();

            mockRepo.Setup(x => x.GetAsync(token, eventId, userId)).ReturnsAsync(new Event()
            {
                Id = 1,
                OwnerId = userId,
                Title = "title",
                Destription = " desc",
                TicketQuantity = 100,
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3),
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                ImageId = null
            });

            var result = await eventService.GetByIdAsync(token, eventId, userId);
            Assert.IsType<EventResponseModel>(result);
            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
            Assert.Equal(userId, result.OwnerId);
        }
        [Fact]
        public async Task GetEvent_WhenIdDoesNotExist_ShouldReturnException()
        {

            var mockRepo = new Mock<IEventRepository>(MockBehavior.Strict);
            var eventService = new EventService(mockRepo.Object);

            var userId = new Guid().ToString();
            var token = new CancellationToken();

            mockRepo.Setup(x => x.GetAsync(token, It.IsAny<int>(), userId)).ThrowsAsync(new ItemNotFoundException("item not found"));

            await Assert.ThrowsAsync<ItemNotFoundException>(() => eventService.GetByIdAsync(token, 3, userId));

        }
        [Fact]
        public async Task GetEvent_WhenDataIsCorrect_PropertiesAreCorrect()
        {

            var mockRepo = new Mock<IEventRepository>(MockBehavior.Strict);
            var eventService = new EventService(mockRepo.Object);

            var eventId = 1;
            var userId = new Guid().ToString();
            var token = new CancellationToken();

            mockRepo.Setup(x => x.GetAsync(token, eventId, userId)).ReturnsAsync(new Event()
            {
                Id = 1,
                OwnerId = userId,
                Title = "title",
                Destription = " desc",
                TicketQuantity = 100,
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3),
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                ImageId = null
            });

            var result = await eventService.GetByIdAsync(token, eventId, userId);
            result.Title.Should().NotBeEmpty();
            result.Destription.Should().NotBeEmpty();
            result.TicketQuantity.Should().BeGreaterThan(0);

        }


        public Event GetEventModel()
        {
            return new Event
            {
                Id = 1,
                Title = "title",
                Destription = "desc",
                TicketQuantity = 100,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                IsArchived = false
            };
        }
        public EventRequestModel GetEventRequestModel()
        {
            return new EventRequestModel
            {
                Title = "title",
                Destription = "desc",
                TicketQuantity = 100,
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(4)
            };
        }
    }
}
