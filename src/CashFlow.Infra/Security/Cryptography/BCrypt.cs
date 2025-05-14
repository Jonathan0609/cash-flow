using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infra.Security.Cryptography;

public class BCrypt : IPasswordEncripter
{
    public string EncryptPassword(string password) => BC.HashPassword(password);

    public bool VerifyPassword(string password, string passwordHash) => BC.Verify(password, passwordHash);
}