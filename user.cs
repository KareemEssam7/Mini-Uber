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
    public void depositCash(double cash)
    {
        CreditInfo.CreditBalance += cash;
    }
    public void checkCreditInfo()
    {
        Console.WriteLine(CreditInfo.CreditName);
        Console.WriteLine(CreditInfo.CreditNumber);
        Console.WriteLine(CreditInfo.monthYear.Month + "/" + CreditInfo.monthYear.Year);
        Console.WriteLine(CreditInfo.CreditCVV);
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