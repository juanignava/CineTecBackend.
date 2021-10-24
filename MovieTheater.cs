using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class MovieTheater
    {
        public MovieTheater()
        {
            Cinemas = new HashSet<Cinema>();
            Purchases = new HashSet<Purchase>();
        }

        public string Name { get; set; }
        public string Location { get; set; }
        public int? CinemaAmount { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
