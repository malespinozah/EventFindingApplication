using EventFindingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EventFindingApplication.Controllers
{
    public class CityDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all cities from the database.
        /// </summary>
        /// <returns>Returns an IHttpActionResult containing a list of CityDto objects representing the cities.</returns>
        [HttpGet]
        [ResponseType(typeof(EventDto))]
        public IHttpActionResult ListCity()
        {
            List<City> City = db.Cities.ToList();
            List<CityDto> CityDtos = new List<CityDto>();

            City.ForEach(C => CityDtos.Add(new CityDto()
            {
                CityId = C.CityId,
                CityName = C.CityName,
            }));

            return Ok(CityDtos);
        }

        [HttpGet]
        [Route("api/CityData/ListCities")]
        public List<CityDto> ListCities()
        {
            List<City> Cities = db.Cities.ToList();
            List<CityDto> CityDtos = new List<CityDto>();

            foreach (City City in Cities)
            {
                CityDto CityDto = new CityDto();

                CityDto.CityId = City.CityId;
                CityDto.CityName = City.CityName;

                CityDtos.Add(CityDto);

            }
            return CityDtos;
        }

        /// <summary>
        /// Retrieves the details of a specific city by ID.
        /// </summary>
        /// <param name="id">The ID of the city to retrieve.</param>
        /// <returns>Returns an IHttpActionResult containing the CityDto object with the city's details.</returns>
        [HttpGet]
        [Route("api/CityData/FindCity/{id}")]
        public IHttpActionResult FindCity(int id)
        {
            City City = db.Cities.Find(id);
            CityDto CityDto = new CityDto();
            CityDto.CityId = City.CityId;
            CityDto.CityName = City.CityName;

            return Ok(CityDto);
        }

    }
}
