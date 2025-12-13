using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;

namespace SelfLearning.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse?> GetByIdAsync(int id);
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> CreateAsync(CreateUserRequest request);
    Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request);
    Task<bool> DeleteAsync(int id);
}

