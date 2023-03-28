using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly DataContext _context;

        public TypeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemonsByType(int typeId)
        {
            return _context.PokemonTypes.Where(pt => pt.TypeId == typeId).Select(pt => pt.Pokemon).ToList();
        }

        public Models.Type GetType(int id)
        {
            return _context.Types.Where(type => type.Id == id).FirstOrDefault();
        }

        public ICollection<Models.Type> GetTypeByName(string name)
        {
            return _context.Types.Where(type => type.Name.Contains(name)).ToList();
        }

        public ICollection<Models.Type> GetTypes()
        {
            return _context.Types.OrderBy(type => type.Id).ToList();
        }
    }
}
