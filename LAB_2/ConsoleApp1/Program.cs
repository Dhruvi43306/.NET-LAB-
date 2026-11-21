using ConsoleApp1.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Faculty f1 = new Faculty();
            //f1.GetFacultyDetails();
            //f1.DisplayFacultyDetails();

            //Employee e1 = new Employee();
            //e1.GetEmpDetails();
            //e1.DisplayEmpDetails();

            //Cube c1 = new Cube();
            //c1.getVolume();
            //c1.setVolume();

            //Console.Write("Enter Model = ");
            //string model = Console.ReadLine();

            //Console.Write("Enter Year = ");
            //int year = Convert.ToInt32(Console.ReadLine());

            //Console.Write("Enter FuelType = ");
            //string FuelType = Console.ReadLine();

            //Console.Write("Enter Horsepower = ");
            //string Horsepower = Console.ReadLine();

            //Car c1 = new Car(model,year,FuelType,Horsepower);
            //c1.Display();

            //Item[] item = new Item[2];
            //for(int i = 0; i < item.Length; i++)
            //{
            //    Console.WriteLine("\nItem " + (i + 1));

            //    Console.Write("Enter Item Code:");
            //    int code = int.Parse(Console.ReadLine());

            //    Console.Write("enter Item Name:");
            //    string iname = Console.ReadLine();


            //    Console.Write("Enter stock Quantity:");
            //    int sqantity = int.Parse(Console.ReadLine());

            //    item[i] = new Item(code, iname, sqantity);
            //}

            //for(int i = 0; i < item.Length; i++)
            //{
            //    item[i].Display();
            //}

            Patient[] p1 = new Patient[2];
            for(int i = 0; i < p1.Length; i++)
            {
                Console.WriteLine("\nPatient " + (i + 1));
                Console.Write("Enter Patient Id = ");
                int Patient_ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Patient Name = ");
                string Name = Console.ReadLine();
                Console.Write("Enter a Patient Age = ");
                int Age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Patient Disease = ");
                int Disease = Convert.ToInt32(Console.ReadLine());

                p1[i] = new Patient(Patient_ID, Name, Age, Disease);
            }

            for(int i = 0; i < p1.Length; i++)
            {
                p1[i].Display();
            }
        }
    }
}
