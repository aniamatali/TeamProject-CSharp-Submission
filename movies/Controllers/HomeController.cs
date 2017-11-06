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

    }

}
