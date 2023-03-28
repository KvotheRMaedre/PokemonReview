using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReview.Dto;
using PokemonReview.Interfaces;

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
    }
}
