using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using System.Diagnostics.Metrics;

namespace PokemonReview.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
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

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(owner => owner.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}