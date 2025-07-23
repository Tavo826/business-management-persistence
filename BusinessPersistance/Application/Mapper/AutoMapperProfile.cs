using AutoMapper;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MessageRequestDto, Message>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Message, MessageResponseDto>();
        }
    }
}
