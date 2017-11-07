using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using EMDB.Models;
using EMDB;

namespace EMDB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          Dictionary<string, List<Movie>> model = new Dictionary<string, List<Movie>>();
          return View(model);
        }

        [HttpPost("/")]
        public ActionResult Result()
        {
          Dictionary<string, List<Movie>> model = new Dictionary<string, List<Movie>>();
          List<Movie> resultMovie = Movie.FindTitle(Request.Form["inputTitle"]);
          model.Add("Title",resultMovie);
          return View("index",model);
        }

        [HttpGet("/{id}")]
      public ActionResult MovieDetails(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Movie selectedMovie = Movie.Find(id);
        model.Add("movie", selectedMovie);
        return View(model);
      }

      [HttpPost("/{id}")]
      public ActionResult ReviewDetails(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        // List<Movie> resultMovie = Movie.FindTitle(Request.Form["inputReview"]);
        model.Add("Title",resultMovie);
        Movie selectedMovie = Movie.Find(id);
        model.Add("movie", selectedMovie);
        return View("moviedetails",model);
      }

    }

}
