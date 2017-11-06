using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using EMDB.Models;
using EMDB;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }

        [HttpPost("/result")]
        public ActionResult Result()
        {
          Dictionary<string, List<Movie>> model = new Dictionary<string, List<Movie>>();
          List<Movie> resultMovie = Movie.FindTitle(Request.Form["inputTitle"]);
          List<Movie> resultGenre = Movie.FindGenre(Request.Form["inputGenre"]);
          model.Add("Title",resultMovie);
          model.Add("Genre",resultGenre);
          return View(model);
        }

    }

}
