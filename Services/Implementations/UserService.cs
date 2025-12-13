using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;
using SelfLearning.Domain.Entities;
using SelfLearning.Repositories.Interfaces;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : MapToResponse(user);
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToResponse);
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var createdUser = await _userRepository.CreateAsync(user);
        return MapToResponse(createdUser);
    }

    public async Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        var updatedUser = await _userRepository.UpdateAsync(user);
        return MapToResponse(updatedUser);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}

