using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Purchase
    {
        public Purchase()
        {
            Seats = new HashSet<Seat>();
        }

        public int Purchaseid { get; set; }
        public int? Clientid { get; set; }
        public string Theatername { get; set; }
        public string Movieoriginalname { get; set; }
        public int? Screeningid { get; set; }
        public DateTime? Date { get; set; }

        public virtual Client Client { get; set; }
        public virtual Movie MovieoriginalnameNavigation { get; set; }
        public virtual Screening Screening { get; set; }
        public virtual MovieTheater TheaternameNavigation { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
