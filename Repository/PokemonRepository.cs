using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using System.Runtime.Intrinsics.Arm;

namespace PokemonReview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository (DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(pokemon => id == pokemon.Id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(pokemon => name == pokemon.Name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(pokemon => pokemon.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(pokemon => id == pokemon.Id);
        }
    }
}
