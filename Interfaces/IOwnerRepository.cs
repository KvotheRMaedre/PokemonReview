using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        Owner GetOwnerByName(string name);
        Owner GetOwnerByGym(string gym);
        ICollection<Pokemon> GetPokemonsByOwner(int ownerId);
    }
}
