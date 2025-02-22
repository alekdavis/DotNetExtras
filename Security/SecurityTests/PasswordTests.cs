using DotNetExtras.Security;

namespace SecurityTests;
public class PasswordTests
{
    [Fact]
    public void Generate_DefaultLength()
    {
        string password;
        
        for (int i = 0; i < 100; i++)
        {
            password = Password.Generate();

            Assert.NotNull(password);
            Assert.InRange(password.Length, 8, 12);
        }
    }

    [Theory]
    [InlineData(8)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(30)]
    [InlineData(124)]
    public void Generate_ExactLength
    (
        int length
    )
    {
        string password = Password.Generate(length);

        Assert.NotNull(password);
        Assert.Equal(length, password.Length);
    }

    [Theory]
    [InlineData(8, 12)]
    [InlineData(10, 20)]
    [InlineData(34, 35)]
    public void Generate_MinMaxLength(int minLength, int maxLength)
    {
        string password = Password.Generate(minLength, maxLength);

        Assert.NotNull(password);
        Assert.InRange(password.Length, minLength, maxLength);
    }
}
