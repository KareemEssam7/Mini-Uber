public class Car
{
    public string Brand { get; }
    public string Model { get; }
    public string LicensePlate { get; }
    public int Year { get; }
    public int Capacity { get; }

    public bool AirConditioner { get; }
    public Car(string brand, string model, string licensePlate, int year, bool airconditioner)
    {
        Brand = brand;
        Model = model;
        LicensePlate = licensePlate;
        Year = year;
        AirConditioner = airconditioner;
    }

    // public override string ToString()
    // {
    //     return $"{Year} {Make} {Model} ({LicensePlate}), Capacity: {Capacity}";
    // }
}