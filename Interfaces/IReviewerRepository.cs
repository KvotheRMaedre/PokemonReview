using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Reviewer> GetReviewerByFirstName(string name);
        ICollection<Review> GetReviewsOfAReviewer(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool ReviewerExists(int id);
        bool Save();
    }
}
