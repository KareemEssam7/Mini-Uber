using MySql.Data.MySqlClient;

public class UserManager
{
    private static UserManager? _instance;

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
    public string getUserData(User activeUser)
    {
        Console.WriteLine("Enter First Name: ");
        activeUser.FirstName = Console.ReadLine()!;
        while (!Validate.ValidateName(activeUser.FirstName))
        {
            Console.WriteLine("Invalid! Please Reenter Your First Name: ");
            activeUser.FirstName = Console.ReadLine()!;
        }

        Console.WriteLine("Enter Last Name: ");
        activeUser.LastName = Console.ReadLine()!;
        while (!Validate.ValidateName(activeUser.LastName))
        {
            Console.WriteLine("Invalid! Please Reenter Your Last Name: ");
            activeUser.LastName = Console.ReadLine()!;
        }

        Console.WriteLine("Enter Email: ");
        activeUser.Email = Console.ReadLine()!;
        while (!Validate.IsValidEmail(activeUser.Email))
        {
            Console.WriteLine("Invalid! Please Reenter Your Email: ");
            activeUser.Email = Console.ReadLine()!;
        }

        Console.WriteLine("Enter Password (Must Include: Lowercase, Uppercase, Number, Special Char, More Than 8 Chars): ");
        activeUser.Password = Console.ReadLine()!;
        while (!Validate.IsValidPassword(activeUser.Password))
        {
            Console.WriteLine("Password Is Weak (Must Include: Lowercase, Uppercase, Number, Special Char, More Than 8 Chars): ");
            activeUser.Password = Console.ReadLine()!;
        }

        string passConfirm;
        Console.WriteLine("Confirm Password: ");
        passConfirm = Console.ReadLine()!;
        while (passConfirm != activeUser.Password)
        {
            Console.WriteLine("Password Does Not Match! Reenter Your Password: ");
            passConfirm = Console.ReadLine()!;
        }

        Console.WriteLine("Enter PhoneNumber: ");
        activeUser.PhoneNumber = Console.ReadLine()!;
        while (!Validate.ValidateEgyptPhoneNumber(activeUser.PhoneNumber))
        {
            Console.WriteLine("Invalid! Please Reenter Your PhoneNumber: ");
            activeUser.PhoneNumber = Console.ReadLine()!;
        }

        return "Validation Succesful";
    }
    public string Register(User activeUser, string connectionstring, CreditCardPayment activeCredit)
    {
        using var con = new MySqlConnection(connectionstring);
        con.Open();
        MySqlCommand cmd = new MySqlCommand(@"Insert into newww_users(FirstName, LastName, Email, Password, PhoneNumber, PaymentType, CreditNumber, PayPalEmail) values (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @PaymentType, @CreditNumber, @PayPalEmail)", con);
        cmd.Parameters.AddWithValue("@FirstName", activeUser.FirstName);
        cmd.Parameters.AddWithValue("@LastName", activeUser.LastName);
        cmd.Parameters.AddWithValue("@Email", activeUser.Email);
        cmd.Parameters.AddWithValue("@Password", activeUser.Password);
        cmd.Parameters.AddWithValue("@PhoneNumber", activeUser.PhoneNumber);
        cmd.Parameters.AddWithValue("@PaymentType", activeUser.paymentType);
        cmd.Parameters.AddWithValue("@CreditNumber", activeCredit.CreditNumber);
        cmd.Parameters.AddWithValue("@PayPalEmail", activeUser.PayPalEmail);
        cmd.ExecuteNonQuery();
        activeUser.ID = cmd.LastInsertedId;
        con.Close();

        return "User registered successfully.";
    }
    public string Login(User activeUser, string connectionstring, PaymentSetter paymentSetter, CreditCardPayment activeCredit)
    {
        do
        {
            Console.WriteLine("enter email: ");
            activeUser.Email = Console.ReadLine()!;
            Console.WriteLine("enter password: ");
            activeUser.Password = Console.ReadLine()!;

            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();

                string selectQuery = "SELECT id, CreditNumber, PaymentType, PayPalEmail FROM newww_users WHERE email = @Email AND password = @Password";
                using MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@Email", activeUser.Email);
                command.Parameters.AddWithValue("@Password", activeUser.Password);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        activeUser.ID = reader.GetInt32("ID");
                        activeUser.PayPalEmail = reader.GetString("PayPalEmail");
                        activeUser.paymentType = reader.GetString("PaymentType");
                        activeCredit.CreditNumber = reader.GetString("CreditNumber");
                        if (activeUser.paymentType == "creditcard")
                        {
                            paymentSetter.setPaymentStrategy(new CreditCardPayment(activeCredit.CreditName, activeCredit.CreditNumber, activeCredit.Month, activeCredit.Year, activeCredit.CreditCVV));
                        }
                        else if (activeUser.paymentType == "paypal")
                        {
                            paymentSetter.setPaymentStrategy(new PayPalPayment(activeUser.PayPalEmail));
                        }
                        else if (activeUser.paymentType == "cash")
                        {
                            paymentSetter.setPaymentStrategy(new CashPayment());
                        }
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
}


public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public long ID { get; set; }
    public string? paymentType { get; set; }
    public string? PayPalEmail { get; set; }
}