using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReview.Dto;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeController(ITypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTypes() 
        {
            var types = _mapper.Map<List<TypeDto>>(_typeRepository.GetTypes());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(types);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetType(int id)
        {
            var type = _mapper.Map<TypeDto>(_typeRepository.GetType(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (type == null)
                return NotFound("This type doesn't exist.");

            return Ok(type);
        }

        [HttpGet("name={name}")]
        public IActionResult GetTypeByName(string name)
        {
            var types = _mapper.Map<List<TypeDto>>(_typeRepository.GetTypeByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (types.IsNullOrEmpty())
                return NotFound("There are no types matching the name: " + name);

            return Ok(types);
        }

        [HttpGet("pokemons/{typeId}", Name = "GetPokemonsByType")]
        public IActionResult GetPokemonsByType(int typeId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_typeRepository.GetPokemonsByType(typeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pokemons.IsNullOrEmpty())
                return NotFound("There are no pokemons of this type.");

            return Ok(pokemons);
        }

        [HttpPost]
        public IActionResult PostType([FromBody] TypePostDto type)
        {
            if (type == null)
                return BadRequest(ModelState);

            if (_typeRepository.TypeExists(type.Name))
                return StatusCode(422, "This type already exists.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeMapped = _mapper.Map<Models.Type>(type);

            if (!_typeRepository.CreateType(typeMapped))
                return StatusCode(500, "Something went wrong saving this type.");

            return CreatedAtAction("GetType", new { id = typeMapped.Id }, typeMapped);
        }
    }
}
