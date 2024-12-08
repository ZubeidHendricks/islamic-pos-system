namespace IslamicPOS.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var userStore = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStore.Object, null, null, null, null, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();

        _authService = new AuthService(
            _userManagerMock.Object,
            _configurationMock.Object,
            Mock.Of<ILogger<AuthService>>()
        );
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var user = new ApplicationUser
        {
            UserName = username,
            Email = "test@example.com",
            IsActive = true
        };

        _userManagerMock.Setup(x => x.FindByNameAsync(username))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(true);

        // Act
        var result = await _authService.LoginAsync(username, password);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Token);
        Assert.NotNull(result.User);
    }
}