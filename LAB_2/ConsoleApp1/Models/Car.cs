using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    internal class Car
    {
        string model;
        int year;
        string FuelType;
        string Horsepower;

        public Car(string model, int year, string FuelType, string Horsepower)
        {
            this.model = model;
            this.FuelType = FuelType;
            this.Horsepower = Horsepower;

        }

        public void Display()
        {
            Console.WriteLine("\n===== Display details of premium sports cars ======");
            Console.WriteLine("Model = " + model);
            Console.WriteLine("FuelType = " + FuelType);
            Console.WriteLine("Hoursepower = " + Horsepower);
        }
    }
}
