INSERT INTO CLIENT (ID, First_name, Last_name, Sec_last_name, Age, Birth_date, Phone_number, Password)
    VALUES  (1, 'Juan', 'Navarro', 'Navarro', '20', '2001-02-08', '87175508', 'passwordnacho'),
            (2, 'Monica', 'Waterhouse', 'Montoya', '20', '2001-02-08', '871755555', 'passwordmoni'),
            (3, 'Luis', 'Morales', 'Rodriguez', '20', '2001-02-08', '87778899', 'passwordluis'),
            (4, 'Ignacio', 'Grnados', 'Marin', '20', '2001-02-08', '87176644', 'passwordnacho');


INSERT INTO MOVIE_THEATER (Name, Location, Cinema_amount)
    VALUES  ('Paseo', 'Cartago', 2),
            ('Multiplaza', 'San Jose', 1),
            ('CineMark', 'Heredia', 2);


INSERT INTO  CINEMA (Number, Rows, Columns, Capacity, Name_movie_theater)
    VALUES  (1, 10, 10, 50, 'Paseo'),
            (2, 15, 15, 50, 'Paseo'),
            (3, 10, 15, 30, 'Multiplaza'),
            (4, 20, 15, 50, 'CineMark'),
            (5, 10, 10, 50, 'CineMark');


INSERT INTO MOVIE (Original_name, Gendre, Director, Image_url, Lenght)
    VALUES ('Titanic', 'Suspense', 'Cameron', 'https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_.jpg', 180),
           ('Minions', 'Comedy', 'Polack', 'https://images.moviesanywhere.com/913f00d3cac979222c7db177dead87ae/a2df1afa-57b8-48a9-bec8-4293d0097236.jpg', 120),
           ('Space Jam', 'Comedy', 'Lee', 'https://c8.alamy.com/compes/2ecg8yh/space-jam-1996-dirigida-por-joe-pytka-y-protagonizada-por-michael-jordan-wayne-knight-y-theresa-randle-accion-en-vivo-pelicula-hibrida-animada-donde-los-personajes-de-dibujos-animados-looney-tunes-inscriben-la-ayuda-de-michael-jordan-para-ganar-un-partido-de-baloncesto-y-su-libertad-2ecg8yh.jpg', 140),
           ('Black Widow', 'Action', 'Shortlan', 'https://images-na.ssl-images-amazon.com/images/I/914MHuDfMSL.jpg', 110);


INSERT INTO ACTORS(Original_movie_name, Actor_name)
    VALUES  ('Titanic', 'DiCaprio'),
            ('Titanic', 'Winslet'),
            ('Space Jam', 'LeBron'),
            ('Space Jam', 'Zendaya'),
            ('Space Jam', 'Jordan'),
            ('Black Widow', 'Johansson'),
            ('Black Widow', 'Pugh');


