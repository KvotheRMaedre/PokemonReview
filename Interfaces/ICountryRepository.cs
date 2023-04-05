using PokemonReview.Dto;
using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByName(string name);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool CountryExists(int id);
        bool CountryExists(string name);
        bool Save();
    }
}
