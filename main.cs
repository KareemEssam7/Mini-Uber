using System;
using System.Runtime.InteropServices;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Location test = new Location(1.2, 3.2);
            CreditExpiry expdate = new CreditExpiry(09, 27);
            CreditInfo wallet1 = new CreditInfo("parlerler", "1010010101", expdate, "033", 0.0);
            Rider one = new Rider("Kareem", "Essam", "kareemessam2101@gmail.com", "testtube", "01092217001", test, wallet1);

            /*Console.WriteLine(one.FirstName);
            Console.WriteLine(one.LastName);
            Console.WriteLine(one.Email);
            Console.WriteLine(one.Password);
            Console.WriteLine(one.PhoneNumber);
            Console.WriteLine(one.CurrentLocation.Latitude);
            Console.WriteLine(one.CurrentLocation.Longitude);*/

            Console.WriteLine("Login or Register");
            //do the login/register logic

            Console.WriteLine("Select action");
            Console.WriteLine("1: Edit credit card info\n2:Check credit info\n3: Deposit money\n4: Check current funds");
            int action;
            char actionloop = 'y';
            do
            {
                action = Convert.ToInt32(Console.ReadLine());
                if (action == 1)
                {
                    wallet1.CreditName = Console.ReadLine();
                    wallet1.CreditNumber = Console.ReadLine();
                    wallet1.monthYear.Month = Convert.ToInt32(Console.ReadLine());
                    wallet1.monthYear.Year = Convert.ToInt32(Console.ReadLine());
                    wallet1.CreditCVV = Console.ReadLine();
                }
                else if (action == 2)
                {
                    one.checkCreditInfo();
                }
                else if (action == 3)
                {
                    double cash;
                    cash = Convert.ToDouble(Console.ReadLine());
                    one.depositCash(cash);
                }
                else if (action == 4)
                {
                    Console.WriteLine(one.CreditInfo.CreditBalance);
                }
                Console.WriteLine("would you like to do another action? type Y to continue");
                actionloop = Convert.ToChar(Console.ReadLine());
            } while (actionloop == 'y' || actionloop == 'Y');
        }
    }
}