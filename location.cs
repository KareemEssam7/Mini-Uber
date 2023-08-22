public class Location
{
    public double x { get; set; }
    public double y { get; set; }

    public Location(double latitude, double longitude)
    {
        x = latitude;
        y = longitude;
    }
}
