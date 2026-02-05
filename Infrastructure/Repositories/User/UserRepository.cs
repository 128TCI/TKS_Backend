using Dapper;
using DomainEntities.DTO;
using DomainEntities.DTO.User;
using Infrastructure.IRepositories.UserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.DTOs.User;
using System.Data;
using Timekeeping.Infrastructure.Data;
namespace Infrastructure.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly TimekeepingContext _context;
   


    public UserRepository(TimekeepingContext context)
    {
        _context = context;
        
    }

    public async Task<UserDTO> InsertAsync(UserDTO employee)
    {
        _context.tk_Users.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<UserDTO> UpdateAsync(UserDTO employee)
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

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        return await _context.tk_Users.FindAsync(id);
    }

    public async Task<List<UserDTO>> GetAllAsync()
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
    public async Task<UserDTO?> GetByUserNameAsync(string userName)
    {
        return await _context.tk_Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }
   
}