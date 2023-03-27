using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReview.Dto;
using PokemonReview.Interfaces;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult getReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.getReviews());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public IActionResult getReview(int id)
        {
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (review == null) 
                return NotFound("This review doesn't exist.");

            return Ok(review);

        }

        [HttpGet("title={title}")]
        public IActionResult getReviewByTitle(string title)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewByTitle(title));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reviews.IsNullOrEmpty())
                return NotFound("There are no reviews matching the title: " + title);

            return Ok(reviews);

        }

        [HttpGet("text={text}")]
        public IActionResult getReviewByText(string text)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewBySampleText(text));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reviews.IsNullOrEmpty())
                return NotFound("There are no reviews matching the text: " + text);

            return Ok(reviews);

        }

        [HttpGet("pokemon/{pokeId}")]
        public IActionResult getReviewsOfAPokemon(int pokeId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reviews.IsNullOrEmpty())
                return NotFound("There are no reviews for this pokemon.");

            return Ok(reviews);

        }


    }
}
