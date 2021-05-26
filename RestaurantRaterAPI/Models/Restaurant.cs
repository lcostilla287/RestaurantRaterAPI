using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    //this is an entity similar to POCO
    //also there is no need for empty constructor, it is implied
    public class Restaurant
    {
        //Id is required and unique!!!
        //make sure to have the using statement from above
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        //this collection will populate automatically that have referenced this Restaurant Entity
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating 
        { 
            get 
            {
                double totalAverageRating = 0;
                foreach(Rating rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return totalAverageRating / Ratings.Count;
            } 
        }

        //average food score
        public double AverageFoodScore 
        { 
            get 
            {
                double totalAverageFoodScore = 0;
                foreach(Rating rating in Ratings)
                {
                    totalAverageFoodScore += rating.FoodScore;
                }
                return totalAverageFoodScore / Ratings.Count;
                
            } 
        }

        //average environment score
        public double AverageEnvironmentScore 
        { 
            get 
            {
                double totalAverageEnvironmentScore = 0;
                foreach (Rating rating in Ratings)
                {
                    totalAverageEnvironmentScore += rating.EnvironmentScore;
                }
                return totalAverageEnvironmentScore / Ratings.Count;
            } 
        }
        //average cleanliness score
        public double AverageCleanlinessScore 
        { 
            get 
            {
                double totalAverageCleanlinessScore = 0;
                foreach (Rating rating in Ratings)
                {
                    totalAverageCleanlinessScore += rating.EnvironmentScore;
                }
                return totalAverageCleanlinessScore / Ratings.Count;
            } 
        }

        public bool IsRecommended => Rating > 8.5; // this is shorthand
    }
}