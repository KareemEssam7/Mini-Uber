public interface IPaymentStrategy
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public string CreditName { get; set; }
    public string CreditNumber { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string CreditCVV { get; set; }
    public double CreditBalance { get; set; }

    public CreditCardPayment(string creditName, string creditNumber, int month, int year, string creditCVV, double creditBalance)
    {
        CreditName = creditName;
        CreditNumber = creditNumber;
        Month = month;
        Year = year;
        CreditCVV = creditCVV;
        CreditBalance = creditBalance;
    }
    public void ProcessPayment(double amount)
    {
        //Console.WriteLine($"Processing credit card payment of ${amount} using card number {cardNumber}.");
        // Actual payment processing logic here
    }
    public void registerCreditInfo()
    {
        do
        {
            Console.WriteLine("enter credit name: ");
            CreditName = Console.ReadLine()!;
        } while (!Validate.ValidateName(CreditName));

        do
        {
            Console.WriteLine("enter credit number: ");
            CreditNumber = Console.ReadLine()!;
        } while (!Validate.ValidateCreditCardNumber(CreditNumber));

        do
        {
            Console.WriteLine("enter expiry month and year: ");
            Month = Convert.ToInt32(Console.ReadLine());
            Year = Convert.ToInt32(Console.ReadLine());
        } while (!Validate.ValidateExpirationDate(Month, Year));

        do
        {
            Console.WriteLine("enter CVV: ");
            CreditCVV = Console.ReadLine()!;
        } while (!Validate.ValidateCVV(CreditCVV));
    }

}
public static class CreditMethods
{
    public static string GetUserPaymentMethod()
    {
        Console.WriteLine("Enter your desired payment method\n 1: CreditCard\n 2: PayPal\n 3: Cash");
        string interaction = Console.ReadLine()!;
        return interaction;
    }

}

public class PayPalPayment : IPaymentStrategy
{
    private string email;

    public PayPalPayment(string email)
    {
        this.email = email;
    }

    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing PayPal payment of ${amount} using email {email}.");
        // Actual payment processing logic here
    }
}

public class CashPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Please pay the driver in person");
    }

}
