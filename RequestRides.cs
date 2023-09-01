using System;
public class RequestRide
{
    string answer;
    double x2,y2,x1,y1,distance=0 ,TargetIdx=0,CurrentIdx=0;
    double[] Locationx ={0 , 30 , 32 , 26  , 20, 16};
    double[] Locationy ={0 , 32 , 34 , 29 , 19 , 14};
    public void HandleRide(PaymentSetter paymentSetter)
    {

        Console.WriteLine("Enter your Target Location: 1-Egyptian Museum\n2-Cairo Tower\n3-Khan el-Khalili Market\n4-Salah El-Din Citadel\nCairo Opera House" );
        TargetIdx=Convert.ToDouble(Console.ReadLine());

        x2=Locationx[(int)TargetIdx];
        y2=Locationy[(int)TargetIdx];
        Location destination = new Location(x2, y2);

        Console.WriteLine("Enter your Current Location: 1-Egyptian Museum\n2-Cairo Tower\n3-Khan el-Khalili Market\n4-Salah El-Din Citadel\nCairo Opera House" );
        CurrentIdx=Convert.ToDouble(Console.ReadLine());

        x1=Locationx[(int)CurrentIdx];
        y1=Locationy[(int)CurrentIdx];
        Location current = new Location(x1, y1);

        Console.WriteLine("Enter the Car type you would like to Order :\n 1-Ride (Without Air conditionar) \n 2-RideAC (With Air conditionar) \n 3-Moto \n 4-Freight");
        answer = Console.ReadLine()!;
        RideType(paymentSetter);
    }
    public double DistanceCalc(double x1, double x2, double y1, double y2)
    {
        double _x = x1 - x2;
        double _y = y1 - y2;
        return distance = Math.Sqrt((_x * _x) + (_y * _y));
    }
    public void RideType(PaymentSetter paymentSetter)
    {

        if (answer == "Ride")
        {
            Ride cartype = new Ride();

            Location fst = new Location(x1, y1);
            Location snd = new Location(x2, y2);

            cartype.generateOptions(fst, snd, paymentSetter);
        }
        else if (answer == "RideAC")
        {
            Car cartype = new RideAC();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination, paymentSetter);
        }
        else if (answer == "Freight")
        {
            Car cartype = new Freight();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination, paymentSetter);
        }
        else
        {
            Car cartype = new Moto();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination, paymentSetter);
        }
    }
}