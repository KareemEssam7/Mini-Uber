using MySql.Data.MySqlClient;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////
            string cs = "server=127.0.0.1;uid=root;pwd=parlerler1543#;database=oracle; Allow User Variables=True;";

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

            //Object Initializations
            PaymentInfoToStore paymentInfoToStore = new PaymentInfoToStore();
            User activeUser = new User();
            CreditCardPayment activeCredit = new CreditCardPayment("default", "0", 0, 0, "000");
            PaymentSetter paymentSetter = new PaymentSetter();

            //Login and register logic
            IHandler loginHandler = new LoginHandler();
            IHandler registerHandler = new RegisterHandler();
            loginHandler.SetNext(registerHandler);
            Console.WriteLine("Register or Login?");
            string enteraction = Console.ReadLine()!;
            Console.WriteLine(loginHandler.HandleRequest(enteraction, activeUser, cs, paymentSetter, activeCredit, paymentInfoToStore));
            Console.WriteLine(paymentInfoToStore.paymentType);

            //Main Logic
            int userChoice;
            Console.WriteLine("To request rides Press: 1 \nTo change the Account Information Press: 2");
            userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                RequestRide HandleRide = new RequestRide();
                HandleRide.HandleRide(paymentSetter);
            }
            else
            {
                IIHandler passwordReset = new PasswordReset();
                IIHandler emailReset = new EmailReset();
                IIHandler phoneNumberReset = new PhoneNumberReset();
                passwordReset.SetNext(emailReset);
                emailReset.SetNext(phoneNumberReset);
                UpdaateUserInformation.UserInput(passwordReset, activeUser, cs);
            }
        }
    }
}