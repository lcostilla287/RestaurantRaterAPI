using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private RestaurantDbContext _context = new RestaurantDbContext();

        //Create(POST)
        [HttpPost] // this isn't absolutely necessary but can come in handy
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body can't be empty"); // this is an IHttpActionResult
            }

            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok(); // this is our action result
            }
            return BadRequest(ModelState);
        }

        //Read(GetAll)
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }



        //GetById
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        //Update(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant(int id, Restaurant updatedRestaurant)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Address = updatedRestaurant.Address;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            //number of things that have been saved
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was successfully deleted.");
            }

            return InternalServerError();
        }
        //Getallrecommendedrestaurants
        [HttpGet]
        //this affects the url
        [Route("api/Restaurant/IsRecommended")]
        public async Task<IHttpActionResult> GetRestaurantByIsRecommended()
        {
            // List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            //List<Restaurant> recommendedRestaurants = new List<Restaurant>();

            //foreach (Restaurant restaurant in restaurants)
            //{
            // if (restaurant.IsRecommended)
            //{
            //take from the old and put in recommended
            //no need to save the changes
            // recommendedRestaurants.Add(restaurant);
            // }
            //  }
            //if (restaurants.Count < 1)
            //{
            // return NotFound();
            // }
            //return Ok(recommendedRestaurants);

            //can do it for names and other things too. Have to do this because it is readonly. other properties are fine so the middle ToList can be removed
            List<Restaurant> restaurants = _context.Restaurants.ToList().Where(r => r.IsRecommended).ToList();
            return Ok(restaurants);
        }
    }
}
