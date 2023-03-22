using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }
        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(owner => owner.Id == id).FirstOrDefault();
        }

        public Owner GetOwnerByGym(string gym)
        {
            return _context.Owners.Where(owner => owner.Gym == gym).FirstOrDefault();
        }

        public Owner GetOwnerByName(string name)
        {
            return _context.Owners.Where(owner => owner.Name == name).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(owner => owner.Id).ToList();
        }
    }
}
