abstract public class Car
{
    public double speed, Ratio;
    double[] dists = new double[100];
    public int AirConditioner;
    static double dist(double x1, double y1, double x2, double y2)
    {
        double deltaX = x2 - x1;
        double deltaY = y2 - y1;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
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


    public void generateOptions(Location start, Location destination, PaymentSetter paymentSetter)
    {

        Location[] arr = new Location[10000];
        DriverLocationGenerator dlg = new DriverLocationGenerator();


        double x = start.x, y = start.y;


        for (int i = 0; i < 100; i++)
        {

            arr[i] = dlg.GenerateRandomLocation();

            dists[i] = dist(x, y, arr[i].x, arr[i].y);

        }

        Array.Sort(dists);
        displayOptions(start, destination, paymentSetter);
    }

    public void displayOptions(Location start, Location destination, PaymentSetter paymentSetter)
    {
        Console.WriteLine("here's the available drivers\n");

        double D = dist(start.x, start.y, destination.x, destination.y);
        int[] pricearr = new int[10];

        for (int i = 0; i < 10; i++)
        {

            Console.Write(i + 1);

            Console.Write(") ");

            Console.Write((int)(dists[i] / speed));

            Console.Write("mins, Price : ");

            Random rand = new Random();

            long ran = rand.NextInt64() % 100;

            Console.Write((int)(D * Ratio + ran));
            pricearr[i] = (int)(D * Ratio + ran);
            Console.Write(" pounds\n");
        }

        int choice;
        do
        {
            choice = Convert.ToInt32(Console.Read());
        } while (!(choice <= 10 && choice >= 1));
        choice--;

        countDown(start, destination, pricearr[choice], choice, paymentSetter);
    }

    public void countDown(Location start, Location destination, int amount, int choice, PaymentSetter paymentSetter)
    {

        Console.WriteLine("\nDriver on his way\n");

        //put all variable declaration up
        //subtract fee from credict
        //class for invalid input, 
        //start some count down, with the option to cancel with a fee, or just if countdown
        //finished, write "Done!"
        int countdown = (int)dists[choice] / (int)speed;

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
        paymentSetter.ProcessPayment(amount);
    }
}


public class Ride : Car
{

    public Ride()

    {
        AirConditioner = 0;
        speed = 4.0;
        Ratio = 2 + speed / 10 + AirConditioner;
    }




}
public class RideAC : Car
{

    public RideAC()

    {
        AirConditioner = 5;
        speed = 6.0;
        Ratio = 2 + speed / 10 + AirConditioner;
    }


}
public class Moto : Car
{

    public Moto()

    {
        speed = 5.0;
        Ratio = 1.5;
    }


}
public class Freight : Car
{
    public Freight()

    {
        AirConditioner = 3;
        speed = 3.0;
        Ratio = 9 + speed / 10 + AirConditioner;
    }


}

