using System;
using System.Data;
using System.Data.SqlTypes;
using Org.BouncyCastle.Cms;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata.Ecma335;


public class RequestRide
{
    string answer;
    double x2,y2,x1,y1,money=0,distance=0;
    public void HandleRide()
    {
        
        Console.WriteLine("Enter your Target Location as X-axis , Y-axis :" );
        x2=Convert.ToInt32(Console.ReadLine());
        y2=Convert.ToInt32(Console.ReadLine());
        Location destination=new Location(x2,y2);

        Console.WriteLine("Enter your Current Location as X-axis , Y-axis :" );
        x1=Convert.ToInt32(Console.ReadLine());
        y1=Convert.ToInt32(Console.ReadLine());
        Location current =new Location(x1,y1);    

        Console.WriteLine("Enter the Car type you would like to Order :\n 1-Ride (Without Air conditionar) \n 2-RideAC (With Air conditionar) \n 3-Moto \n 4-Freight");
        answer=Console.ReadLine()!;     
        RideType();
    }       
    public double DistanceCalc(double x1,double x2,double y1,double y2)
    {
        double _x=x1-x2;
        double _y=y1-y2;
        return distance=Math.Sqrt((_x*_x )+(_y*_y ));
    }
    public void RideType()
    {

        if(answer=="Ride")
        {
            Ride cartype = new Ride();
            
            Location fst = new Location(x1, y1);
            Location snd = new Location(x2, y2);

            cartype.generateOptions(fst, snd);
        }
        else if(answer=="RideAC")
        {
            Car cartype=new RideAC();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination);
        }
        else if(answer=="Freight")
        {
            Car cartype= new Freight();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination);
        }
        else
        {
            Car cartype= new Moto();
            Location start = new Location(x1, y1);
            Location destination = new Location(x2, y2);

            cartype.generateOptions(start, destination);
        }
    }    
}