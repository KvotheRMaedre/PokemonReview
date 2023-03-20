using AutoMapper;
using PokemonReview.Dto;
using PokemonReview.Models;

namespace PokemonReview.Helper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
