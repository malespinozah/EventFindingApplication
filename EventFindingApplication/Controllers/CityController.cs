using EventFindingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace EventFindingApplication.Controllers
{
    public class CityController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CityController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44340/api/CityData/");
        }

        /// <summary>
        /// Retrieves a list of cities from the API and displays them.
        /// </summary>
        /// <returns>Returns the List view with a collection of CityDto objects.</returns>
        // GET: City/List
        public ActionResult List()
        {
            //objective: communicate with our City data api to retrieve a list of events
            //curl https://localhost:44340/api/CityData/ListCities


            string url = "https://localhost:44340/api/CityData/ListCities";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CityDto> Cities = response.Content.ReadAsAsync<IEnumerable<CityDto>>().Result;
            //Debug.WriteLine("Number of events received : ");
            //Debug.WriteLine(Events.Count());

            return View(Cities);
        }

        /// <summary>
        /// Retrieves the details of a specific city and its related events from the API.
        /// </summary>
        /// <param name="id">The ID of the city to retrieve.</param>
        /// <returns>Returns the Details view with the selected city details and related events.</returns>
        // GET: City/Details/{id}
        public ActionResult Details(int id)
        {
            //objective: communicate with our City data api to retrieve one Event
            //curl https://localhost:44340/api/CityData/FindCity/{id}

            DetailsCity ViewModel = new DetailsCity();

            string url = "https://localhost:44340/api/CityData/FindCity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CityDto SelectedCity = response.Content.ReadAsAsync<CityDto>().Result;

            ViewModel.SelectedCity = SelectedCity;

            url = "https://localhost:44340/api/EventData/ListEventsForCity/" + id ;
            response = client.GetAsync(url).Result;
            IEnumerable<EventDto> RelatedEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;

            ViewModel.KeptEvents = RelatedEvents;

            return View(ViewModel);
        }


    }
}