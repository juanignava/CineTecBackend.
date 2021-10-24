using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Seat
    {
        public int ScreeningId { get; set; }
        public int RowNum { get; set; }
        public int ColumnNum { get; set; }
        public int? PurchaseId { get; set; }
        public string State { get; set; }

        public virtual Purchase Purchase { get; set; }
        public virtual Screening Screening { get; set; }
    }
}
