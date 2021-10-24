using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CineTecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieTheaterController : ControllerBase
    {
        private readonly cinetecContext _context;
        public MovieTheaterController(cinetecContext context)
        {
            _context = context;
        }

        // Get all movie theaters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieTheater>>> GetMovieTheaters()
        {
            // Use raw SQL query to get all movie theaters
            return await _context.MovieTheaters.FromSqlRaw(SqlQueries.GetAllMovieTheaters).ToListAsync();
        }

        // Get a movie theater by id
        [HttpGet("{name}")]
        public async Task<ActionResult<MovieTheater>> GetMovieTheater(string name)
        {
            // Use SQL query to get an specific movie theater
            var movieTheater = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                                FROM MOVIE_THEATER
                                                                                WHERE Name = {name}").FirstOrDefaultAsync();

            return movieTheater;
        }

        // Post a movie theater
        [HttpPost]
        public async Task<ActionResult> Add(MovieTheater movieTheater)
        {            
            // First look if the movie theater already exists
            var itemToAdd = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                                FROM MOVIE_THEATER
                                                                                WHERE Name = {movieTheater.Name}").FirstOrDefaultAsync();

            if (itemToAdd != null)
                return Conflict();
            
            // Add the movie theater and save the changes in the database
            movieTheater.CinemaAmount = 0;
            _context.MovieTheaters.Add(movieTheater);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Update a movie theater
        [HttpPut("{name}")]
        public async Task<ActionResult> UpdateMovieTheater(string name, MovieTheater movieTheater)
        {
            // first look if the movie theater already exists
            var itemToUpdate = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                                FROM MOVIE_THEATER
                                                                                WHERE Name = {name}").FirstOrDefaultAsync();

            if (itemToUpdate == null)
                return NotFound();
            
            // update the values of the movie theater and save changes
            itemToUpdate.Location = movieTheater.Location;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a movie theater
        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteMovieTheater(string name)
        {

            // first look if the item already exists
            var itemToRemove = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                                FROM MOVIE_THEATER
                                                                                WHERE Name = {name}").FirstOrDefaultAsync();

            if (itemToRemove == null)
                return NotFound();

            // Delete the item from the database and save the changes
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM MOVIE_THEATER 
                                                                WHERE Name = {name}");

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}