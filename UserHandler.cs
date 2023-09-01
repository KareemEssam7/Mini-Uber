using System;
using System.Data;
using System.Data.SqlTypes;
using Org.BouncyCastle.Cms;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Drawing.Text;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    string HandleRequest(string userCommand, User activeUser, string connectionstring);
}


public class LoginHandler : IHandler
{
    private IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(string userCommand, User activeUser, string connectionstring)
    {
        if (userCommand == "login" || userCommand == "Login")
        {
            do
            {
                Console.WriteLine("enter email: ");
                activeUser.Email = Console.ReadLine();
                Console.WriteLine("enter password: ");
                activeUser.Password = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionstring))
                {
                    connection.Open();

                    string selectQuery = "SELECT ID FROM newww_users WHERE email = @Email AND password = @Password";
                    using MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@Email", activeUser.Email);
                    command.Parameters.AddWithValue("@Password", activeUser.Password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {   
                           activeUser.ID = reader.GetInt32("ID");
                            
                            break;
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                    }

                    connection.Close();
                }
            } while (true);
            return "User logged in successfully.";
        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(userCommand, activeUser, connectionstring);
        }
        else
        {
            return "please re enter your command.";
        }
    }
}


// Concrete register handler
public class RegisterHandler : IHandler
{
    private IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(string userCommand, User activeUser, string connectionstring)
    {
        if (userCommand == "Register" || userCommand == "register")
        {
            Console.WriteLine(RegisterMethod.getUserData(activeUser));

            using var con = new MySqlConnection(connectionstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(@"Insert into newww_users(FirstName, LastName, Email, Password, PhoneNumber) values (@FirstName, @LastName, @Email, @Password, @PhoneNumber)", con);
            cmd.Parameters.AddWithValue("@FirstName", activeUser.FirstName);
            cmd.Parameters.AddWithValue("@LastName", activeUser.LastName);
            cmd.Parameters.AddWithValue("@Email", activeUser.Email);
            cmd.Parameters.AddWithValue("@Password", activeUser.Password);
            cmd.Parameters.AddWithValue("@PhoneNumber", activeUser.PhoneNumber);
            cmd.ExecuteNonQuery();
            activeUser.ID=cmd.LastInsertedId;
            con.Close();
            return "User registered successfully.";
        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(userCommand, activeUser, connectionstring);
        }
        else
        {
            return "please re enter your command.";
        }
    }
}

static class RegisterMethod
{
    public static string getUserData(User activeUser)
    {
        do
        {
            Console.WriteLine("enter first name: ");
            activeUser.FirstName = Console.ReadLine()!;

        } while (!Validate.ValidateName(activeUser.FirstName));

        do
        {
            Console.WriteLine("enter last name: ");
            activeUser.LastName = Console.ReadLine()!;
        } while (!Validate.ValidateName(activeUser.LastName));

        do
        {
            Console.WriteLine("enter Email: ");
            activeUser.Email = Console.ReadLine()!;
        } while (!Validate.IsValidEmail(activeUser.Email));

        do
        {
            Console.WriteLine("enter password(must include: lowercase, uppercase, number, special char, more than 8 chars): ");
            activeUser.Password = Console.ReadLine()!;
        } while (!Validate.IsValidPassword(activeUser.Password));

        string tmppass;
        do
        {
            Console.WriteLine("Confirm password: ");
            tmppass = Console.ReadLine()!;
        } while (tmppass != activeUser.Password);

        do
        {
            Console.WriteLine("enter phone number: ");
            activeUser.PhoneNumber = Console.ReadLine()!;
        } while (!Validate.ValidateEgyptPhoneNumber(activeUser.PhoneNumber));
        return "Validation Succesful";
    }
}