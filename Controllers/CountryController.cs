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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
        [ProducesResponseType(400)]
        public IActionResult getCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{id:int}", Name = "GetCountryById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getCountriesById(int id)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (country == null)
                return NotFound("This country doesn't exist.");

            return Ok(country);
        }
        
        [HttpGet("{name}", Name = "GetCountryByName")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getCountriesByName(string name)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (country == null)
                return NotFound("This country doesn't exist.");

            return Ok(country);
        }

        [HttpPost]
        public IActionResult PostCountry([FromBody] CountryPostDto country)
        {
            if (country == null)
                return BadRequest(ModelState);

            if(_countryRepository.CountryExists(country.Name))
                return StatusCode(422, "This country already exists.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMapped = _mapper.Map<Country>(country);

            if(!_countryRepository.CreateCountry(countryMapped))
                return StatusCode(500, "Something went wrong saving this category.");

            return CreatedAtAction("getCountriesById", new { id = countryMapped.Id }, countryMapped);
        }

    }
}
