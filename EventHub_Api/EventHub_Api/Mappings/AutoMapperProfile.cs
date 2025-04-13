using AutoMapper;
using EventHub_Api.DTOs;
using EventHub_Api.Models;

namespace EventHub_Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
