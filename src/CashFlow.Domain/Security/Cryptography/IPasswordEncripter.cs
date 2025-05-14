namespace CashFlow.Domain.Security.Cryptography;

public interface IPasswordEncripter
{
    string EncryptPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
}