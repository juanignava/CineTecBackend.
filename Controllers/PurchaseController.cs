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
    public class PurchaseController : ControllerBase
    {
        private readonly cinetecContext _context;
        public PurchaseController(cinetecContext context)
        {
            _context = context;
        }

        // Post a Purchase
        [HttpPost]
        public async Task<int> AddPurchase(Purchase purchase)
        {

            // assign a cinema number based on the existing numbers
            var purchases = await _context.Purchases.FromSqlRaw(SqlQueries.GetAllPurchases).ToListAsync();

            int max = 0;
            foreach (var pur in purchases)
            {
                if (pur.Purchaseid > max)
                {
                    max = pur.Purchaseid;
                }
            }

            purchase.Purchaseid = max+1;



            foreach (var seat in purchase.Seats)
            {
                Seat seatToUpdate = await _context.Seats.FirstOrDefaultAsync(p => p.ScreeningId == purchase.Screeningid && p.RowNum == seat.RowNum && p.ColumnNum == seat.ColumnNum);
                seatToUpdate.State = "sold";
                await _context.SaveChangesAsync();
            }

            purchase.Seats = null;
            _context.Purchases.Add(purchase);
            // save changes in the database
            await _context.SaveChangesAsync();

            return purchase.Purchaseid;
        }
    }
}