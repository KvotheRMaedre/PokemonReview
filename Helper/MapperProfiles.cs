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
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryPostDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Models.Type, TypeDto>();
        }
    }
}
