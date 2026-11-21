using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    internal class Patient
    {
        int Patient_ID;
        string Name;
        int Age;
        int Disease;

        public Patient(int Patient_ID,string Name,int Age,int Disease)
        {
            this.Patient_ID = Patient_ID;
            this.Name = Name;
            this.Age = Age;
            this.Disease = Disease;
        }

        public void Display()
        {
            Console.WriteLine($"\nPatient ID = {Patient_ID}");
            Console.WriteLine($"Patient Name = {Name}");
            Console.WriteLine($"Patient Age = {Age}");
            Console.WriteLine($"PAtient Disease = {Disease}");

        }
    }
}
