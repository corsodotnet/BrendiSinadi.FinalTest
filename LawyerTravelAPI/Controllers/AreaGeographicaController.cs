using LawyerTravelAPI.Models;
using LawyerTravelAPI.Persistence.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;



namespace LawyerTravelAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AreaGeographicaController : ControllerBase
    {
        private readonly ILogger<AreaGeographicaController> _logger;
        private readonly IMapper _mapper;
        private DatabaseCxt _context;
        private IOptions<AppSettings> _setting;
        public AreaGeographicaController(ILogger<AreaGeographicaController> logger,
            DatabaseCxt ctx

            )
        {
            _logger = logger;
            _context = ctx;
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> Get()
        {
            var countries = await _context.Country.ToListAsync();
            return Ok(countries);
        }


        [HttpGet("Country/{Name}")]
        public async Task<IActionResult> GetByCountry(string Name)
        {
            Country c = null;
            using (_context)
            {
                var data =_context.Country
               .Where(c => c.Name == Name)
               .Include(s => s.cities)
               .First(c => c.Name == c.Name);

                return Ok(data);
            }

        }


        [HttpGet("Cities/{Name}")]
        public async Task<IActionResult> GetByCity([FromServices] IOptions<AppSettings> setting, string Name)
        {
            City c;
            using (_context)
            {
                try
                {
                    c = await _context.City.Where(c => c.Name == Name).FirstAsync();
                    return Ok(c);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name,[FromBody] SaveCityResource2 value)
        {
            City result = null;
            Country c = null;
            try
            {
                try
                {
                    result = _context.City.Add(value.ToCity2()).Entity;
                    _context.SaveChanges();
                    c = await _context.Country.Where(c => c.Id == result.CountryId).FirstAsync();
                    c.cities.Add(result);
                    c.NPositivi = c.NPositivi + result.NPositivi;
                    _context.SaveChanges();

                    return Ok(result);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put(string Name, [FromBody] SaveCityResource cityResource)
        {
            long n = 0;
            var c = await _context.City.Where(c => c.Name == Name).FirstAsync();
            
            City ctRsrc = cityResource.ToCity();
            c.NPositivi = ctRsrc.NPositivi;
            Country co = await _context.Country.Where(co => co.Id == c.CountryId).FirstAsync();
            

            var result = _context.City.Update(c).Entity;
            try
            {
                var res = await _context.SaveChangesAsync();
                co = await _context.Country.Where(co => co.Id == result.CountryId).FirstAsync();
                if (c.NPositivi < result.NPositivi)
                {
                    n = result.NPositivi - c.NPositivi;
                    co.NPositivi = co.NPositivi - n;
                    _context.SaveChanges();
                }
                else if (c.NPositivi >= result.NPositivi)
                {
                    n = result.NPositivi - c.NPositivi;
                    co.NPositivi = co.NPositivi + n;
                    _context.SaveChanges();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        [HttpDelete("{Name}")]
        public async Task Delete(string Name)
        {
            Country co;
            City c = await _context.City.Where(c => c.Name == Name).FirstAsync();
            _context.City.Remove(c);
            await _context.SaveChangesAsync();
        }

    }
}
