// using System.Collections.Generic;
// using MySql.Data.MySqlClient;
// using System;
//
// namespace EMDB.Models
// {
//   public class Users
//   {
//       private int _id;
//       private string _name;
//       private string _username;
//       private string _password;
//
//       public Users(string name, string username, string password, int id = 0)
//       {
//           _id = id;
//           _name = name;
//           _username = username;
//           _password = password;
//       }
//
//       public int GetId()
//       {
//           return _id;
//       }
//
//       public string GetName()
//       {
//           return _name;
//       }
//
//       public string GetUsername()
//       {
//           return _username;
//       }
//
//       public string GetPassword()
//       {
//           return _password;
//       }
//
//       public static Users FindUser(string username, string password)
//       {
//         MySqlConnection conn = DB.Connection();
//         conn.Open();
//         var cmd = conn.CreateCommand() as MySqlCommand;
//         cmd.CommandText = @"SELECT * FROM users WHERE username = (@searchUsername) AND password = (@searchPassword);";
//
//         MySqlParameter searchUsername = new MySqlParameter();
//         searchUsername.ParameterName = "@searchUsername";
//         searchUsername.Value = username;
//         cmd.Parameters.Add(searchUsername);
//
//         MySqlParameter searchPassword = new MySqlParameter();
//         searchPassword.ParameterName = "@searchPassword";
//         searchPassword.Value = password;
//         cmd.Parameters.Add(searchPassword);
//
//         var rdr = cmd.ExecuteReader() as MySqlDataReader;
//
//         string userName = "";
//         string userUsername = "";
//         string userPassword = "";
//         int userId = 0;
//
//         while(rdr.Read())
//         {
//           userId = rdr.GetInt32(0);
//           userName = rdr.GetString(1);
//           userUsername = rdr.GetString(2);
//           userPassword = rdr.GetString(3);
//         }
//
//         User newUser = new User(userName, userUsername, userPassword, UserId);
//
//         conn.Close();
//         if (conn != null)
//         {
//           conn.Dispose();
//         }
//
//         return newUser;
//       }
//   }
//
// }
