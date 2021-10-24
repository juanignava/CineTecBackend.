using System;
using System.Collections.Generic;


namespace CineTecBackend
{
    static class SqlQueries
    {
        public const string GetAllClients = "SELECT * FROM CLIENT";
        public const string GetAllMovieTheaters = "SELECT * FROM MOVIE_THEATER";
        public const string GetAllCinemas = "SELECT * FROM CINEMA";
        public const string GetAllScreenings = "SELECT * FROM SCREENING";
        public const string GetAllMovies = "SELECT * FROM MOVIE";
        public const string GetAllActors = "SELECT * FROM ACTORS";
        public const string GetAllPurchases = "SELECT * FROM PURCHASE";
        public const string GetAllEmployees = "SELECT * FROM EMPLOYEE";

    }
}
