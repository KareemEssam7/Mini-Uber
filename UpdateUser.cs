using MySql.Data.MySqlClient;
public interface IIHandler
{
    IIHandler SetNext(IIHandler handler);
    string HandleRequest(int numCommand, User activeUser, string connectionstring);
}

public class PasswordReset : IIHandler
{
    private IIHandler _nextHandler;

    public IIHandler SetNext(IIHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(int numCommand, User activeUser, string connectionstring)
    {
        if (numCommand == 1)
        {
            string newPassword, confirmPassword;

            Console.WriteLine("Enter New Password: ");
            newPassword = Console.ReadLine()!;
            /*do{
                Console.WriteLine("Password Is Weak. Reenter Your New Password: ");
                newPassword = Console.ReadLine();
            }while(!Validate.IsValidPassword(newPassword));*/
            Console.WriteLine("Confirm Password: ");
            confirmPassword = Console.ReadLine()!;
            while (confirmPassword != newPassword)
            {
                Console.WriteLine("Password Does Not Match. Reenter: ");
                confirmPassword = Console.ReadLine()!;
            }


            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();
                long userID = activeUser.ID;
                string updatePasswordQuery = "UPDATE newww_users SET Password = @newPassword WHERE ID =" + userID;

                using (MySqlCommand cmd = new MySqlCommand(updatePasswordQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    int rowsAffected = cmd.ExecuteNonQuery();

                }
            }

            return "Password Changed Succesfully!";

        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(numCommand, activeUser, connectionstring);
        }
        else
        {
            return "Invalid Command";
        }
    }
}

public class EmailReset : IIHandler
{
    private IIHandler _nextHandler;

    public IIHandler SetNext(IIHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(int numCommand, User activeUser, string connectionstring)
    {
        if (numCommand == 2)
        {
            string newEmail;

            Console.WriteLine("Enter New Email: ");
            newEmail = Console.ReadLine()!;
            /*do{
                Console.WriteLine("Invalid Email. Reenter Your new Email: ");
                newEmail = Console.ReadLine();
            }while(!Validate.IsValidEmail(newEmail));*/

            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();
                long userID = activeUser.ID;
                string updateEmailQuery = "UPDATE newww_users SET Email = @newEmail WHERE ID =" + userID;

                using (MySqlCommand cmd = new MySqlCommand(updateEmailQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@newEmail", newEmail);
                    int rowsAffected = cmd.ExecuteNonQuery();

                }
            }

            return "Email Changed Succesfully!";

        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(numCommand, activeUser, connectionstring);
        }
        else
        {
            return "Invalid Command";
        }
    }
}

public class PhoneNumberReset : IIHandler
{
    private IIHandler _nextHandler;

    public IIHandler SetNext(IIHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public string HandleRequest(int numCommand, User activeUser, string connectionstring)
    {
        if (numCommand == 3)
        {
            string newPhoneNumber;

            Console.WriteLine("Enter New PhoneNumber: ");
            newPhoneNumber = Console.ReadLine()!;
            /*do{
                Console.WriteLine("Invalid PhoneNumber. Reenter Your new PhoneNumber: ");
                newPhoneNumber = Console.ReadLine();
            }while(!Validate.IsValidPhoneNumber(newPhoneNumber));*/

            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();
                long userID = activeUser.ID;

                string updatePhoneNumberQuery = "UPDATE newww_users SET PhoneNumber = @newPhoneNumber WHERE ID =" + userID;

                using (MySqlCommand cmd = new MySqlCommand(updatePhoneNumberQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return "PhoneNumber Changed Succesfully!";

        }
        else if (_nextHandler != null)
        {
            return _nextHandler.HandleRequest(numCommand, activeUser, connectionstring);
        }
        else
        {
            return "Invalid Command";
        }
    }
}


public static class UpdaateUserInformation
{
    public static void UserInput(IIHandler passwordReset, User activeUser, string connectionstring)
    {
        string answer, enteraction;
        Console.WriteLine("Do You Want To Change Your Account Information? Y/N");
        answer = Console.ReadLine()!;
        while (answer == "Y" || answer == "y")
        {
            Console.WriteLine("Choose What You Want To Change:\n1-Change Password\n2-Change Email\n3-Change PhoneNumber");
            enteraction = Console.ReadLine()!;
            int command;
            command = Convert.ToInt32(enteraction);
            Console.WriteLine(passwordReset.HandleRequest(command, activeUser, connectionstring));
            Console.WriteLine("Do You Want To Change Your Account Information? Y/N");
            answer = Console.ReadLine()!;
        }
    }

}