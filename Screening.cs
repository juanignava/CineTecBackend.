using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Screening
    {
        public Screening()
        {
            Purchases = new HashSet<Purchase>();
            Seats = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public int? CinemaNumber { get; set; }
        public string MovieOriginalName { get; set; }
        public int? Hour { get; set; }
        public int? Capacity { get; set; }

        public virtual Cinema CinemaNumberNavigation { get; set; }
        public virtual Movie MovieOriginalNameNavigation { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
