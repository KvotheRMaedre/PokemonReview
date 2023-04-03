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

        public bool CreateType(Models.Type type)
        {
            _context.Add(type);
            return Save();
        }

        public ICollection<Pokemon> GetPokemonsByType(int typeId)
        {
            return _context.PokemonTypes
                .Where(pt => pt.TypeId == typeId)
                .Select(pt => pt.Pokemon).ToList();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TypeExists(int id)
        {
            return _context.Types.Any(type => type.Id == id);
        }

        public bool TypeExists(string name)
        {
            return _context.Types.Any(type => type.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }
    }
}
