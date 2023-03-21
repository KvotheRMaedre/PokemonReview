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

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(category => category.Id).ToList();
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.Where(category => category.Name == name).FirstOrDefault();
        }

    }
}
