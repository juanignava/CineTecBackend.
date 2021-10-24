using System;
using System.Collections.Generic;

#nullable disable

namespace CineTecBackend
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecLastName { get; set; }
        public int? Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NameMovieTheater { get; set; }
        public string Role { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual MovieTheater NameMovieTheaterNavigation { get; set; }
    }
}
