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

            //Login and register logic
            IHandler loginHandler = new LoginHandler();
            IHandler registerHandler = new RegisterHandler();
            loginHandler.SetNext(registerHandler);
            Console.WriteLine("Register or Login?");
            string enteraction = Console.ReadLine()!;
            Console.WriteLine(loginHandler.HandleRequest(enteraction, activeUser, cs));

            //geting payment type
            PaymentSetter paymentSetter = new PaymentSetter();
            PaymentMethods.GetUserPaymentMethod(paymentSetter);


            IIHandler passwordReset = new PasswordReset();
            IIHandler emailReset = new EmailReset();
            IIHandler phoneNumberReset = new PhoneNumberReset();
            passwordReset.SetNext(emailReset);
            emailReset.SetNext(phoneNumberReset);
            string answer;
            Console.WriteLine("Do You Want To Change Your Account Information? Y/N");
            answer = Console.ReadLine()!;
            while (answer == "Y" || answer == "y")
            {
                Console.WriteLine("Choose What You Want To Change:\n1-Change Password\n2-Change Email\n3-Change PhoneNumber");
                enteraction = Console.ReadLine()!;
                int command;
                command = Convert.ToInt32(enteraction);
                Console.WriteLine(passwordReset.HandleRequest(command, activeUser, cs));
                Console.WriteLine("Do You Want To Change Your Account Information? Y/N");
                answer = Console.ReadLine()!;
            }
        }
    }
}