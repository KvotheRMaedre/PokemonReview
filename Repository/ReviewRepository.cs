﻿using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PokemonReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(review => review.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewBySampleText(string text)
        {
            return _context.Reviews.Where(review => review.Text.Contains(text)).ToList();
        }

        public ICollection<Review> GetReviewByTitle(string title)
        {
            return _context.Reviews.Where(review => review.Title.Contains(title)).ToList();
        }

        public ICollection<Review> getReviews()
        {
            return _context.Reviews.OrderBy(review => review.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(review => review.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(review => review.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
