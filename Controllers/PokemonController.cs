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
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository,
                                 ICategoryRepository categoryRepository,
                                 IOwnerRepository ownerRepository,
                                 ITypeRepository typeRepository,
                                 IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _categoryRepository = categoryRepository;
            _ownerRepository = ownerRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemon(int id)
        {
            if(!_pokemonRepository.PokemonExists(id))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemon(string name)
        {
            if(!_pokemonRepository.PokemonExists(name))
                return NotFound();
            
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostPokemon([FromBody] PokemonPostDto pokemon)
        {
            if (pokemon == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(_pokemonRepository.PokemonExists(pokemon.Name))
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "This pokemon already exists.");

            if (!_categoryRepository.CategoryExists(pokemon.CategoryId))
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "This category doesn't exist, please check the id and try again.");

            if (!_ownerRepository.OwnerExists(pokemon.OwnerId))
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "This owner doesn't exist, please check the id and try again.");

            if (!_typeRepository.TypeExists(pokemon.TypeId))
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "This type doesn't exist, please check the id and try again.");

            var pokemonMapped = _mapper.Map<Pokemon>(pokemon);

            if (!_pokemonRepository.CreatePokemon(pokemonMapped, pokemon.CategoryId, pokemon.OwnerId, pokemon.TypeId))
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong saving this pokemon.");

            return CreatedAtAction("GetPokemon", new { id = pokemonMapped.Id }, pokemonMapped);
        }

    }
}
