using System;

namespace DotNetOutdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null) 
              args = new string[] { };
            Console.WriteLine(string.Join(" ", args));
        }
    }
}