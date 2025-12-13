using AutoMapper;
using SelfLearning.Domain.Entities;
using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;

namespace SelfLearning.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User mappings
        CreateMap<User, UserResponse>();
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
    }
}

