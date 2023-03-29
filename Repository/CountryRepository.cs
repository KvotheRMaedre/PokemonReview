using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(country => country.Id == id);
        }

        public bool CountryExists(string name)
        {
            return _context.Countries.Any(country => country.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(country => country.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(country => country.Id == id).FirstOrDefault();
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(country => country.Name == name).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
