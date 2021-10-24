# CineTecBackend
This repo corresponds to the backend of the backend of the first project of the course of Databases

## Backend JSON Body

#### Clients

```Json
[
  {
    "id": 1,
    "firstName": "Juan",
    "lastName": "Navarro",
    "secLastName": "Navarro",
    "age": 20,
    "birthDate": "2001-02-08T00:00:00",
    "phoneNumber": "87175508",
    "password": "password"
  }
]
```

#### Movie Theater

```Json
[
  {
    "name": "Paseo",
    "location": "Cartago",
    "cinemaAmount": 5,
  },
  {
    "name": "Multiplaza",
    "location": "San Jose",
    "cinemaAmount": 6
  }
]
```

#### Cinemas

```Json
[
  {
    "number": 1,
    "rows": 10,
    "columns": 10,
    "nameMovieTheater": "Paseo"
  },
  {
    "number": 2,
    "rows": 10,
    "columns": 5,
    "nameMovieTheater": "Paseo"
  }
]
```

#### Movies

```Json
[
  {
    "originalName": "Titanic",
    "gendre": "Suspense",
    "name": null,
    "director": "Cameron",
    "imageUrl": "https://github.com/juanignava/CineTecBackend/blob/main/MovieImages/Titanic.jpg",
    "lenght": 180
  },
  {
    "originalName": "Minions",
    "gendre": "Comedy",
    "name": null,
    "director": "Polack",
    "imageUrl": "https://github.com/juanignava/CineTecBackend/blob/main/MovieImages/Minions.jpg",
    "lenght": 120
  }
]
```

#### Screening

```Json
[
  {
    "id": 1,
    "cinemaNumber": 1,
    "movieOriginalName": "Titanic",
    "hour": 10,
    "capacity": 50
  }
]
```

#### Seat

```Json
[
  {
    "screeningId": 1,
    "rowNum": 1,
    "columnNum": 1,
    "state": "free",
    "screening": null
  },
  {
    "screeningId": 1,
    "rowNum": 1,
    "columnNum": 2,
    "state": "free",
    "screening": null
  }
]
```

#### Actors

```Json
[
  {
    "originalMovieName": "Titanic",
    "actorName": "Leonardo"
  }
]
```


## Backend requests

Here we have a small description of the programmed requests and their respective response (if there is any).

### Client Requests

###### POST client

Add a new client, where their id is the key attribute. If theres al ready a client with the respective id there will be an 409 error (Conflict). This is a POST request with this url `http://localhost:5000/api/client` the body to include corresponds to the one of the clients.

###### GET clients

Get all the clients saved in the database. This is a GET request with this url `http://localhost:5000/api/client` the body aswered has the form of the client JSON body.

###### GET client by username

Get an specific client based on the username. If the user doesn't exists then a not found error will be answered. This is a GET request with this url `http://localhost:5000/api/client/#` where `#` represents the username of the request. the body aswered has the form of the client JSON body.


###### DELETE client

Deletes a client from the database. If the client doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/client/#` where `#` represents the username of the request. This request has no answer.

###### PUT client

Updates the information from a client in the databse. If the client doesn't exists then a not found error (404) will be answered. This is a PUT request with this url `http://localhost:5000/api/client/#` where `#` represents the username of the request. The body of this requests is the one specified for the clients.

### Movie Theater Requests

###### POST movie theater

Add a new movie theater, where the id is the key attribute. If theres already a movie theater with the respective id there will be an 409 error (Conflict). This is a POST request with this url `http://localhost:5000/api/movietheater` the body to include corresponds to the one of the movie theater.

###### GET movies theater

Get all the movie theater saved in the database. This is a GET request with this url `http://localhost:5000/api/movietheater` the body aswered has the form of the movie theater JSON body.

###### GET movie theater by name

Get an specific movie theater based on the name. If the movie theater doesn't exists then a not found error will be answered. This is a GET request with this url `http://localhost:5000/api/movietheater/#` where `#` represents the movie theater of the request. the body aswered has the form of the movie theater JSON body.


###### DELETE movie theater

Deletes a movie from the database. If the movie doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/movietheater/#` where `#` represents the name of the request. This request has no answer.

###### PUT movie theater

Updates the information from a movie theater in the databse. If the movie theater doesn't exists then a not found error (404) will be answered. This is a PUT request with this url `http://localhost:5000/api/movietheater/#` where `#` represents the name of the request. The body of this requests is the one specified for the movie theater.

### Cinema Requests

###### POST cinema

Add a new cinema, there is a foreign key with the name of the movie theater, if this foreign key doesn't exist a conflict will be returned. This is a POST request with this url `http://localhost:5000/api/cinema`.

There is no need to specify the cinema number in the post JSON, this number will be added by the backend in order to avoid conflicts. The post JSON has to be like this one:

```Json
{
  "rows": 10,
  "columns": 10,
  "nameMovieTheater": "Paseo"
}
```

###### GET cinemas

Get all the cinema saved in the database. This is a GET request with this url `http://localhost:5000/api/cinema` the body aswered has the form of the cinema JSON body.

###### GET cinema by number

Get an specific movie theater based on the name. If the cinema doesn't exists then a not found error will be answered. This is a GET request with this url `http://localhost:5000/api/cinema/#` where `#` represents the cinema number of the request. the body aswered has the form of the cinema JSON body.


###### DELETE cinema

Deletes a cinema from the database. If the cinema doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/cinema/#` where `#` represents the number of the request. This request has no answer.

###### PUT cinema

Updates the information from a cinema in the databse. If the cinema doesn't exists then a not found error (404) will be answered. Also there is a foreign key with the name of the movie theater, if this foreign key doesn't exist a conflict will be returned. This is a PUT request with this url `http://localhost:5000/api/cinema/#` where `#` represents the number of the cinema of the request. The body of this requests is the one specified for the cinema.

### Movies Requests

###### POST movie

Add a new movie, where the original name is the key attribute. If theres already a movie with the respective original name there will be an 409 error (Conflict). This is a POST request with this url `http://localhost:5000/api/movie` the body to include corresponds to the one of the movie.

###### GET movie

Get all the movie saved in the database. This is a GET request with this url `http://localhost:5000/api/movie` the body aswered has the form of the movie JSON body.

###### GET movie by original name

Get an specific movie theater based on the original name. If the movie doesn't exists then a not found error will be answered. This is a GET request with this url `http://localhost:5000/api/movie/#` where `#` represents the movie original name of the request. the body aswered has the form of the movie JSON body.


###### DELETE movie

Deletes a movie from the database. If the movie doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/movie/#` where `#` represents the original name of the request. This request has no answer.

###### PUT movie

Updates the information from a movie in the database. If the movie doesn't exists then a not found error (404) will be answered. This is a PUT request with this url `http://localhost:5000/api/movie/#` where `#` represents the original name of the movie of the request. The body of this requests is the one specified for the movie.

###### GET movie by movie theater

This requests shows all the movies available in one especific movie theater. The url is `http://localhost:5000/api/filter_movie/{theater_name}`. The body of the answer corresponds to the one of movies.

### Screening Requests

###### POST screening

Add a new screening, there is a foreign key in cinemaNumber and movieOriginalName foreign keys, if one of these keys is not in the database a Conflict will be returned. This is a POST request with this url `http://localhost:5000/api/screening`.

There is no need to include the id in the post JSON, the backend will define the id, a JSON like this one will be enough:

```Json
{

  "cinemaNumber": 1,
  "movieOriginalName": "Titanic",
  "hour": 10,
  "capacity": 50
}
```

###### GET screening

Get all the screening saved in the database. This is a GET request with this url `http://localhost:5000/api/screening` the body aswered has the form of the screening JSON body.

###### GET screening by id

Get an specific screening based on the id. If the movie doesn't exists then a not found error will be answered. This is a GET request with this url `http://localhost:5000/api/screening/#` where `#` represents the screening id of the request. the body aswered has the form of the movie JSON body.


###### DELETE screening

Deletes a screening from the database. If the screening doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/screening/#` where `#` represents the id of the request. This request has no answer.

###### PUT screening

Updates the information from a screening in the database. If the screening doesn't exists then a not found error (404) will be answered. Also this relation has the cinemaNumber and movieOriginalName foreign keys, if one of these keys is not in the database a Conflict will be returned. This is a PUT request with this url `http://localhost:5000/api/movie/#` where `#` represents the id of the screening of the request. The body of this requests is the one specified for the screening.

###### GET screening by movie theater and movie name

This requests is used to filter the movies based on a movie theater and movie name. The url is `http://localhost:5000/api//screening/filter_screening/{theater_name}/{movie_name}`. The answer of this requests corresponds to the one of screening.


### Seat Requests

###### POST seat

Add a new seat, where the combination of screeningId rowNum and columnNum is the key attribute. If theres already a seat with the respective combination there will be an 409 error (Conflict). Also this relation has the screeningID foreign key, if this keys is not in the database a Conflict will be returned. This is a POST request with this url `http://localhost:5000/api/seat` the body to include corresponds to the one of the seat.

This requests is used internally from the database, every time a screening is created a set of seats are created too.

###### GET seats by screeningId

Get all the seats that have the indicated screeningId. If the screeningId doesn't exists then a conflict error will be answered. This is a GET request with this url `http://localhost:5000/api/seat/#` where `#` represents the screeningid of the request. the body aswered has the form of the movie JSON body.


###### PUT seat

Updates the information from a seat with the respective screeningID, row number and column number. If the screeningID doesn't exists the a conflict is returned. The url of this request is `http://localhost:5000/api/movie/{screening_id}/{row_number}/{column_number}`.


### Actors Requests

###### POST actor

Add a new actor, where the original movie name and the actor name is the key attribute. If theres already a key attribute with the respective original name there will be an 409 error (Conflict). This is a POST request with this url `http://localhost:5000/api/actor` the body to include corresponds to the one of the actor.

###### GET actors

Get all the actors saved in the database. This is a GET request with this url `http://localhost:5000/api/actor` the body aswered has the form of the actors JSON body.

###### DELETE actor

Deletes a actor from the database. If the actor doesn't exists then a not found error (404) will be answered. This is a DELETE request with this url `http://localhost:5000/api/actor/{movie_name}/{name}` where `movie_name` represents the original name of the movie and name the name of the actor to remove. This request has no answer.



