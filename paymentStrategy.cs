public interface IPaymentStrategy
{
    void ProcessPayment(int amount);
}

public class PaymentSetter
{
    IPaymentStrategy paymentStrategy;

    public void setPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        this.paymentStrategy = paymentStrategy;
    }

    public void ProcessPayment(int amount)
    {
        this.paymentStrategy.ProcessPayment(amount);
    }
}

public class CreditCardPayment : IPaymentStrategy
{
    public string CreditName { get; set; }
    public string CreditNumber { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string CreditCVV { get; set; }

    public CreditCardPayment(string creditName, string creditNumber, int month, int year, string creditCVV)
    {
        CreditName = creditName;
        CreditNumber = creditNumber;
        Month = month;
        Year = year;
        CreditCVV = creditCVV;
    }
    public void ProcessPayment(int amount)
    {
        Console.WriteLine($"Processing credit card payment of ${amount} using email {CreditNumber}.");
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
public static class PaymentMethods
{
    public static void GetUserPaymentMethod(PaymentSetter paymentSetter, CreditCardPayment activeCredit, PaymentInfoToStore paymentInfoToStore)
    {
        do
        {
            Console.WriteLine("Enter your desired payment method\n 1: CreditCard\n 2: PayPal\n 3: Cash");
            string interaction = Console.ReadLine()!;

            if (interaction == "creditcard")
            {
                activeCredit.registerCreditInfo();
                paymentSetter.setPaymentStrategy(new CreditCardPayment(activeCredit.CreditName, activeCredit.CreditNumber, activeCredit.Month, activeCredit.Year, activeCredit.CreditCVV));
                paymentInfoToStore.paymentType = interaction;
                break;
            }
            else if (interaction == "paypal")
            {
                Console.WriteLine("Enter paypal Email");
                paymentInfoToStore.PayPalEmail = Console.ReadLine()!;
                paymentSetter.setPaymentStrategy(new PayPalPayment(paymentInfoToStore.PayPalEmail));
                paymentInfoToStore.paymentType = interaction;
                break;
            }
            else if (interaction == "cash")
            {
                Console.WriteLine("Thank you");
                paymentSetter.setPaymentStrategy(new CashPayment());
                paymentInfoToStore.paymentType = interaction;
                break;
            }
            else
            {
                Console.WriteLine("Invalid Payment Type");
            }
        } while (true);
    }
}

public class PayPalPayment : IPaymentStrategy
{
    private string email;

    public PayPalPayment(string email)
    {
        this.email = email;
    }

    public void ProcessPayment(int amount)
    {
        Console.WriteLine($"Processing PayPal payment of ${amount} using email {email}.");
    }
}

public class CashPayment : IPaymentStrategy
{
    public void ProcessPayment(int amount)
    {
        Console.WriteLine("Thank you for choosing Uber-Mini");
    }
}
public class PaymentInfoToStore
{
    public string paymentType { get; set; }
    public string PayPalEmail { get; set; }
}