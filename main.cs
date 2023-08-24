using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.X509;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////
            string cs = "server=127.0.0.1;uid=root;pwd=parlerler1543#;database=oracle";

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = """DROP TABLE IF EXISTS cars""";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY AUTO_INCREMENT,name TEXT, price INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('audi' ,9999)";
            cmd.ExecuteNonQuery();
            ///////////////////////////////////////////////////

            Location test = new Location(1.2, 3.2);
            CreditExpiry expdate = new CreditExpiry(09, 27);
            CreditInfo wallet1 = new CreditInfo("parlerler", "1010010101", expdate, "033", 0.0);
            Rider one = new Rider("Kareem", "Essam", "kareemessam2101@gmail.com", "testtube", "01092217001", test, wallet1);

            Context context;
            context = new Context(new creditCardMethod());

            Console.WriteLine("Login or Register?");
            string ch = Console.ReadLine()!;

            if (ch == "login" || ch == "Login")
            {
                one.Login(cs);
            }
            else if (ch == "register" || ch == "Register")
            {
                one.RegisterUser(cs);
                one.Login(cs);
            }

            Console.WriteLine(one.FirstName);
            Console.WriteLine(one.LastName);
            Console.WriteLine(one.Email);
            Console.WriteLine(one.Password);
            Console.WriteLine(one.PhoneNumber);


            Console.WriteLine("Select action");
            Console.WriteLine("1: Edit credit card info\n2: Check credit info\n3: Deposit money\n4: Check current funds\n5: Get a ride");
            int action;
            char actionloop = 'y';
            do
            {
                action = Convert.ToInt32(Console.ReadLine());
                if (action == 1)
                {
                    //pcon.registerCreditInfo(one);
                }
                else if (action == 2)
                {
                    one.checkCreditInfo();
                }
                else if (action == 3)
                {
                    one.depositCash();
                }
                else if (action == 4)
                {
                    Console.WriteLine(one.CreditInfo.CreditBalance);
                }
                else if (action == 5)
                {
                    Console.WriteLine("\n1 : Ride \n2 : Ride AC+ \n3 : Moto \n4 : Freight");
                    int choose;
                    choose = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nenter your x-coordinate");

                    int x1 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nenter your y-coordinate");

                    int x2 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("enter your destination's x-coordinate");

                    int y1 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("enter your destination's y-coordinate");

                    int y2 = Convert.ToInt32(Console.ReadLine());

                    Location loc = new Location(x1, y1);

                    Location des = new Location(x2, y2);

                    if (choose == 1)
                    {
                        Ride ride = new Ride();
                        ride.RequestRide(loc, des);
                    }
                    else if (choose == 2)
                    {
                        RideAC ride = new RideAC();
                        ride.RequestRide(loc, des);
                    }
                    else if (choose == 3)
                    {
                        Moto ride = new Moto();
                        ride.RequestRide(loc, des);
                    }
                    else if (choose == 4)
                    {
                        Freight ride = new Freight();
                        ride.RequestRide(loc, des);
                    }
                }

                else if (action == 6)
                {
                    // feedback, inquire
                }

                Console.WriteLine("would you like to do another action? type Y to continue");
                actionloop = Convert.ToChar(Console.ReadLine()!);
            } while (actionloop == 'y' || actionloop == 'Y');
        }
    }
}