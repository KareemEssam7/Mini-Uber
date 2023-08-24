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

    public void registerCreditInfo(Rider rider)
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
            monthYear.Month = Convert.ToInt32(Console.ReadLine());
            monthYear.Year = Convert.ToInt32(Console.ReadLine());
        } while (!Validate.ValidateExpirationDate(monthYear.Month, monthYear.Year));

        do
        {
            Console.WriteLine("enter CVV: ");
            CreditCVV = Console.ReadLine()!;
        } while (!Validate.ValidateCVV(CreditCVV));
    }

}