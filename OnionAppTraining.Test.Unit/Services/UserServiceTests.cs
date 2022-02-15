using AutoMapper;
using Moq;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnionAppTraining.Test.Unit.Services
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private Mock<IEncrypter> _encrypterMock = new Mock<IEncrypter>();
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();

        [Fact]
        public async Task GivenValidData_WhenUserRegisterInvoked_ShouldCreateUser()
        {
            _encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("salt");
            _encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var userEmail = "useremail@gmail.com";
            var secret = "sercret1";
            var role = "user";
            var username = "createduser1";
            var sut = new UserService(_userRepositoryMock.Object, _encrypterMock.Object, _mapperMock.Object);

            await sut.RegisterAsync(Guid.NewGuid(), userEmail, secret, role, username);

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GivenValidEmail_WhenGetMethodCalled_ShouldReturnUser()
        {
            _encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var userEmail = "useremail@gmail.com";
            var secret = "sercret1";
            var role = "user";
            var username = "createduser1";
            var sut = new UserService(_userRepositoryMock.Object, _encrypterMock.Object, _mapperMock.Object);
            await sut.GetByEmailAsync(userEmail);

            _userRepositoryMock.Setup(x => x.GetByEmailAsync("useremail@gmail.com"))
                              .ReturnsAsync(() => null);

            _userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GivenEmptyUsername_WhenUserRegisterInvoked_ShouldThrowError()
        {
            _encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("salt");
            _encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var userEmail = "useremail@gmail.com";
            var secret = "sercret1";
            var role = "user";
            var username = "";
            var sut = new UserService(_userRepositoryMock.Object, _encrypterMock.Object, _mapperMock.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => sut.RegisterAsync(Guid.NewGuid(), userEmail, secret, role, username));
            Assert.Equal("Field 'username' cannot be empty", exception.Result.Message);
        }

        [Fact]
        public void GivenEmptyPassword_WhenUserRegisterInvoked_ShouldThrowError()
        {
            _encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("salt");
            _encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var userEmail = "useremail@gmail.com";
            var secret = "";
            var role = "user";
            var username = "createduser1";
            var sut = new UserService(_userRepositoryMock.Object, _encrypterMock.Object, _mapperMock.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => sut.RegisterAsync(Guid.NewGuid(), userEmail, secret, role, username));
            Assert.Equal("Field 'password' cannot be empty", exception.Result.Message);
        }
    }
}
