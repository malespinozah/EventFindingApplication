using EventFindingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Description;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace EventFindingApplication.Controllers
{
    public class EventDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all events from the database.
        /// </summary>
        /// <returns>Returns an IEnumerable of EventDto objects representing the events.</returns>
        [HttpGet]
        [Route("api/EventData/ListEvents")]
        public IEnumerable<EventDto> ListEvents()
        {
            List<Event> Events = db.Events.ToList();
            List<EventDto> EventDtos = new List<EventDto>();

            foreach (Event Event in Events)
            {
                EventDto EventDto = new EventDto();

                EventDto.EventId = Event.EventId;
                EventDto.EventName = Event.EventName;
                EventDto.EventDescription = Event.EventDescription;
                EventDto.EventDate = Event.EventDate;
                EventDto.EventTime = Event.EventTime;
                EventDto.EventPrice = Event.EventPrice;
                EventDto.EventAddress = Event.EventAddress;

                EventDto.CityName = Event.City.CityName;

                EventDtos.Add(EventDto);

            }
            return EventDtos;
        }

        /// <summary>
        /// Retrieves a list of events for a specific city from the database.
        /// </summary>
        /// <param name="id">The ID of the city.</param>
        /// <returns>Returns an IEnumerable of EventDto objects representing the events for the specified city.</returns>
        [HttpGet]
        [Route("api/EventData/ListEventsForCity/{id}")]
        [ResponseType(typeof(EventDto))]
        public IEnumerable<EventDto> ListEventsForCity(int id)
        {
            List<Event> Events = db.Events.Where(e => e.CityId == id).ToList();
            List<EventDto> EventDtos = new List<EventDto>();

            Events.ForEach(e => EventDtos.Add(new EventDto()
            {
                EventId = e.EventId,
                EventName = e.EventName,
                CityId = e.City.CityId,
            }));

            return EventDtos;
        }

        /// <summary>
        /// Retrieves the details of a specific Event by ID.
        /// </summary>
        /// <param name="id">The ID of the Event to retrieve.</param>
        /// <returns>Returns an IHttpActionResult containing the EventDto object.</returns>
        [HttpGet]
        [Route("api/EventData/FindEvents/{id}")]
        public IHttpActionResult FindEvents(int id)
        {
            Event Event = db.Events.Find(id);
            EventDto EventDto = new EventDto();
                EventDto.EventId = Event.EventId;
                EventDto.EventName = Event.EventName;
                EventDto.EventDescription = Event.EventDescription;
                EventDto.EventDate = Event.EventDate;
                EventDto.EventTime = Event.EventTime;
                EventDto.EventPrice = Event.EventPrice;
                EventDto.EventAddress = Event.EventAddress;
                EventDto.CityName = Event.City.CityName;
            return Ok(EventDto);
        }

        /// <summary>
        /// Adds a new Event to the database.
        /// </summary>
        /// <param name="Event">The Event object to add.</param>
        /// <returns>Returns an IHttpActionResult containing the created Event object.</returns>
        [HttpPost]
        [Route("api/EventData/AddEvent")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult AddEvent(Event Event)
        {
            db.Events.Add(Event);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = Event.EventId }, Event);
        }

        /// <summary>
        /// Deletes an Event from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the Event to delete.</param>
        /// <returns>Returns an IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [Route("api/EventData/DeleteEvent/{id}")]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event Event = db.Events.Find(id);
            db.Events.Remove(Event);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates an existing Event in the database by ID.
        /// </summary>
        /// <param name="id">The ID of the Event to update.</param>
        /// <param name="Event">The updated Event object.</param>
        /// <returns>Returns an IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [Route("api/EventData/UpdateEvents/{id}")]
        public IHttpActionResult UpdateEvents(int id, Event Event)
        {
            if (!ModelState.IsValid)
            {
              return  BadRequest(ModelState);
            }

            if (id!= Event.EventId)
            {

              return  BadRequest();
            }

            db.Entry(Event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                   return NotFound();
                }
                else
                {
                    throw;
                }
            }
           return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Checks if an Event exists in the database by ID.
        /// </summary>
        /// <param name="id">The ID of the Event to check.</param>
        /// <returns>Returns true if the Event exists, false otherwise.</returns>
        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.EventId == id) > 0;
        }
    }
}
