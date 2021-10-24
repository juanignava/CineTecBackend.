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
    public class ActorController : ControllerBase
    {
        private readonly cinetecContext _context;
        public ActorController(cinetecContext context)
        {
            _context = context;
        }

        // Get all actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            // Use raw SQL query to get all clients
            return await _context.Actors.FromSqlRaw(SqlQueries.GetAllActors).ToListAsync();


        }

        // Post a actor
        [HttpPost]
        public async Task<ActionResult> Add(Actor actor)
        {
            var itemToAdd = await _context.Actors.FirstOrDefaultAsync(p => p.OriginalMovieName == actor.OriginalMovieName && p.ActorName == actor.ActorName);

            var itemToAddMovie = await _context.Movies.FindAsync(actor.OriginalMovieName);
            if (itemToAdd != null || itemToAddMovie == null)
                return Conflict();

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // Delete a actor
        [HttpDelete("{movie_name}/{name}")]
        public async Task<ActionResult> DeleteActor(string movie_name, string name)
        {
            var itemToRemove = await _context.Actors.FirstOrDefaultAsync(p => p.OriginalMovieName == movie_name && p.ActorName == name);

            if (itemToRemove == null)
                return NotFound();

            _context.Actors.Remove(itemToRemove);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}