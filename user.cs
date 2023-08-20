using System;
using System.Data;
using System.Data.SqlTypes;


public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public User(string firstName, string lastName, string email, string password, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}

public class Rider : User
{
    public Location CurrentLocation { get; set; }
    public CreditInfo CreditInfo { get; set; }
    public Rider(string firstName, string lastName, string email, string password, string phoneNumber, Location currentLocation, CreditInfo creditInfo)
        : base(firstName, lastName, email, password, phoneNumber)
    {
        CurrentLocation = currentLocation;
        CreditInfo = creditInfo;
    }
    public void depositCash()
    {
        double cash;
        cash = Convert.ToDouble(Console.ReadLine());
        CreditInfo.CreditBalance += cash;
    }
    public void checkCreditInfo()
    {
        Console.WriteLine(CreditInfo.CreditName);
        Console.WriteLine(CreditInfo.CreditNumber);
        Console.WriteLine(CreditInfo.monthYear.Month + "/" + CreditInfo.monthYear.Year);
        Console.WriteLine(CreditInfo.CreditCVV);
    }

    public void registerUser()
    {
        do
        {
            Console.WriteLine("enter first name: ");
            FirstName = Console.ReadLine()!;
        } while (!Validate.ValidateName(FirstName));

        do
        {
            Console.WriteLine("enter last name: ");
            LastName = Console.ReadLine()!;
        } while (!Validate.ValidateName(LastName));

        do
        {
            Console.WriteLine("enter Email: ");
            Email = Console.ReadLine()!;
        } while (!Validate.IsValidEmail(Email));

        do
        {
            Console.WriteLine("enter password(must include: lowercase, uppercase, number, special char, more than 8 chars): ");
            Password = Console.ReadLine()!;
        } while (!Validate.IsValidPassword(Password));

        string tmppass;
        do
        {
            Console.WriteLine("Confirm password: ");
            tmppass = Console.ReadLine()!;
        } while (tmppass != Password);

        do
        {
            Console.WriteLine("enter phone number: ");
            PhoneNumber = Console.ReadLine()!;
        } while (!Validate.ValidateEgyptPhoneNumber(PhoneNumber));
    }



    // public void RequestRide(Location destination)
    // {
    //     Console.WriteLine($"{FirstName} {LastName} is requesting a ride to {destination}.");
    //     // Logic to request a ride goes here
    // }

    // public void CancelRide()
    // {
    //     Console.WriteLine($"{FirstName} {LastName} has canceled the ride.");
    //     // Logic to cancel a ride goes here
    // }
}

public class Driver : User
{
    public Car DriverCar { get; set; }

    public Driver(string firstName, string lastName, string email, string password, string phoneNumber, Car driverCar)
        : base(firstName, lastName, email, password, phoneNumber)
    {
        DriverCar = driverCar;
    }

    // public void AcceptRideRequest(Rider rider, Location destination)
    // {
    //     Console.WriteLine($"{FirstName} {LastName} is accepting a ride request from {rider.FirstName} to {destination}.");
    //     // Logic to accept a ride request goes here
    // }

    // public void StartRide()
    // {
    //     Console.WriteLine($"{FirstName} {LastName} has started the ride.");
    //     // Logic to start a ride goes here
    // }

    // public void EndRide()
    // {
    //     Console.WriteLine($"{FirstName} {LastName} has ended the ride.");
    //     // Logic to end a ride goes here
    // }
}