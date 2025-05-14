using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Repositories;

internal class UsersRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly CashFlowDbContext _context;
    
    public UsersRepository(CashFlowDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _context.Users
            .AnyAsync(user => user.Email.Equals(email));
    }

    public async Task Register(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}