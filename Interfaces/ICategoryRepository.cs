using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
    }
}
