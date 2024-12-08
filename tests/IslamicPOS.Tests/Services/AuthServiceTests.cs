using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using IslamicPOS.Core.Services.Auth;
using IslamicPOS.Infrastructure.Services.Auth;

namespace IslamicPOS.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserManager> _userManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userManagerMock = new Mock<IUserManager>();
        _tokenServiceMock = new Mock<ITokenService>();
        _loggerMock = new Mock<ILogger<AuthService>>();

        _authService = new AuthService(
            _userManagerMock.Object,
            _tokenServiceMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var username = "testuser";
        var password = "password";
        var user = new ApplicationUser
        {
            Id = "1",
            UserName = username,
            Email = "test@example.com",
            IsActive = true
        };

        _userManagerMock.Setup(x => x.FindByNameAsync(username))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(true);
        _tokenServiceMock.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
            .Returns("token");

        // Act
        var result = await _authService.LoginAsync(username, password);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Token);
        Assert.NotNull(result.User);
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsFailure()
    {
        // Arrange
        var username = "testuser";
        var password = "wrongpassword";
        var user = new ApplicationUser
        {
            Id = "1",
            UserName = username,
            IsActive = true
        };

        _userManagerMock.Setup(x => x.FindByNameAsync(username))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(false);

        // Act
        var result = await _authService.LoginAsync(username, password);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Token);
    }
}