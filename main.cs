using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    class person
    {
        public string Name
        {
            get;
            set;
        }
        public person(string name = "lion")
        {
            Name = name;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello please work again!");
            person guy = new person();
            Console.WriteLine(guy.Name);
        }
    }
}