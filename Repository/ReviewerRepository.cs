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
    }
}
