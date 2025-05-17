using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public readonly Mock<IPasswordEncripter> _passwordEncripterMock;
    
    public PasswordEncripterBuilder()
    {
        _passwordEncripterMock = new Mock<IPasswordEncripter>();
        
        _passwordEncripterMock
            .Setup(passwordEncripter => passwordEncripter.EncryptPassword(It.IsAny<string>()))
            .Returns("password");
    }
    
    public void VerifyPassword(string password)
    {
        _passwordEncripterMock.Setup(passwordEncripter => passwordEncripter.VerifyPassword(password, It.IsAny<string>())).Returns(true);
    }
    
    public IPasswordEncripter Build() => _passwordEncripterMock.Object;
}