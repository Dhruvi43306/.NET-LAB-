using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    internal class Faculty
    {
        public int id;
        public string name;
        public int age;
        public double weight;
        public double height;

        public void GetFacultyDetails()
        {
            Console.Write("Enter Faculty ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Faculty Name: ");
            name = Console.ReadLine();

            Console.WriteLine("Enter Faculty Age: ");
            age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Faculty Weight: ");
            weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Faculty Height: ");
            height = Convert.ToDouble(Console.ReadLine());

        }
        public void DisplayFacultyDetails()
        {
            // Display the stored faculty details.
            Console.WriteLine("\nFaculty Details");
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Weight: " + weight + " kg");
            Console.WriteLine("Height: " + height + " cm");
        }

    }
}
