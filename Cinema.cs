using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Cinema
    {
        public Cinema()
        {
            Screenings = new HashSet<Screening>();
        }

        public int Number { get; set; }
        public int? Rows { get; set; }
        public int? Columns { get; set; }
        public int? Capacity { get; set; }
        public string NameMovieTheater { get; set; }

        public virtual MovieTheater NameMovieTheaterNavigation { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; }
    }
}
