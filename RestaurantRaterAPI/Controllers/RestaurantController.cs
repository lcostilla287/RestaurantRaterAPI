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

        //Delete
    }
}
