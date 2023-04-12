using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{name}", Name = "GetCategoryByName")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(string name)
        {
            if(!_categoryRepository.CategoryExists(name))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemons/{categoryId}")]
        public IActionResult getPokemonsByCategory(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonsByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pokemons.IsNullOrEmpty())
                return NotFound("This category doesn't exist or have pokemons in it.");

            return Ok(pokemons);
        }

        [HttpPost]
        public IActionResult PostCategory([FromBody] CategoryPostDto category)
        {
            if (category == null)
                return BadRequest(ModelState);

            if (_categoryRepository.CategoryExists(category.Name))
                return StatusCode(422, "This category already exists.");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(category);

            if (!_categoryRepository.CreateCategory(categoryMap))
                return StatusCode(500, "Something went wrong saving this category.");


            return CreatedAtAction("GetCategoryById", new { id = categoryMap.Id }, categoryMap);
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryPostDto category)
        {
            if (category == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryMap = _mapper.Map<Category>(category);
            categoryMap.Id = categoryId;

            if (!_categoryRepository.UpdateCategory(categoryMap))
                return StatusCode(500, "Something went wrong updating this category.");

            return CreatedAtAction("GetCategoryById", new { id = categoryMap.Id }, categoryMap);
        }
    }
}
