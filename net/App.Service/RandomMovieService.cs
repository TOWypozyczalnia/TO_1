using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service
{
    public class RandomMovieService
    {
        
        public Movie DrawMovie(List<Movie>movies)
        {
            Random random = new Random();
            if (movies.Count == 0) return null;
            return movies[random.Next(movies.Count())];
        }
    }
}
