using System;
using System.Data;
using System.Data.SqlTypes;
using Org.BouncyCastle.Cms;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

 // singleton
public class UserManager
{
    private static UserManager _instance;

    private UserManager() { }

    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            return _instance;
        }
    }

    public void Register(User newUser, string connectionstring)
    {
        using var con = new MySqlConnection(connectionstring);
        con.Open();
        MySqlCommand cmd = new MySqlCommand(@"Insert into newww_users(FirstName, LastName, Email, Password, PhoneNumber) values (@FirstName, @LastName, @Email, @Password, @PhoneNumber)", con);
        cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
        cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
        cmd.Parameters.AddWithValue("@Email", newUser.Email);
        cmd.Parameters.AddWithValue("@Password", newUser.Password);
        cmd.Parameters.AddWithValue("@PhoneNumber", newUser.PhoneNumber);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void Login(string email, string password,string connectionstring)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM newww_users WHERE Email = @Email AND Password = @Password";
                using MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                if (count > 0)
                {
                    Console.WriteLine("Login successful!");
                }

                connection.Close();
            }
    }
}

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}