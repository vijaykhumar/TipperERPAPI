using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TipperERP.Domain.Entities;

namespace TipperERP.Application.Users;

public interface IUserService
{
    Task<IEnumerable<UserListDto>> GetAllAsync();
    Task<UserListDto?> GetByIdAsync(Guid userId);
    Task<Guid> CreateAsync(UserCreateDto dto, Guid createdBy);
    Task UpdateAsync(Guid userId, UserUpdateDto dto, Guid updatedBy);
    Task DeleteAsync(Guid userId, Guid deletedBy);
    Task<UserMaster?> GetByUsernameAsync(string username);
}
