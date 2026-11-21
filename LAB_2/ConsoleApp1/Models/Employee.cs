using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApp1.Models
{
    internal class Employee
    {
        int Emp_ID;
        string Name;
        string Department;
        string Designation;
        int Salary;

        public void GetEmpDetails()
        {
            Console.Write("Enter a Employee Id = ");
             Emp_ID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Name: ");
            Name = Console.ReadLine();

            Console.Write("Enter Department: ");
            Department = Console.ReadLine();

            Console.Write("Enter Designation: ");
            Designation = Console.ReadLine();

            Console.Write("Enter Employee Salary: ");
            Salary = Convert.ToInt32(Console.ReadLine());
        }

        public void DisplayEmpDetails()
        {
            Console.WriteLine("\nEmployee Details");
            Console.WriteLine("ID: " + Emp_ID);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("Designation: " + Designation);
            Console.WriteLine("salary: " + Salary);
        }
    }
}
