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
            CreateMap<PokemonPostDto, Pokemon>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryPostDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerPostDto, Owner>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewPostDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerPostDto, Reviewer>();
            CreateMap<Models.Type, TypeDto>();
            CreateMap<TypePostDto, Models.Type>();
        }
    }
}
