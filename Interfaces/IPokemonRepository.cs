using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId, int typeId);
        bool PokemonExists(int id);
        bool PokemonExists(string name);
        bool Save();
    }
}
