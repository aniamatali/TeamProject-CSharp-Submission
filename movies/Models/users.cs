using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace EMDB.Models
{
  public class Users
  {
      private int _id;
      private string _name;
      private string _username;
      private string _password;

      public Users(string name, string username, string password, int id = 0)
      {
          _id = id;
          _name = name;
          _username = username;
          _password = password;
      }

      public int GetId()
      {
          return _id;
      }

      public string GetName()
      {
          return _name;
      }

      public string GetUsername()
      {
          return _username;
      }

      public string GetPassword()
      {
          return _password;
      }

      public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO users (name, username, password) VALUES (@name, @username, @password);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter username = new MySqlParameter();
      username.ParameterName = "@username";
      username.Value = this._username;
      cmd.Parameters.Add(username);

      MySqlParameter password = new MySqlParameter();
      password.ParameterName = "@password";
      password.Value = this._password;
      cmd.Parameters.Add(password);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

        public bool IsNewUsers()
       {
        bool IsNewUsers = true;
        List<Users> allUsers = new List<Users>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM users;";

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int userId = rdr.GetInt32(0);
          string userName = rdr.GetString(1);
          string userUsername = rdr.GetString(2);
          string userPassword = rdr.GetString(3);
          Users newUsers = new Users(userName,userUsername,userPassword,userId);
          allUsers.Add(newUsers);
        }

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

        foreach (var user in allUsers)
        {
          if(user.GetUsername() == _username)
          {
            IsNewUsers = false;
          }
        }
        return IsNewUsers;
      }

      public static Users FindUser(string username, string password)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM users WHERE username = (@searchUsername) AND password = (@searchPassword);";

        MySqlParameter searchUsername = new MySqlParameter();
        searchUsername.ParameterName = "@searchUsername";
        searchUsername.Value = username;
        cmd.Parameters.Add(searchUsername);

        MySqlParameter searchPassword = new MySqlParameter();
        searchPassword.ParameterName = "@searchPassword";
        searchPassword.Value = password;
        cmd.Parameters.Add(searchPassword);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        string userName = "";
        string userUsername = "";
        string userPassword = "";
        int userId = 0;

        while(rdr.Read())
        {
          userId = rdr.GetInt32(0);
          userName = rdr.GetString(1);
          userUsername = rdr.GetString(2);
          userPassword = rdr.GetString(3);
        }

        Users newUser = new Users(userName, userUsername, userPassword, userId);

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

        return newUser;
      }

      public static Users FindId(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM users WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int userId = 0;
        string userName = "";
        string userUsername = "";
        string userPassword = "";

        while(rdr.Read())
        {
          userId = rdr.GetInt32(0);
          userName = rdr.GetString(1);
          userUsername = rdr.GetString(2);
          userPassword = rdr.GetString(3);
        }
        Users newUsers = new Users(userName, userUsername, userPassword, userId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newUsers;
      }
  }
}
