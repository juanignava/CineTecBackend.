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
    public class CinemaController : ControllerBase
    {
        private readonly cinetecContext _context;
        public CinemaController(cinetecContext context)
        {
            _context = context;
        }

        // Get all cinemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas()
        {
            // Use raw SQL query to get all cinemas
            return await _context.Cinemas.FromSqlRaw(SqlQueries.GetAllCinemas).ToListAsync();

        }

        // Get a cinema by id
        [HttpGet("{number}")]
        public async Task<ActionResult<Cinema>> GetCinema(int number)
        {
            // Use SQL query to get an specific cinema
            var cinema = await _context.Cinemas.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CINEMA
                                                                        WHERE Number = {number}").FirstOrDefaultAsync();
            return cinema;

        }

        // Post a cinema
        [HttpPost]
        public async Task<ActionResult> Add(Cinema cinema)
        {
            // Search if the related movie theater exists
            var itemToAddMovieTheater = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM MOVIE_THEATER
                                                                        WHERE Name = {cinema.NameMovieTheater}").FirstOrDefaultAsync();

            if (itemToAddMovieTheater == null)
                return Conflict();

            // assign a cinema number based on the existing numbers
            var cinemas = await _context.Cinemas.FromSqlRaw(SqlQueries.GetAllCinemas).ToListAsync();

            int max = 0;
            foreach (var cin in cinemas)
            {
                if (cin.Number > max)
                {
                    max = cin.Number;
                }
            }

            cinema.Number = max+1;

            // update the movie theater cinema amount
            itemToAddMovieTheater.CinemaAmount = itemToAddMovieTheater.CinemaAmount+1;

            _context.Cinemas.Add(cinema);

            // save changes in the database
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Update a cinema
        [HttpPut("{number}")]
        public async Task<ActionResult> UpdateCinema(int number, Cinema cinema)
        {
            // first search if the cinema exists
            var itemToUpdate = await _context.Cinemas.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CINEMA
                                                                        WHERE Number = {number}").FirstOrDefaultAsync();

            if (itemToUpdate == null)
                return NotFound();

            // get the related movie theater
            var itemToUpdateMovieTheater = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM MOVIE_THEATER
                                                                        WHERE Name = {cinema.NameMovieTheater}").FirstOrDefaultAsync();

            if (itemToUpdateMovieTheater == null)
                return Conflict();
            
            // update the cinema data
            itemToUpdate.Rows = cinema.Rows;
            itemToUpdate.Columns = cinema.Columns;

            // save changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a cinema
        [HttpDelete("{number}")]
        public async Task<ActionResult> DeleteCinema(int number)
        {
            // first search if the item exists
            var itemToRemove = await _context.Cinemas.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CINEMA
                                                                        WHERE Number = {number}").FirstOrDefaultAsync();

            if (itemToRemove == null)
                return NotFound();

            // delete the cinema 
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM CINEMA 
                                                                WHERE Number = {number}");

            // decrease amount of cinemas related to the movie teather
            var itemToDeleteMovieTheater = await _context.MovieTheaters.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM MOVIE_THEATER
                                                                        WHERE Name = {itemToRemove.NameMovieTheater}").FirstOrDefaultAsync();

            itemToDeleteMovieTheater.CinemaAmount = itemToDeleteMovieTheater.CinemaAmount - 1;
            
            // Save changes in the database
            await _context.SaveChangesAsync();
            return Ok();
            
            
        }
    }
}