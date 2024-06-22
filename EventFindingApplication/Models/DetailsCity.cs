using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventFindingApplication.Models
{
    public class DetailsCity
    {
        public CityDto SelectedCity { get; set; }
        public IEnumerable<EventDto> KeptEvents { get; set; }
    }
}