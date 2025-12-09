using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Users;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;
using TipperERP.Infrastructure.Security;

namespace TipperERP.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly TipperErpDbContext _db;

    public UserService(TipperErpDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserListDto>> GetAllAsync()
    {
        return await _db.Users
            .Include(u => u.Role)
            .Where(u => !u.IsDeleted)
            .Select(u => new UserListDto
            {
				UserId = u.UserId,
                FullName = u.FullName,
                Username = u.Username,
                Email = u.Email,
                RoleName = u.Role.RoleName,
                IsActive = u.IsActive
            })
            .ToListAsync();
    }

    public async Task<UserListDto?> GetByIdAsync(Guid userId)
    {
        return await _db.Users
            .Include(u => u.Role)
            .Where(u => u.UserId == userId && !u.IsDeleted)
            .Select(u => new UserListDto
            {
				UserId = u.UserId,
                FullName = u.FullName,
                Username = u.Username,
                Email = u.Email,
                RoleName = u.Role.RoleName,
                IsActive = u.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(UserCreateDto dto, Guid createdBy)
    {
        var user = new UserMaster
        {
			UserId = Guid.NewGuid(),
            CompanyId = dto.CompanyId,
            BranchId = dto.BranchId,
            RoleId = dto.RoleId,
            FullName = dto.FullName,
            Username = dto.Username,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            Email = dto.Email,
            MobileNo = dto.MobileNo,
            IsActive = dto.IsActive,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user.UserId;
    }

    public async Task UpdateAsync(Guid userId, UserUpdateDto dto, Guid updatedBy)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
        if (user == null) throw new KeyNotFoundException("User not found");

        user.CompanyId = dto.CompanyId;
        user.BranchId = dto.BranchId;
        user.RoleId = dto.RoleId;
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.MobileNo = dto.MobileNo;
        user.IsActive = dto.IsActive;
        user.UpdatedBy = updatedBy;
        user.UpdatedDate = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId, Guid deletedBy)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
        if (user == null) return;

        user.IsDeleted = true;
        user.UpdatedBy = deletedBy;
        user.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }

    public async Task<UserMaster?> GetByUsernameAsync(string username)
    {
        try
        {
            return await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted && u.IsActive);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
