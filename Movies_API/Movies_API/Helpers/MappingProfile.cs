using AutoMapper;
using Movies_API.Dtos;
using Movies_API.Models;

namespace Movies_API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MovieDto, Movie>().ForMember(dest => dest.Poster, opt => opt.Ignore());
        }
    }
}
