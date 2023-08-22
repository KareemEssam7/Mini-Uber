using System.Diagnostics.SymbolStore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;
using System.Threading;
using System.Reflection.Metadata.Ecma335;

public class Car
{
    public double speed, Ratio;

    public int AirConditioner;
    public Car()
    {


        speed = 1;
        Ratio = 1;
    }

    public class DriverLocationGenerator
    {
        private Random random;

        public DriverLocationGenerator()
        {
            random = new Random();
        }

        public Location GenerateRandomLocation()
        {
            // Define the latitude and longitude range within your desired boundaries
            double minLatitude = -90.0;
            double maxLatitude = 90.0;
            double minLongitude = -180.0;
            double maxLongitude = 180.0;

            // Generate random latitude and longitude within the defined range
            double latitude = random.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
            double longitude = random.NextDouble() * (maxLongitude - minLongitude) + minLongitude;

            return new Location(latitude, longitude);
        }
    }

    public void RequestRide(Location start, Location destination)
    {
        // choose ride type (require class for types)

        // generate random drivers locations
        // run dij to see nearest driver location
        // estimate time and (money based on ride type)
        // have an option to cancel while waiting
        // 

        Console.WriteLine(speed);

        static double dist(double x1, double y1, double x2, double y2)
        {

            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        Location[] arr = new Location[10000];
        DriverLocationGenerator dlg = new DriverLocationGenerator();

        // Dijkstra d = new Dijkstra();

        double x = start.x, y = start.y;
        // d.Init(10000);

        double[] dists = new double[100];
        for (int i = 0; i < 100; i++)
        {

            arr[i] = dlg.GenerateRandomLocation();
            //Console.WriteLine(arr[i].x); Console.WriteLine('\n');

            dists[i] = dist(x, y, arr[i].x, arr[i].y);

            // d.Edge(1, i + 1, 8);
        }

        //d.Run(1);

        //   d.dists.Sort();

        Array.Sort(dists);

        Console.WriteLine("here's the available drivers\n");

        //this should be abstract, should have the real values base on ride types

        double D = dist(start.x, start.y, destination.x, destination.y);

        for (int i = 0; i < 10; i++)
        {

            Console.Write(i + 1);

            Console.Write(") ");

            

            Console.Write((int)(dists[i] / speed));

            Console.Write("mins, Price : ");
            
            Random rand = new Random();

            long ran = rand.NextInt64() % 100;

            Console.Write((int)(D * Ratio + ran));

            Console.Write(" pounds\n");
        }

        int ch;

        ch = Convert.ToInt32(Console.Read());

        Console.WriteLine("\nDriver on his way\n");

        ch--;

        //start some count down, with the option to cancel with a fee, or just if countdown
        //finished, write "Done!"
        int countdown = (int)dists[ch] / (int)speed;


        while (countdown >= 0)
        {
            Console.Clear();
            Console.WriteLine("Type 1 to cancel\nEstimated driver arrival time: " + countdown);
            countdown--;

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.KeyChar == '1')
                {
                    Console.WriteLine("Ride cancelled, 10 pounds fee");

                    // Logic to subtract 10 pounds from credit card
                    // ...  

                    return;
                }
            }

            Thread.Sleep(1000); // Delay for 1 second
        }



        Console.WriteLine("Done!");
    }
}

public class Ride : Car
{

//    static double speed, Ratio;

 //   static int AirConditioner;

    public Ride()
        : base()
    {
        AirConditioner = 0;
        speed = 4.0;
        Ratio = 2 + speed / 10 + AirConditioner;
    }




}
public class RideAC : Car
{

    
   // static double speed, Ratio;
    //static int AirConditioner;
    public RideAC()
        : base()
    {
        AirConditioner = 5;
        speed = 6.0;
        Ratio = 2 + speed / 10 + AirConditioner;
    }


}
public class Moto : Car
{

    //static double speed, Ratio;

    public Moto()
        : base()
    {
        speed = 5.0;
        Ratio = 1.5;
    }


}
public class Freight : Car
{

    static double speed, Ratio;
    static int AirConditioner;

    public Freight()
        : base()
    {
        AirConditioner = 3;
        speed = 3.0;
        Ratio = 9 + speed / 10 + AirConditioner;
    }


}
