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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
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

        [HttpPost]
        public IActionResult PostOwner([FromBody] OwnerPostDto owner)
        {
            if (owner == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMapped = _mapper.Map<Owner>(owner);
            ownerMapped.Country = _countryRepository.GetCountry(owner.CountryId);

            if (!_ownerRepository.CreateOwner(ownerMapped))
                return StatusCode(500, "Something went wrong saving this Owner.");

            return CreatedAtAction("GetOwner", new { id = ownerMapped.Id }, ownerMapped);
        }

        [HttpPut("{ownerId}")]
        public IActionResult UpdateCategory(int ownerId, [FromBody] OwnerPostDto owner)
        {
            if (owner == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryRepository.CountryExists(ownerId))
                return NotFound();

            var ownerMapped = _mapper.Map<Owner>(owner);
            ownerMapped.Id = ownerId;
            ownerMapped.Country = _countryRepository.GetCountry(owner.CountryId);

            if (!_ownerRepository.UpdateOwner(ownerMapped))
                return StatusCode(500, "Something went wrong updating this owner.");

            return CreatedAtAction("GetOwner", new { id = ownerMapped.Id }, ownerMapped);
        }

    }
}
