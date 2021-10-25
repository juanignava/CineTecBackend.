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
    public class ClientController : ControllerBase
    {

        private readonly cinetecContext _context;
        public ClientController(cinetecContext context)
        {
            _context = context;
        }

        // Get all clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            // Use raw SQL query to get all clients
            return await _context.Clients.FromSqlRaw(SqlQueries.GetAllClients).ToListAsync();

        }

        // Get a client by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            // Use SQL query to get an specific client
            var client = await _context.Clients.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CLIENT
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            return client;
        }

        // Post a client
        [HttpPost]
        public async Task<ActionResult> Add(Client client)
        {
            // First look if the client already exists
            var itemToAdd = await _context.Clients.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CLIENT
                                                                        WHERE Id = {client.Id}").FirstOrDefaultAsync();
            if (itemToAdd != null)
                return Conflict();

            // Then add the client to the database
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Update a client
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, Client client)
        {
            // Look if the client already exists
            var itemToUpdate = await _context.Clients.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CLIENT
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            if (itemToUpdate == null)
                return NotFound();

            // Define it's new values
            itemToUpdate.FirstName = client.FirstName;
            itemToUpdate.LastName = client.LastName;
            itemToUpdate.SecLastName = client.SecLastName;
            itemToUpdate.Age = client.Age;
            itemToUpdate.PhoneNumber = client.PhoneNumber;
            itemToUpdate.Password = client.Password;
            itemToUpdate.BirthDate = client.BirthDate;
            // save the changes in the databse
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a client
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            // First look fot the client with an sql query
             var itemToRemove = await _context.Clients.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM CLIENT
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            if (itemToRemove == null)
                return NotFound();

            // Delete the client using an SQL query
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM CLIENT 
                                                                WHERE Id = {id}");

            // Save changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}