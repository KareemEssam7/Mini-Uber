using System;
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Location test= new Location(1.2,3.2);
           Rider one= new  Rider("Kareem","Essam","kareemessam2101@gmail.com","testtube","01092217001",test);
           Console.WriteLine(one.FirstName);
           Console.WriteLine(one.LastName);
           Console.WriteLine(one.Email);
           Console.WriteLine(one.Password);
           Console.WriteLine(one.PhoneNumber);
           Console.WriteLine(one.CurrentLocation.Latitude);
           Console.WriteLine(one.CurrentLocation.Longitude);
        }
    }
    
}