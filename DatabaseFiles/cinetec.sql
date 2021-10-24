CREATE TABLE CLIENT
(
    ID              INT  PRIMARY KEY,
    First_name      VARCHAR(20),
    Last_name       VARCHAR(20),
    Sec_last_name   VARCHAR(20),
    Age             INT,
    Birth_date      DATE,
    Phone_number    VARCHAR(20),
	Password		VARCHAR(30)
);

CREATE TABLE MOVIE_THEATER
(
    Name            VARCHAR(20) PRIMARY KEY,
    Location        VARCHAR(20),
    Cinema_amount   INT
);

CREATE TABLE CINEMA
(
    Number              INT PRIMARY KEY,
    Rows                INT,
    Columns             INT,
    Capacity            INT,
    Name_movie_theater  VARCHAR(20)
);

CREATE TABLE MOVIE
(
    Original_name       VARCHAR(20) PRIMARY KEY,
    Gendre              VARCHAR(20),
    Name                VARCHAR(20),
    Director            VARCHAR(20),
	Image_url			VARCHAR(350),
    Lenght              INT
);

CREATE TABLE SCREENING
(
    ID                  INT PRIMARY KEY,
    Cinema_number       INT,
    Movie_original_name VARCHAR(20),
    Hour                INT,
    Capacity            INT
);

CREATE TABLE ACTORS
(
    Original_movie_name VARCHAR(20),
    Actor_name          VARCHAR(20),
    PRIMARY KEY (Original_movie_name, Actor_name)
);

CREATE TABLE SEAT
(
    Screening_id       INT,
    Row_num             INT,
    Column_num          INT,
	Purchase_id			INT,
    State               VARCHAR(20),
    PRIMARY KEY (Screening_id, Row_num, Column_num)
);


CREATE TABLE PURCHASE
(
	PurchaseId			INT PRIMARY KEY,
	ClientId			INT,
	TheaterName		VARCHAR(20),
	MovieOriginalName	VARCHAR(20),
	ScreeningId			INT,
	Date				DATE
);

CREATE TABLE EMPLOYEE
(
    ID              INT  PRIMARY KEY,
    First_name      VARCHAR(20),
    Last_name       VARCHAR(20),
    Sec_last_name   VARCHAR(20),
    Age             INT,
    Birth_date      DATE,
    Phone_number    VARCHAR(20),
	Password		VARCHAR(30),
	Name_movie_theater  VARCHAR(20),
	Role			VARCHAR(30),
	Entry_date		DATE
);

ALTER TABLE PURCHASE
ADD CONSTRAINT PURCHASE_CLIENT_FK FOREIGN KEY (ClientId)
REFERENCES CLIENT(ID);

ALTER TABLE PURCHASE
ADD CONSTRAINT PURCHASE_MOVIE_THEATER_FK FOREIGN KEY (TheaterName)
REFERENCES MOVIE_THEATER(Name);

ALTER TABLE PURCHASE
ADD CONSTRAINT PURCHASE_MOVIE_FK FOREIGN KEY (MovieOriginalName)
REFERENCES MOVIE(Original_name);


ALTER TABLE PURCHASE
ADD CONSTRAINT PURCHASE_SCREENING_FK FOREIGN KEY(ScreeningId)
REFERENCES SCREENING(ID);

ALTER TABLE CINEMA
ADD CONSTRAINT CINEMA_MOVIE_THEATER_FK FOREIGN KEY (Name_movie_theater)
REFERENCES MOVIE_THEATER(Name);

ALTER TABLE SCREENING
ADD CONSTRAINT SCREENING_CINEMA_FK FOREIGN KEY (Cinema_number)
REFERENCES CINEMA(Number);

ALTER TABLE SCREENING
ADD CONSTRAINT SCREENING_MOVIE_FK FOREIGN KEY (Movie_original_name)
REFERENCES MOVIE(Original_name);

ALTER TABLE SEAT
ADD CONSTRAINT SEAT_SCREENING_FK FOREIGN KEY (Screening_id)
REFERENCES SCREENING(ID);

ALTER TABLE SEAT
ADD CONSTRAINT SEAT_PURCHASE_FK FOREIGN KEY (Purchase_id)
REFERENCES PURCHASE (PuRchaseId);

ALTER TABLE ACTORS
ADD CONSTRAINT ACTORS_MOVIE_FK FOREIGN KEY (Original_movie_name)
REFERENCES MOVIE (Original_name);

ALTER TABLE EMPLOYEE
ADD CONSTRAINT EMPLOYEE_MOVIE_THEATER_FK FOREIGN KEY (Name_movie_theater)
REFERENCES MOVIE_THEATER(Name);