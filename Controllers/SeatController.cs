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
    public class SeatController : ControllerBase
    {
        private readonly cinetecContext _context;
        public SeatController(cinetecContext context)
        {
            _context = context;
        }

        // Get a seats by cinema
        [HttpGet("{number}")]
        public async Task<IEnumerable<Seat>> GetSeats(int number)
        {
            //return await _context.Seats.Where(p => p.ScreeningId == number).ToListAsync();;
            return await _context.Seats.FromSqlInterpolated(@$"SELECT * FROM SEAT
                                                            WHERE Screening_id = {number}
                                                            ORDER BY row_num, column_num").ToListAsync();
        }

        // Get an specific seat
        [HttpGet("{number}/{row}/{column}")]
        public async Task<Seat> GetSpecificSeat(int number, int row, int column)
        {
            return await _context.Seats.FromSqlInterpolated(@$"SELECT * 
                                                            FROM SEAT
                                                            WHERE screening_id = {number} AND row_num = {row} AND column_num = {column}").FirstOrDefaultAsync();
        }

        // Post a seat
        [HttpPost]
        public async Task<ActionResult> Add(Seat seat)
        {
            var itemToAdd = await _context.Seats.FirstOrDefaultAsync(p => p.ScreeningId == seat.ScreeningId && p.RowNum == seat.RowNum && p.ColumnNum == seat.ColumnNum);
            var itemToAddCinema = await _context.Cinemas.FindAsync(seat.ScreeningId);
            if (itemToAdd != null || itemToAddCinema == null)
                return Conflict();
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Update a seat
        [HttpPut("{number}/{row_num}/{column_num}")]
        public async Task<ActionResult> UpdateCinema(int number, int row_num, int column_num, Seat seat)
        {
            var itemToUpdate = await _context.Seats.FirstOrDefaultAsync(p => p.ScreeningId == number && p.RowNum == row_num && p.ColumnNum == column_num);
            if (itemToUpdate == null)
                return NotFound();

            var itemToAddCinema = await _context.Cinemas.FindAsync(seat.ScreeningId);
            if (itemToAddCinema == null)
                return Conflict();
            
            itemToUpdate.State = seat.State;

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}