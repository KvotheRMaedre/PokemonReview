using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReview.Dto;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using System.Collections.Generic;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetReviewer(int id)
        {
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reviewer == null)
                return NotFound("This reviewer doesn't exist.");

            return Ok(reviewer);
        }

        [HttpGet("{name}")]
        public IActionResult GetReviewerByName(string name)
        {
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewerByFirstName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reviewer.IsNullOrEmpty())
                return NotFound("There are no reviewer matching the name: " + name);

            return Ok(reviewer);
        }
    }
}
