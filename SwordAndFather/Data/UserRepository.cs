﻿using SwordAndFather.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        //static List<User> _users = new List<User>();

        const string ConnectionString = "Server=localhost; Database=SwordAndFather; Trusted_Connection=True;";

        public User AddUser(string username, string password)
        {
            //var newUser = new User(username, password);

            //newUser.Id = _users.Count + 1;

            //_users.Add(newUser);

            //return newUser;

            using (var connection = new SqlConnection(ConnectionString)) //the using statement makes this connection close at the end of the code block
            {

                 var newUser = connection.QueryFirstOrDefault<User>(@"Insert into users (username, password)
                                                        Output inserted.*
                                                        Values(@username,@password)", 
                     new { Username = username, Password = password /* is the same as {username, password} */ } );

                if (newUser != null)
                {
                    return newUser;
                }

                /*

                connection.Open();

                var insertUserCommand = connection.CreateCommand();

                insertUserCommand.CommandText = $@"Insert into users (username, password)
                                              Output inserted.*
                                              Values(@username,@password)";

                insertUserCommand.Parameters.AddWithValue("username", username); //used to avoid SQL injection from usernames or passwords of really smart people(little bobby tables)
                insertUserCommand.Parameters.AddWithValue("password", password);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUsername = reader["username"].ToString();
                    var insertedPassword = reader["password"].ToString();
                    var insertedId = (int)reader["Id"];

                    var newUser = new User(insertedUsername, insertedPassword) { Id = insertedId };

                    connection.Close();

                    return newUser;
                }

                */
            }

            throw new Exception("No user found");

        }

        public void DeleteUser(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
               var rowsAffected = db.Execute("Delete from Users where Id = @id", new { id }); //that's it.

               if (rowsAffected != 1)
                {
                    throw new Exception("Didn't do right");
                }
            }
        }

        public User UpdateUser(User userToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Execute(@"Update Users 
                             Set username = @username,
                                 password = @password
                             Where id = @id", userToUpdate); 

                return userToUpdate;
            }


        }

        public IEnumerable<User> GetAll()
        {

            using (var db = new SqlConnection(ConnectionString))
            {

                //db.Open();  dapper auto opens the connection, and the using statement will close it when the block of code is done.

                return db.Query<User>("select username,password,id from users"); //This is all that's left when we use Dapper.  This is a little slower to run because of the ORM that runs it.

                /*
                 
                var getAllUsersCommand = connection.CreateCommand();

                getAllUsersCommand.CommandText = @"select username,password,id
                                              from users";

                var reader = getAllUsersCommand.ExecuteReader();

                while (reader.Read()) //reader.Read is a bool that checks to see if there is any data left
                {
                    // reader[0]  -this returns an object?
                    var id = (int)reader["id"];
                    var username = reader["username"].ToString();
                    var password = reader["password"].ToString();
                    var user = new User(username, password) { Id = id };

                    users.Add(user);
                }

               */

            }

        }

    }

}
