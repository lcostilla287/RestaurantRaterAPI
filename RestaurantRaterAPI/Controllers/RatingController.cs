using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if(restaurant == null)
            {
                return BadRequest($"The target restaurant with the Id of {model.RestaurantId} does not exist");
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully");
            }

            return InternalServerError();
        }

        //GetAllRatings?
        //GetRatingById?
        
        //Get rating by restaurant id


        //Update Rating PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating(int id, Rating updatedRating)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(updatedRating.RestaurantId);
            if (restaurant != null)
            {
                if (ModelState.IsValid)
                {
                    Rating rating = await _context.Ratings.FindAsync(id);

                    if (rating != null)
                    {
                        rating.FoodScore = updatedRating.FoodScore;
                        rating.EnvironmentScore = updatedRating.EnvironmentScore;
                        rating.CleanlinessScore = updatedRating.CleanlinessScore;

                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    return NotFound();
                }
            }
            return BadRequest();
        }


        //Delete Rating
        

    }
}
