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
    public class MovieController : ControllerBase
    {
        private readonly cinetecContext _context;
        public MovieController(cinetecContext context)
        {
            _context = context;
        }

        // Get all movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            // Use raw SQL query to get all movieas
            return await _context.Movies.FromSqlRaw(SqlQueries.GetAllMovies).ToListAsync();

        }

        // Get a movie by name
        [HttpGet("{name}")]
        public async Task<ActionResult<Movie>> GetMovie(string name)
        {
            // Use SQL query to get an specific movie
            var movie = await _context.Movies.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM MOVIE
                                                                        WHERE Original_Name = {name}").FirstOrDefaultAsync();

            return movie;

        }

        // Get a movie by movie theater
        [HttpGet("filter_movie/{theater_name}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviePerTheater(string theater_name)
        {
            // Use a sql query to get an specific movie based on the movie theater name
            var movieList = await _context.Movies.FromSqlInterpolated($@"SELECT DISTINCT M.Original_name, M.gendre, M.Name, M.Director, M.Image_url, M.Lenght 
                                                                        FROM MOVIE AS M, SCREENING AS SC, CINEMA AS C, MOVIE_THEATER AS MT
                                                                        WHERE M.Original_name = SC.Movie_original_name AND SC.Cinema_number = C.Number
                                                                       AND C.Name_movie_theater = MT.Name AND MT.Name = {theater_name};").ToListAsync();

            return movieList;
        }

        // Post a movie
        [HttpPost]
        public async Task<ActionResult> Add(Movie movie)
        {
            // Look if the movie exists
            var itemToAdd = await _context.Movies.FindAsync(movie.OriginalName);
            if (itemToAdd != null)
                return Conflict();

            // add the movie
            _context.Movies.Add(movie);

            // save the changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Update a movie
        [HttpPut("{name}")]
        public async Task<ActionResult> UpdateMovie(string name, Movie movie)
        {
            // search if the movie exists
            var itemToUpdate = await _context.Movies.FindAsync(name);
            if (itemToUpdate == null)
                return NotFound();

            // update the movie values
            itemToUpdate.Gendre = movie.Gendre;
            itemToUpdate.Name = movie.Name;
            itemToUpdate.Director = movie.Director;
            itemToUpdate.Lenght = movie.Lenght;
            itemToUpdate.ImageUrl = movie.ImageUrl;

            

            // save the changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a movie
        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteMovie(string name)
        {
            // search if the movie exists before deleting it
            var itemToRemove = await _context.Movies.FindAsync(name);

            if (itemToRemove == null)
                return NotFound();

            // Delete the item from the database and save the changes
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM ACTORS
                                                                WHERE original_movie_name = {name}");

            await _context.SaveChangesAsync();
            // remove the movie from the datbase and update
            _context.Movies.Remove(itemToRemove);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}