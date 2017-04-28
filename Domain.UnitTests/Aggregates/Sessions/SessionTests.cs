using System;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;
using Domain.Aggregates.Sessions;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Aggregates.Sessions
{
    public class SessionTests
    {
        private readonly Screen _screen;
        private readonly Film _film;

        private readonly Session _sut;

        public SessionTests()
        {
            _screen = new Screen(new Cinema("Cinemundo"), "Guara", 2, 2);
            _film = new Film("Pulp fiction", 120);

            _sut = new Session(_screen, _film, DateTime.Today.AddHours(17));
        }

        [Fact]
        public void Can_Publish_Unpublished_Session()
        {
            // Act
            _sut.Publish();

            // Assert
            _sut.IsPublished.Should().BeTrue();
        }

        [Fact]
        public void Can_Publish_Published_Session()
        {
            // Arrange
            _sut.Publish();

            // Act
            _sut.Publish();

            // Assert
            _sut.IsPublished.Should().BeTrue();
        }

        [Fact]
        public void Can_Unpublish_Unpublished_Session()
        {
            // Act
            _sut.Unpublish();

            // Assert
            _sut.IsPublished.Should().BeFalse();
        }

        [Fact]
        public void Can_Unpublish_Published_Session()
        {
            // Arrange
            _sut.Publish();

            // Act
            _sut.Unpublish();

            // Assert
            _sut.IsPublished.Should().BeFalse();
        }
    }
}
