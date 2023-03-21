using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Dto;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using PokemonReview.Repository;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult getCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategoryById(int id)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (category == null)
                return NotFound("This category doesn't exist.");

            return Ok(category);
        }

        [HttpGet("{name}", Name = "GetCategoryByName")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getCategoryByName(string name)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (category == null)
                return NotFound("This category doesn't exist.");

            return Ok(category);
        }
    }
}
