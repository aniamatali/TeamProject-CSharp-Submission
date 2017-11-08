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
          return View();
        }

        [HttpGet("/Account")]
        public ActionResult AccountForm()
        {
          return View();
        }

        [HttpPost("/")]
        public ActionResult IndexAccount()
        {
          Users newUser = new Users(Request.Form["inputName"],Request.Form["inputUser"],Request.Form["inputPass"]);
          if(newUser.IsNewUsers())
          {
            newUser.Save();
          }
          return View("index");
        }

        [HttpPost("/homepage")]
        public ActionResult Result()
        {
          if(Request.Form["inputUser"] ==  "ω")
          {
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<Movie> resultMovie = Movie.FindTitle(Request.Form["inputTitle"]);
            model.Add("Title",resultMovie);
            Users foundUser = Users.FindId(Int32.Parse(Request.Form["userId"]));
            model.Add("User",foundUser);
            return View("homepage",model);
          }
          else
          {
            Users foundUser = Users.FindUser(Request.Form["inputUser"],Request.Form["inputPass"]);
            if(foundUser.GetUsername() == "")
            {
              return View("index");
            }

            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("User",foundUser);
            return View("homepage",model);
          }
        }

        [HttpPost("/{id}")]
        public ActionResult MovieDetails(int id)
        {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Movie selectedMovie = Movie.Find(id);
        model.Add("movie", selectedMovie);
        Users foundUser = Users.FindId(Int32.Parse(Request.Form["userId"]));
        model.Add("User",foundUser);
        if(Request.Form["inputReview"] != "")
        {
            selectedMovie.AddReviewUser(foundUser, Request.Form["inputReview"]);
        }

        List<Review> ListReview = Review.GetAllReview(id);
        model.Add("Review", ListReview);

        return View(model);
        }


    }

}
