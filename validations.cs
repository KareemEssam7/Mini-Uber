using System;
using System.Text.RegularExpressions;
static class Validate
{
    public static bool ValidateName(string name)
    {
        // Check if the name is not empty or null
        if (string.IsNullOrEmpty(name))
            return false;

        // Check if the name contains only letters and spaces
        foreach (char c in name)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                return false;
        }
        return true;
    }

    public static bool IsValidEmail(string email)
    {
        // Regular expression pattern for basic email validation
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, pattern);
    }
    public static bool IsValidPassword(string password)
    {
        // Define password criteria
        int minLength = 8;
        bool hasUppercase = false;
        bool hasLowercase = false;
        bool hasDigit = false;
        bool hasSpecialChar = false;
        string specialChars = @"!@#$%^&*()-_=+[]{}|;:',.<>?/";

        if (password.Length < minLength)
            return false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
                hasUppercase = true;
            else if (char.IsLower(c))
                hasLowercase = true;
            else if (char.IsDigit(c))
                hasDigit = true;
            else if (specialChars.Contains(c))
                hasSpecialChar = true;
        }

        return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
    }
    public static bool ValidateEgyptPhoneNumber(string phoneNumber)
    {
        string pattern = @"^(010|011|012|015)\d{8}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    public static bool ValidateExpirationDate(int expMonth, int expYear)
    {
        int currentYear = DateTime.Now.Year % 100; // Get last two digits of current year
        int currentMonth = DateTime.Now.Month;

        if (expYear < currentYear || (expYear == currentYear && expMonth < currentMonth))
        {
            return false; // Card is expired
        }
        return true;
    }

    public static bool ValidateCVV(string cvv)
    {
        if (cvv.Length < 3 || cvv.Length > 4 || !int.TryParse(cvv, out _))
        {
            return false; // CVV must be 3 or 4 digits
        }

        return true;
    }

    public static bool ValidateCreditCardNumber(string cardNumber)
    {
        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(cardNumber[i].ToString());

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }
}