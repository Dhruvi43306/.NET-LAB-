

namespace ConsoleApp2.Models
{
    internal class Student
    {
        public int Id;
        public string Name;
        public int Age;
    }
}



//using ConsoleApp2.Models;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace ConsoleApp1
//{
//    internal class Program
//    {
//        // One shared collection is used by all static menu methods
//        //Static methods can directly access static fields.

//        // A static method can be called using the class name
//        // without creating an object of that class.

//        // An instance method belongs to an object and must be called
//        // through an object reference.

//        // The List is declared outside Main() but inside the Program class.
//        // Therefore, it is a class-level field, not a local variable.
//        // All static methods of this class can directly access this List.

//        // If we declare the List inside Main(), it becomes a local variable.
//        // Its scope is limited to the Main() method, so other methods
//        // cannot directly access it


//        // Approach A – Write methods outside Main()
//        //Each method has its own responsibility.
//        //We can call the same method from different methods of the class.
//        //We can understand and debug each operation separately.
//        //Main remains small and easy to understand.
//        static List<Student> students = new List<Student>();

//        static void Main(string[] args)
//        {
//            while (true)
//            {
//                Console.WriteLine("\n===== STUDENT MANAGEMENT SYSTEM =====");
//                Console.WriteLine("1. Add Student");
//                Console.WriteLine("2. Display Students");
//                Console.WriteLine("3. Search Student");
//                Console.WriteLine("4. Update Student");
//                Console.WriteLine("5. Delete Student");
//                Console.WriteLine("6. Exit");

//                Console.Write("\nEnter your choice: ");
//                int choice = Convert.ToInt32(Console.ReadLine());

//                switch (choice)
//                {
//                    case 1:
//                        AddStudent();
//                        break;

//                    case 2:
//                        DisplayStudents();
//                        break;

//                    case 3:
//                        SearchStudent();
//                        break;

//                    case 4:
//                        UpdateStudent();
//                        break;

//                    case 5:
//                        DeleteStudent();
//                        break;

//                    case 6:
//                        Console.WriteLine("Exiting application...");
//                        return;

//                    default:
//                        Console.WriteLine("Invalid choice!");
//                        break;
//                }
//            }
//        }

//        // 1. Add Student
//        static void AddStudent()
//        {
//            Student student = new Student();

//            Console.Write("\nEnter Student ID: ");
//            student.Id = Convert.ToInt32(Console.ReadLine());

//            // Check duplicate ID
//            if (FindStudentIndex(student.Id) != -1)
//            {
//                Console.WriteLine("Student ID already exists!");
//                return;
//            }

//            Console.Write("Enter Student Name: ");
//            student.Name = Console.ReadLine() ?? "";

//            Console.Write("Enter Student Age: ");
//            student.Age = Convert.ToInt32(Console.ReadLine());

//            students.Add(student);

//            Console.WriteLine("Student added successfully!");
//        }

//        // 2. Display Students
//        static void DisplayStudents()
//        {
//            if (students.Count == 0)
//            {
//                Console.WriteLine("\nNo student records available!");
//                return;
//            }

//            Console.WriteLine("\n===== STUDENT RECORDS =====");

//            foreach (Student student in students)
//            {
//                Console.WriteLine("-------------------------");
//                Console.WriteLine("ID   : " + student.Id);
//                Console.WriteLine("Name : " + student.Name);
//                Console.WriteLine("Age  : " + student.Age);
//            }
//        }

//        // 3. Search Student
//        static void SearchStudent()
//        {
//            Console.Write("\nEnter Student ID to Search: ");
//            int id = Convert.ToInt32(Console.ReadLine());

//            int index = FindStudentIndex(id);

//            if (index == -1)
//            {
//                Console.WriteLine("Student not found!");
//            }
//            else
//            {
//                Console.WriteLine("\nStudent Found!");
//                Console.WriteLine("ID   : " + students[index].Id);
//                Console.WriteLine("Name : " + students[index].Name);
//                Console.WriteLine("Age  : " + students[index].Age);
//            }
//        }

//        // 4. Update Student
//        static void UpdateStudent()
//        {
//            Console.Write("\nEnter Student ID to Update: ");
//            int id = Convert.ToInt32(Console.ReadLine());

//            int index = FindStudentIndex(id);

//            if (index == -1)
//            {
//                Console.WriteLine("Student not found!");
//                return;
//            }

//            Console.Write("Enter New Student Name: ");
//            students[index].Name = Console.ReadLine();

//            Console.Write("Enter New Student Age: ");
//            students[index].Age = Convert.ToInt32(Console.ReadLine());

//            Console.WriteLine("Student updated successfully!");
//        }

//        // 5. Delete Student
//        static void DeleteStudent()
//        {
//            Console.Write("\nEnter Student ID to Delete: ");
//            int id = Convert.ToInt32(Console.ReadLine());

//            int index = FindStudentIndex(id);

//            if (index == -1)
//            {
//                Console.WriteLine("Student not found!");
//            }
//            else
//            {
//                students.RemoveAt(index);

//                Console.WriteLine("Student deleted successfully!");
//            }
//        }

//        // Find Student using ID
//        static int FindStudentIndex(int id)
//        {
//            for (int i = 0; i < students.Count; i++)
//            {
//                if (students[i].Id == id)
//                {
//                    return i;
//                }
//            }

//            return -1;
//        }
//    }
//}


//2.Approach B – Write functions inside Main()
//Actually, C# allows this using something called a Local Function.

//using ConsoleApp2.Models;

//class Program
//{
//    static void Main(string[] args)
//    {
//        List<Student> students = new List<Student>();

//        void AddStudent()
//        {
//            students.Add(new Student());
//        }

//        void DisplayStudents()
//        {
//            foreach (Student student in students)
//            {
//                Console.WriteLine(student.Name);
//            }
//        }

//        AddStudent();
//        DisplayStudents();
//    }
//}

//The local functions can directly access the students variable declared inside Main().

//Why? Because local functions can access variables from their containing method.
//    Small operations used only within Main
//    Restricted to Main






