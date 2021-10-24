using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Actor
    {
        public string OriginalMovieName { get; set; }
        public string ActorName { get; set; }

        public virtual Movie OriginalMovieNameNavigation { get; set; }
    }
}
