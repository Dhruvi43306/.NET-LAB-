using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    internal class Cube
    {
        int side;

        public void getVolume()
        {
            Console.Write("Enter a side = ");
            side = Convert.ToInt32(Console.ReadLine());
        }
        public void setVolume()
        {
            Console.WriteLine($"Volume = {side*side*side}");
        }

    }
}
