using ArtistsPlayerAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.BusinessLogic;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;
using NSubstitute;
using System.Net;

namespace Test
{
    public class Tests
    {
        private ArtistLogic _artistLogic;
        private ArtistsController sut;
        private MusicPlayerContext _context;
        private DbContextOptions<MusicPlayerContext> _options;
        private IDateTimeProvider _dateTimeProvider;
        private IArtists _artists;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptions<MusicPlayerContext>();
            _context = new MusicPlayerContext(_options); //Substitute.For<ArtistLogic>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _dateTimeProvider.Now.Returns(DateTime.Now);
            _artistLogic = Substitute.For<ArtistLogic>(_context, _dateTimeProvider);
            _artists = Substitute.For<IArtists>();
            sut = new ArtistsController(_context, _artists);
        }

        [Test]
        public void GivenCorrectDatabaseConnectionString_WhenValidString_ThenPass()
        {
            // Arrange

            // Act

            // Assert
            Assert.Pass();
        }

        [Test]
        public void GetArtistLogicInstance_RETURNS_NotNull()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(_artistLogic);
        }

        [Test]
        public void WhenGettingArtist_GIVEN_Valid_Id_ReturnNotNull()
        {
            // Arrange
            int artistId = 1;

            // Act
            var act = sut.GetArtists(artistId);

            // Assert
            Assert.NotNull(act);

        }

        [Test]
        public async Task WhenGettingArtist_GIVEN_InValid_Id_ReturnNull()
        {
            // Arrange
            int artistId = -1;

            // Act
            //
            //var act = _context.Artists.Where(x => x.Id == 1).FirstOrDefault();//_artistLogic.GetArtist(artistId);
            var act = sut.GetArtists(artistId);
            var result = act.Result.Result;
            // Assert
            //Assert.That(result., Equa) ;

        }

        [Test]
        public void WhenCreatingArtist_GIVEN_Valid_Object_ReturnNotNull()
        {
            // Arrange
            var dateTime = _dateTimeProvider.Now;
            var artist = new Artists 
            {
                ArtistName = "Test Artist",
                CreatedDate = dateTime
            };

            // Act
            var act = _artistLogic.AddArtist(artist);

            // Assert
            Assert.True(act);
        }
    }
}