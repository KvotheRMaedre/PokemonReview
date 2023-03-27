using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategoryByName(string name);

        ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
    }
}
