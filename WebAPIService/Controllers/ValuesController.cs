﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;
using System.Diagnostics;

namespace WebAPIService.Controllers
{
    /// <summary>
    /// ValuesController
    /// Web API Controller for the Demo
    /// Services CRUD requests for the Demo project
    /// </summary>
    public class ValuesController : ApiController
    {
        // Memory Cache
        ObjectCache cache = MemoryCache.Default;

        // Set up a cache policy
        CacheItemPolicy policy;

        // List of people
        List<string> people;

        /// <summary>
        /// ValuesController
        /// Constructs a temporary collection for the
        /// demo. The collection is a list of string
        /// entities representing characters from
        /// Star Trek TNG
        /// </summary>
        public ValuesController()
        {
            if (!cache.Contains("People"))
            {
                // Simple List of People for CRUD Example
                //List<string> people = new List<string>();
                people = new List<string>();

                // Add some generic values
                people.Add("Patrict Stewart");
                people.Add("Brent Spiner");
                people.Add("Jonathon Frakes");
                people.Add("Marina Sirtus");
                people.Add("Gates McFadden");
                people.Add("Michael Dorn");
                people.Add("LeVar Burton");
                people.Add("Wil Wheaton");
                people.Add("Denise Crosby");
                people.Add("Majel Barrett");
                people.Add("Colm Meaney");
                people.Add("Whoopi Goldberg");
                people.Add("John Di Lancie");
                people.Add("Diana Muldaur");
                people.Add(DateTime.Now.ToLongTimeString());

                policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1) };

                // Add a new Cache!
                cache.Add("People", people, policy);

            } // end of if           
        } // end of constructor

        /// <summary>
        /// Get (Collection)
        /// Gets the entire collection of the entities
        /// GET api/values
        /// </summary>
        /// <returns>a collection (IEnumerable) of entities </returns>
        public IEnumerable<string> Get()
        {
            // Get the List of Entities from the Cache
            return (List<string>)cache.Get("People");
        }

        /// <summary>
        /// Get (Single)
        /// Gets the entity by the identifier
        /// GET api/values/5
        /// IF index out of range => Returns a HttpStatusCode.BadRequest
        /// </summary>
        /// <param name="id">identifier (int) of the entity</param>
        /// <returns>the entity value (string) at the identifer</returns>
        public string Get(int id)
        {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0)
            {
                // Make a bad response and throw it
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(message);
            }
            else
            {
                return people[id];
            }         
        } // end of method

        /// <summary>
        /// Post
        /// Adds a new entity to the collection
        /// POST api/values
        /// </summary>
        /// <param name="value">the value (string) of the entity</param>
        public void Post([FromBody]string value)
        {
            // Get the List of Entities from the Cache
            //List<string> people = (List<string>)cache.Get("People");
            people = (List<string>)cache.Get("People");

            // Add the entity
            people.Add(value);

            // Update the Cache
            //cache["People"] = people;

            // Update the Cache
            //cache.Set("People", people, policy);
        }

        /// <summary>
        /// Put
        /// Replaces the entity with an identifier
        /// with a new value. 
        /// PUT api/values/5
        /// IF index out of range => Returns a HttpStatusCode.BadRequest
        /// </summary>
        /// <param name="id">identifier (int) of the entity to replace</param>
        /// <param name="value">value (string) to replace the existing entity value</param>
        public void Put(int id, [FromBody]string value)
        {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0)
            {
                // Make a bad response and throw it
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(message);
            }

            //Update the Entity
            people[id] = value;

            // Update the Cache
            //cache["People"] = people;

            // Update the Cache
            //cache.Set("People", people, policy);
        }

        /// <summary>
        /// Delete
        /// Deletes an entity based on the id
        /// DELETE api/values/5
        /// IF index out of range => Returns a HttpStatusCode.BadRequest
        /// </summary>
        /// <param name="id">identifier (id) of the entity</param>
        public void Delete(int id)
        {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0)
            {
                // Make a bad response and throw it
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(message);
            }

            // Delete the Entity
            people.RemoveAt(id);

            // Update the Cache
            //cache["People"] = people;

            // Update the Cache
            //cache.Set("People", people, policy);
        }

    } // end of class
} // end of namespace
