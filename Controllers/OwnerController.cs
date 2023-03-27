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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(owners);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOwner(int id)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (owner == null)
                return NotFound("This owner don't exist.");

            return Ok(owner);
        }

        [HttpGet("name={name}", Name = "GetOwnerByName")]
        public IActionResult GetOwnerByName(string name)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwnerByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (owner == null)
                return NotFound("This owner don't exist.");

            return Ok(owner);
        }

        [HttpGet("gym={gym}", Name = "GetOwnerByGym")]
        public IActionResult GetOwnerByGym(string gym)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwnerByGym(gym));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (owner == null)
                return NotFound("This owner don't exist.");

            return Ok(owner);
        }

        [HttpGet("pokemons/{ownerId}")]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pokemons.IsNullOrEmpty())
                return NotFound("This person doesn't own pokemons.");

            return Ok(pokemons);
        }

    }
}
