using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(category => category.Id == id);
        }

        public bool CategoryExists(string name)
        {
            return _context.Categories.Any(category => category.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(category => category.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(category => category.Id == id).FirstOrDefault();
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.Where(category => category.Name == name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(p => p.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }
    }
}
