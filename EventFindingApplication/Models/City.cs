using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace EventFindingApplication.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

        // A city can have lots of events
        public ICollection<Event> Events { get; set; }
    }

    // A Data Transfer Object (DTO)
    // Communicating the clothing item information externally 
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

}