public class CreditExpiry
{

    public int Year { get; set; }
    public int Month { get; set; }

    public CreditExpiry(int year, int month)
    {
        month = Month;
        year = Year;
    }
}


public class CreditInfo
{
    public string CreditName { get; set; }
    public string CreditNumber { get; set; }
    public CreditExpiry monthYear { get; set; }
    public string CreditCVV { get; set; }
    public double CreditBalance { get; set; }

    public CreditInfo(string creditName, string creditNumber, CreditExpiry creditExpiry, string creditCVV, double creditBalance)
    {
        CreditName = creditName;
        CreditNumber = creditNumber;
        monthYear = creditExpiry;
        CreditCVV = creditCVV;
        CreditBalance = creditBalance;
    }
    static bool ValidateExpirationDate(int expMonth, int expYear)
    {
        int currentYear = DateTime.Now.Year % 100; // Get last two digits of current year
        int currentMonth = DateTime.Now.Month;

        if (expYear < currentYear || (expYear == currentYear && expMonth < currentMonth))
        {
            return false; // Card is expired
        }
        return true;
    }

    static bool ValidateCVV(string cvv)
    {
        if (cvv.Length < 3 || cvv.Length > 4 || !int.TryParse(cvv, out _))
        {
            return false; // CVV must be 3 or 4 digits
        }

        return true;
    }

    static bool ValidateCreditCardNumber(string cardNumber)
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