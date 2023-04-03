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

        public bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId, int typeId)
        {
            var category = _context.Categories.Where(category => category.Id == categoryId).FirstOrDefault();
            var owner = _context.Owners.Where(owner => owner.Id == ownerId).FirstOrDefault();
            var type = _context.Types.Where(type => type.Id == typeId).FirstOrDefault();

            var pokemonCategory = new PokemonCategory
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);

            var pokemonOwner = new PokemonOwner
            {
                Owner = owner,
                Pokemon = pokemon
            };

            _context.Add(pokemonOwner);

            var pokemonType = new PokemonType
            {
                Type = type,
                Pokemon = pokemon
            };

            _context.Add(pokemonType);

            _context.Add(pokemon);
            return Save();
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

        public bool PokemonExists(string name)
        {
            return _context.Pokemons.Any(pokemon => pokemon.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
