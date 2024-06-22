using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventFindingApplication.Models
{
    public class UpdateEvent
    {
        //This viewmodel is a class which stores information that we need to present to /Animal/Update/{}

        //the existing animal information

        public EventDto SelectedEvent { get; set; }

        // all species to choose from when updating this animal
        public IEnumerable<CityDto> CityOption { get; set; }
    }
}