# EMDB:C# Week 5 Collaboration Project November 2017

#### _ 11.6.17_

#### Authors
**
Marcus Parmentier, Isaac Niamatali, Hansen, John Murray, Victor M. Puentes Jr**

## Description

An app that will emulate IMBD but with some custom modifications.

## Specifications

| User Behavior | Input | Output |
|---|---|---|
|1. As a user I need to be able to sign in, by creating a username and password | enter username and password | username and password is saved to database, true. |
|2. With a username and password, I can enter homepage and start a search | dark knight | all movies with "dark knight" in the title are appended. |
|3. Once movies are appended I can now click on the movie and see details pulled from the database | movie details are appended including realease date, homewebpage, countries it was produced in, etc... | movie details are appended |
|4. Once movie details are appended, I can now write my own review of the movie. | "this movie should be erased from history!!!" | review is appended |


## Getting Started

May be deployed using git hub at  https://github.com/heartfeats/Epicodus-Movie-Database.git

### Installation/Setup Requirements
1.This app may be cloned at https://github.com/heartfeats/Epicodus-Movie-Database.git
2. Set up .NET dependencies
3. Set up database with MAMP and create a database with these instructions...

## Database Setup
> CREATE DATABASE emdb;
> USE emdb;
> CREATE TABLE movie (id serial PRIMARY KEY, name VARCHAR(255), keywords VARCHAR(255), title VARCHAR(255), > > revenue INT, overview VARCHAR(255), homepage VARCHAR(255), original_language VARCHAR(255), origninal_title VARCHAR(255), overview VARCHAR(255), popularity VARCHAR(255), production_countries VARCHAR(255), release_date DATE, runtime INT, spoken_languages VARCHAR(255), statue INT, tagline VARCHAR(255), title VARCHAR(255), vote_average INT, vote_count INT););
> CREATE TABLE user_details (id serial PRIMARY KEY, description VARCHAR(255))


## Built With

* [C#](https://learnhowtoprogram.com/couses/c#)
* .NET Framework
* MVC
* [HTML5](https://developer.mozilla.org/en-US/docs/Web/Guide/HTML/HTML5) - Used to contruct this webpage
* [CSS3](http://html.com/css/) - Used to style
* [Javascript] (https://www.javascript.com/) - Used for user interactives
* [Bootstrap](http://getbootstrap.com/) - CSS library used



## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Epicodus (https://epicodus.com/)
* free Code Camp (https://freecodecamp.com/)
* Software Engineering Daily (https://softwareengineeringdaily.com/)
