using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> getReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewByTitle(string title);
        ICollection<Review> GetReviewBySampleText(string text);
    }
}
