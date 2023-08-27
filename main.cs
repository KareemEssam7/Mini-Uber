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

            User activeUser = new User();


            IHandler loginHandler = new LoginHandler();
            IHandler registerHandler = new RegisterHandler();

            loginHandler.SetNext(registerHandler);
            Console.WriteLine("Register or Login?");
            string enteraction = Console.ReadLine()!;
            Console.WriteLine(loginHandler.HandleRequest(enteraction, activeUser, cs));

            string paymentMethod = CreditMethods.GetUserPaymentMethod();
            IPaymentStrategy paymentStrategy;

            if (paymentMethod == "creditcard")
            {
                paymentStrategy = new CreditCardPayment("parlerler", "1010010101", 0, 0, "033", 0.0);
                CreditCardPayment activeCredit = new CreditCardPayment("parlerler", "1010010101", 0, 0, "033", 0.0);
                activeCredit.registerCreditInfo();
            }
            else if (paymentMethod == "paypal")
            {
                Console.WriteLine("Enter paypal Email");
                string email = Console.ReadLine()!;
                paymentStrategy = new PayPalPayment(email);
            }
            else if (paymentMethod == "cash")
            {
                Console.WriteLine("Thank you");
            }
            //should only take this information when regestering user.
            //should register this info into the database with the rest of the user info.
        }
    }
}