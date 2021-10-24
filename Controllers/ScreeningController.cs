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
    public class ScreeningController : ControllerBase
    {
        private readonly cinetecContext _context;
        public ScreeningController(cinetecContext context)
        {
            _context = context;
        }

        // Get all screenings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Screening>>> GetScreenings()
        {
            // Use raw SQL query to get all screenings
            return await _context.Screenings.FromSqlRaw(SqlQueries.GetAllScreenings).ToListAsync();

        }

        // Get a screening by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Screening>> GetScreening(int id)
        {
            // Use SQL query to get an specific client
            var screening = await _context.Screenings.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM SCREENING
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            return screening;
            
        }

        
        // Get screening per theater per movie
        [HttpGet("filter_screening/{theater_name}/{movie_name}")]
        public async Task<ActionResult<IEnumerable<Screening>>> GetScreeningPerTheater(string theater_name, string movie_name)
        {

            var screeningList = await _context.Screenings.FromSqlInterpolated($@"SELECT SC.ID, SC.Cinema_number, SC.Movie_original_name, SC.Hour, SC.Capacity 
                                                                            FROM SCREENING AS SC, CINEMA AS C, MOVIE_THEATER AS MT 
                                                                            WHERE SC.Cinema_number = C.Number AND C.Name_movie_theater = MT.Name 
                                                                            AND MT.Name = {theater_name} AND SC.Movie_original_name = {movie_name}").ToListAsync();
            
            return screeningList;
        }
        

        // Post a screening
        [HttpPost]
        public async Task<ActionResult> Add(Screening screening)
        {
            // Look if the related cinema and movie exist
            var screening_cinema = await _context.Cinemas.FindAsync(screening.CinemaNumber);
            var screening_movie = await _context.Movies.FindAsync(screening.MovieOriginalName);

            if(screening_cinema == null || screening_movie == null) return Conflict();

            // define the screening id based on the id of the rest of the instances
            var screenings = await _context.Screenings.ToListAsync();
            int max = 0;
            foreach (var screen in screenings)
            {
                if (screen.Id > max)
                {
                    max = screen.Id;
                }
            }

            screening.Id = max+1;
            _context.Screenings.Add(screening);

            // add the screening and save the changes
            await _context.SaveChangesAsync();

            // create the instances of the seats of a screening
            var itemAddedCinema = await _context.Cinemas.FindAsync(screening.CinemaNumber);

            int restrictedNum = 1;
            bool isRestricted = true;
            if (screening.Capacity == 50) restrictedNum = 2;
            else if(screening.Capacity == 30) restrictedNum = 3;
            else if(screening.Capacity == 20) restrictedNum = 4;
            else isRestricted = false;

            for (int i = 1; i <= itemAddedCinema.Rows; i++)
            {
                for (int j = 1; j <= itemAddedCinema.Columns; j++)   {
                    
                    string state = "free";
                    if (isRestricted) {
                        if ((i+j)%restrictedNum == 0)
                        {
                            state = "restricted";
                        }
                    }

                    Seat seat = new ()
                    {
                        ScreeningId = screening.Id,
                        RowNum = i,
                        ColumnNum = j,
                        State = state
                    };

                    // add the new seat and save the changes in the database
                    _context.Seats.Add(seat);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();
        }

        // Update a screening
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScreening(int id, Screening screening)
        {
            // Check if the item exists
            var itemToUpdate = await _context.Screenings.FindAsync(id);
            if (itemToUpdate == null)
                return NotFound();

            // update the item data
            itemToUpdate.Hour = screening.Hour;
            itemToUpdate.Capacity = screening.Capacity;

            // save the changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a screening
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScreening(int id)
        {
            var seats = _context.Seats.Where(seat => seat.ScreeningId == id);
            
            foreach (var seat in seats)
            {
                _context.Seats.Remove(seat);
            }
            await _context.SaveChangesAsync();

            var itemToRemove = await _context.Screenings.FindAsync(id);

            if (itemToRemove == null)
                return NotFound();

            // Delete the screening using an SQL query
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM SCREENING 
                                                                WHERE Id = {id}");
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}