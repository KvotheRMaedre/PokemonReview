using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(reviewer => reviewer.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewerByFirstName(string name)
        {
            return _context.Reviewers.Where(reviewer => reviewer.FirstName.Contains(name)).ToList();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(reviewer => reviewer.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAReviewer(int reviewerId)
        {
            return _context.Reviews.Where(review => review.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(reviewer => reviewer.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
