using DomainEntities.DTO;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;
using Infrastructure.IRepositories.UserRepository;
using DomainEntities.DTO.User;
namespace Infrastructure.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly TimekeepingContext _context;

    public UserRepository(TimekeepingContext context)
    {
        _context = context;
    }

    public async Task<User> InsertAsync(User employee)
    {
        _context.tk_Users.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<User> UpdateAsync(User employee)
    {
        _context.tk_Users.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.tk_Users.FindAsync(id);
        if (employee == null) return false;

        _context.tk_Users.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.tk_Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.tk_Users
            // Filtering where IsSuspended is false (or null)
            // Since IsSuspended is bool?, we compare it explicitly
            .Where(u => u.IsSuspended == false || u.IsSuspended == null)
            .ToListAsync();
    }
    public async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _context.tk_Users.AnyAsync(u => u.UserName == userName);
    }
    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.tk_Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}