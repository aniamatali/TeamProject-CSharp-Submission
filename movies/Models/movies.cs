using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace EMDB.Models
{
  public class Movie
  {
    private string _title;
    private string _tagline;
    private string _overview;
    private string _status;
    private string _releaseDate;
    private int _runtime;
    private float _voteAverage;
    private int _voteCount;
    private int _budget;
    private int _revenue;
    private string _homepage;
    private int _id;


    public Movie(string title, string tagline, string overview, string status, string releaseDate, int runtime, float voteAverage, int voteCount, int budget, int revenue, string homepage, int id)
    {
      _title = title;
      _tagline = tagline;
      _overview = overview;
      _status = status;
      _releaseDate = releaseDate;
      _runtime = runtime;
      _voteAverage = voteAverage;
      _voteCount = voteCount;
      _budget = budget;
      _revenue = revenue;
      _homepage = homepage;
      _id = id;
    }

    // public float FigureRevenue()
    // {
    //   float result =  (float)_revenue * 100 / ((float)_budget + (float)_revenue);
    //   return result;
    // }
    //
    // public float FigureBudget()
    // {
    //   float result =  (float)_budget * 100 / ((float)_budget + (float)_revenue);
    //   return result;
    // }

    public float ProfitPercentageOfRevenue()
    {
      float result =  ((float)_revenue - (float)_budget) / (float)_revenue * 100;
      return result;
    }

    public float BudgetPercentageOfRevenue()
    {
      float result = (float)_budget / (float)_revenue * 100;
      return result;
    }

    public int FigureProfit()
    {
      int result = _revenue - _budget;
      return result;
    }

    public string GetTitle()
    {
      return _title;
    }

    public string GetTagline()
    {
      return _tagline;
    }

    public string GetOverview()
    {
      return _overview;
    }

    public string GetStatus()
    {
      return _status;
    }

    public string GetReleaseDate()
    {
      return _releaseDate;
    }

    public int GetRuntime()
    {
      return _runtime;
    }

    public float GetVoteAverage()
    {
      return _voteAverage;
    }

    public int GetVoteCount()
    {
      return _voteCount;
    }

    public int GetBudget()
    {
      return _budget;
    }

    public int GetRevenue()
    {
      return _revenue;
    }

    public string GetHomepage()
    {
      return _homepage;
    }

    public int GetId()
    {
      return _id;
    }

    public override int GetHashCode()
    {
      return this.GetTitle().GetHashCode();
    }

    public static List<Movie> FindTitle(string title)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT DISTINCT * FROM movies WHERE title LIKE (@searchTitle) ORDER BY vote_count DESC LIMIT 100;";

      MySqlParameter searchTitle = new MySqlParameter();
      searchTitle.ParameterName = "@searchTitle";

      if(title.Length <= 3)
      {
        searchTitle.Value = title+'%';
        cmd.Parameters.Add(searchTitle);
      }
      else
      {
        searchTitle.Value = '%'+title+'%';
        cmd.Parameters.Add(searchTitle);
      }

      MySqlParameter searchKey = new MySqlParameter();
      searchKey.ParameterName = "@searchKey";
      searchKey.Value = '%'+title+'%';
      cmd.Parameters.Add(searchKey);

      List<Movie> MovieList = new List<Movie> {};

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        string movieTitle = rdr.GetString(17);
        string movieTagline = rdr.GetString(16);
        string movieOverview = rdr.GetString(7);
        string movieStatus = rdr.GetString(15);
        string movieReleaseDate = rdr.GetString(11);
        int movieRuntime = rdr.GetInt32(13);
        float movieVoteAverage = rdr.GetFloat(18);
        int movieVoteCount = rdr.GetInt32(19);
        int movieBudget = rdr.GetInt32(0);
        int movieRevenue = rdr.GetInt32(12);
        string movieHomepage = rdr.GetString(2);
        int movieId = rdr.GetInt32(3);

        Movie newMovie = new Movie(movieTitle, movieTagline, movieOverview, movieStatus, movieReleaseDate, movieRuntime, movieVoteAverage, movieVoteCount, movieBudget, movieRevenue, movieHomepage, movieId);

        MovieList.Add(newMovie);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      if(title.Length == 0)
      {
        List<Movie> EmptyList = new List<Movie> {};
        return EmptyList;
      }

      return MovieList;
    }

    public static List<Movie> FindGenre(string genre)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT DISTINCT * FROM movies WHERE genres LIKE (@searchGenre) ORDER BY vote_count DESC LIMIT 5;";

      MySqlParameter searchGenre = new MySqlParameter();
      searchGenre.ParameterName = "@searchGenre";
      searchGenre.Value = '%'+genre+'%';
      cmd.Parameters.Add(searchGenre);

      List<Movie> MovieList = new List<Movie> {};

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        string movieTitle = rdr.GetString(17);
        string movieTagline = rdr.GetString(16);
        string movieOverview = rdr.GetString(7);
        string movieStatus = rdr.GetString(15);
        string movieReleaseDate = rdr.GetString(11);
        int movieRuntime = rdr.GetInt32(13);
        float movieVoteAverage = rdr.GetFloat(18);
        int movieVoteCount = rdr.GetInt32(19);
        int movieBudget = rdr.GetInt32(0);
        int movieRevenue = rdr.GetInt32(12);
        string movieHomepage = rdr.GetString(2);
        int movieId = rdr.GetInt32(3);

        Movie newMovie = new Movie(movieTitle, movieTagline, movieOverview, movieStatus, movieReleaseDate, movieRuntime, movieVoteAverage, movieVoteCount, movieBudget, movieRevenue, movieHomepage, movieId);

        MovieList.Add(newMovie);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      if(genre.Length == 0)
      {
        List<Movie> EmptyList = new List<Movie> {};
        return EmptyList;
      }
      return MovieList;
    }

    public void AddReviewUser(Users newUsers, string review)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO movies_users (movies_id, users_id, review) VALUES (@moviesId, @usersId, @review);";

      MySqlParameter moviesId = new MySqlParameter();
      moviesId.ParameterName = "@moviesId";
      moviesId.Value = this._id;
      cmd.Parameters.Add(moviesId);

      MySqlParameter usersId = new MySqlParameter();
      usersId.ParameterName = "@usersId";
      usersId.Value = newUsers.GetId();
      cmd.Parameters.Add(usersId);

      MySqlParameter reviewInput = new MySqlParameter();
      reviewInput.ParameterName = "@review";
      reviewInput.Value = review;
      cmd.Parameters.Add(reviewInput);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Movie Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM movies WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int MovieId = 0;
      string MovieTitle = "";
      string movieTagline = "";
      string movieOverview = "";
      string movieStatus = "";
      string movieReleaseDate = "";
      int movieRuntime = 0;
      float movieVoteAverage = 0;
      int movieVoteCount = 0;
      int movieBudget = 0;
      int movieRevenue = 0;
      string movieHomepage = "";


      while(rdr.Read())
      {
        MovieId = rdr.GetInt32(3);
        MovieTitle = rdr.GetString(17);
        movieTagline = rdr.GetString(16);
       movieOverview = rdr.GetString(7);
        movieStatus = rdr.GetString(15);
        movieReleaseDate = rdr.GetString(11);
         movieRuntime = rdr.GetInt32(13);
         movieVoteAverage = rdr.GetFloat(18);
         movieVoteCount = rdr.GetInt32(19);
         movieBudget = rdr.GetInt32(0);
         movieRevenue = rdr.GetInt32(12);
        movieHomepage = rdr.GetString(2);
      }
      Movie newMovie = new Movie(MovieTitle, movieTagline, movieOverview, movieStatus, movieReleaseDate, movieRuntime, movieVoteAverage, movieVoteCount, movieBudget, movieRevenue, movieHomepage, MovieId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newMovie;
    }

    // public static List<Movie> GetBest()
    // {
    //   List<Movie> allMovie = new List<Movie> {};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT TOP 5 FROM movies;";
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   while(rdr.Read())
    //   {
    //     int bookId = rdr.GetInt32(0);
    //     string bookTitle = rdr.GetString(1);
    //     int bookCopies = rdr.GetInt32(2);
    //     Movie newMovie = new Movie(bookTitle, bookCopies, bookId);
    //     allMovie.Add(newMovie);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return allMovie;
    // }


  }

}
