# Lab 2 – Class, Objects, Array of Objects & Constructor

> **Course:** Building RESTful APIs with ASP.NET Core  
> **Lab topic:** Class, Objects, Array of Objects & Constructor  

---


# 2. Understand the problem before coding

Before opening Visual Studio, ask these questions:

1. **What real-world entity am I representing?** For example, a Faculty, Employee, Car, Item or Patient.
2. **What information belongs to that entity?** These become the fields.
3. **What operations must the program perform?** These become methods or constructor initialization.
4. **How many records are required?** One object, five objects, ten objects or a user-entered number?
5. **How should those objects be created?** By calling methods or by using a constructor?
6. **How will I access and display the records?** Through an object variable or an array index and loop?

A good developer does not begin by typing random statements. First, the developer understands the requirement and plans the objects and their responsibilities.

## The core relationship

```text
Class  ->  Object  ->  Multiple objects  ->  Array of objects
```

- A **class** defines the common structure.
- An **object** is one instance of that class.
- Multiple objects represent multiple individual records.
- An **array of objects** groups multiple object references under one array variable.

---

# 3. Class

## 3.1 What is a class?

A **class is a user-defined reference type that describes the data and behaviour that its objects can have.** It acts like a blueprint for creating objects.

For example, a college may have many faculty members. Each faculty member has an ID, name, age, weight and height. We do not need a different class for every faculty member because their structure is common.

We define one `Faculty` class and create as many Faculty objects as the application needs.

## 3.2 Real-world visualization

Think about an architect's building plan:

- The plan describes the structure of a house.
- The plan itself is not a completed physical house.
- Multiple houses can be built using the same plan.
- Each house can have its own address, colour and owner.

Similarly:

- `Faculty` is the class (blueprint).
- `faculty1` and `faculty2` are objects created from the class.
- Each object can store different faculty details.

## 3.3 Example class structure

```csharp
internal class Faculty
{
    public int Id;
    public string Name = "";
    public int Age;
    public double Weight;
    public double Height;

    public void DisplayFacultyDetails()
    {
        Console.WriteLine($"Faculty ID: {Id}");
        Console.WriteLine($"Faculty Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"Height: {Height}");
    }
}
```

In this example:

- `Faculty` is the class name.
- `Id`, `Name`, `Age`, `Weight` and `Height` are fields.
- `DisplayFacultyDetails()` is an instance method.

The manual does not prescribe the exact C# data types. `int` is suitable for ID and age in this simple example. `double` is a useful choice for measurements such as weight and height when decimal values may be required.

## 3.4 Why do we create a class?

Without a class, the information about one faculty member could be spread across many unrelated variables. A class groups related information and operations into one meaningful type.

For example, these values all describe one faculty member:

```text
Id       = 101
Name     = "Amit"
Age      = 35
Weight   = 68.5
Height   = 172.5
```

A `Faculty` class keeps these fields together as one logical entity.

## 3.5 Class versus object

| Class | Object |
|---|---|
| Defines a type or blueprint. | Is an instance of a class. |
| Describes which fields and methods objects have. | Holds actual instance data and can use instance methods. |
| One class can be used to create many objects. | Each object is an individual instance. |
| Example: `Faculty` | Example: `faculty1` |

---

# 4. Object

## 4.1 What is an object?

An **object is an instance of a class**. It is created at runtime and provides an individual instance through which we access the class's instance members.

## 4.2 Creating an object

```csharp
Faculty faculty1 = new Faculty();
```

Break this statement into parts:

| Part | Meaning |
|---|---|
| `Faculty` | The type of the variable. |
| `faculty1` | The variable that holds a reference to the object. |
| `new` | Creates an object. |
| `Faculty()` | Calls the constructor to initialize the new object. |

The variable `faculty1` is not the class itself. It holds a reference to the created Faculty object.

## 4.3 Accessing fields through an object

```csharp
Faculty faculty1 = new Faculty();

faculty1.Id = 101;
faculty1.Name = "Amit";
faculty1.Age = 35;
```

The dot operator (`.`) is used to access an accessible member through an object reference.

## 4.4 Why do we create multiple objects from one class?

Suppose a college has 50 faculty members. All faculty members have the same kind of information, but each person's values are different.

We should not create 50 different classes. One class is enough because the structure is common.

```csharp
Faculty faculty1 = new Faculty();
Faculty faculty2 = new Faculty();

faculty1.Id = 101;
faculty1.Name = "Amit";

faculty2.Id = 102;
faculty2.Name = "Priya";
```

Conceptually:

```text
Faculty class
   |
   +-- faculty1: Id = 101, Name = Amit
   |
   +-- faculty2: Id = 102, Name = Priya
```

Each object has its **own instance fields**. Updating `faculty1.Name` does not automatically change `faculty2.Name`.

## 4.5 Does one class mean one object?

No. One class can be used to create zero, one or many objects. The number of objects depends on the program's requirements.

---

# 5. Fields, local variables and parameters

These three terms are all related to variables, but their declaration locations, scopes and purposes are different.

## 5.1 Field

A **field** is a variable declared directly inside a class or struct, outside methods and constructors.

```csharp
class Student
{
    public int Id;       // Field
    public string Name = ""; // Field
}
```

An instance field stores data associated with a particular object. Each object has its own instance-field values.

## 5.2 Local variable

A **local variable** is declared inside a method, constructor or block. It can be used only within its applicable scope.

```csharp
void Calculate()
{
    int total = 10; // Local variable
    Console.WriteLine(total);
}
```

The variable `total` is not automatically available to another method.

## 5.3 Parameter

A **parameter** is declared inside a method or constructor's parentheses. It receives a value when the method or constructor is called.

```csharp
void SetStudentDetails(int id, string name)
{
    Id = id;
    Name = name;
}
```

Here, `id` and `name` are parameters. `Id` and `Name` are fields.

## 5.4 Field versus parameter

```csharp
class Student
{
    public int Id; // Field

    public void SetId(int id) // id is a parameter
    {
        Id = id;
    }
}
```

Execution:

1. The caller passes `101` to `SetId()`.
2. The parameter `id` receives `101`.
3. `Id = id` copies that value into the object's field.
4. The field retains the value as part of the object's state.

| Field | Parameter |
|---|---|
| Declared in the class body, outside methods/constructors. | Declared in a method or constructor parameter list. |
| Instance field belongs to an object. | Receives input for a particular method/constructor call. |
| Can be accessed by instance methods according to accessibility rules. | Normally usable only within the method/constructor body and related scope. |
| Example: `public int Id;` | Example: `int id` |

## 5.5 Why do we sometimes write `this.Id = Id`?

Consider:

```csharp
public int Id;

public void SetId(int Id)
{
    Id = Id;
}
```

The parameter name `Id` shadows the field name `Id`. Both references in `Id = Id` refer to the parameter, so the field is not updated.

Use `this` to explicitly refer to the current object's field:

```csharp
public void SetId(int Id)
{
    this.Id = Id;
}
```

- `this.Id` means the `Id` field of the current object.
- `Id` means the method parameter.

Another common style avoids the naming conflict by using camelCase parameters:

```csharp
public void SetId(int id)
{
    Id = id;
}
```

Both approaches are correct.

---

# 6. Instance methods and the `this` keyword

## 6.1 What is an instance method?

An instance method is a non-static method that is called through an object. It operates in the context of the object that invoked it.

```csharp
class Faculty
{
    public int Id;

    public void DisplayId()
    {
        Console.WriteLine(Id);
    }
}
```

Calling it:

```csharp
Faculty faculty1 = new Faculty();
faculty1.Id = 101;
faculty1.DisplayId();
```

## 6.2 How does one method know which object's data to access?

```csharp
Faculty faculty1 = new Faculty();
Faculty faculty2 = new Faculty();

faculty1.Id = 101;
faculty2.Id = 102;

faculty1.DisplayId();
faculty2.DisplayId();
```

The object reference before the dot identifies the receiver of each call:

- `faculty1.DisplayId()` operates on `faculty1`.
- `faculty2.DisplayId()` operates on `faculty2`.

Inside an instance method, `this` refers to the current object. The same method definition can therefore work with different objects in different calls.

## 6.3 Static versus instance (useful supporting concept)

This lab's central topics are classes, objects, constructors and object arrays. However, students also encounter `static` because the traditional Console App entry point `Main()` is static.

| Static member | Instance member |
|---|---|
| Belongs to the type itself. | Belongs to an object instance. |
| Accessed through the class name in the usual case. | Accessed through an object reference. |
| Has no `this` reference. | Has a `this` reference in an instance context. |
| Cannot directly access instance fields without an object. | Can directly access accessible instance and static members. |

Example:

```csharp
class Student
{
    public int Id;                    // Instance field
    public static string College = "DIET"; // Static field

    public void DisplayId()            // Instance method
    {
        Console.WriteLine(Id);
    }

    public static void DisplayCollege() // Static method
    {
        Console.WriteLine(College);
    }
}
```

Usage:

```csharp
Student student = new Student();
student.Id = 101;
student.DisplayId();
Student.DisplayCollege();
```

A static method cannot directly access an instance field because it does not have a current object. It can access an instance field when an object is supplied to it:

```csharp
public static void DisplayStudent(Student student)
{
    Console.WriteLine(student.Id);
}
```

**Remember:** `static` is not required just because a variable is a collection or because a program uses `Main()`. Static versus instance is about which type or object owns the member.

---

# 7. Constructors

## 7.1 What is a constructor?

A **constructor is a special class member that runs when an object is created.** It is commonly used to initialize the new object's fields.

Example:

```csharp
class Student
{
    public int Id;
    public string Name = "";

    public Student()
    {
        Id = 0;
        Name = "Unknown";
    }
}
```

When the program executes:

```csharp
Student student = new Student();
```

The constructor runs as part of creating the object.

## 7.2 Constructor characteristics

- The constructor name must match the class name.
- A constructor has no return type—not even `void`.
- It runs when an object is created.
- It can accept parameters.
- A class can have multiple constructors with different parameter lists (constructor overloading).
- A constructor can use access modifiers such as `public` or `private`.

Incorrect:

```csharp
public void Student() // This is a method, not a constructor
{
}
```

Correct:

```csharp
public Student()
{
}
```

## 7.3 Parameterless constructor

A parameterless constructor takes no parameters.

```csharp
class Student
{
    public int Id;
    public string Name = "";

    public Student()
    {
        Id = 0;
        Name = "Not assigned";
    }
}
```

Creation:

```csharp
Student student = new Student();
```

## 7.4 Parameterized constructor

A parameterized constructor receives values during object creation.

```csharp
class Student
{
    public int Id;
    public string Name;

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
```

Creation:

```csharp
Student student = new Student(101, "Amit");
```

The constructor receives the values and assigns them to the new object's fields.

## 7.5 Default constructor versus parameterless constructor

These terms are related but should not be carelessly treated as identical in every context:

- A **parameterless constructor** is any constructor with no parameters.
- A **default constructor** commonly refers to the parameterless constructor automatically supplied by the compiler when a class declares no instance constructors.
- If you declare your own constructor, the compiler does not also provide the automatic parameterless constructor. You must declare one yourself if you need it.

Example:

```csharp
class Student
{
    public int Id;
}
```

The compiler supplies a parameterless constructor because none is declared.

But:

```csharp
class Student
{
    public int Id;

    public Student(int id)
    {
        Id = id;
    }
}
```

Now this is valid:

```csharp
Student s1 = new Student(101);
```

But this is not valid unless you add a parameterless constructor:

```csharp
Student s2 = new Student();
```

## 7.6 Constructor versus normal method

| Constructor | Normal method |
|---|---|
| Name matches the class name. | Can have any valid method name. |
| Has no return type. | Has a return type or `void`. |
| Runs during object creation. | Runs when it is called. |
| Commonly initializes an object. | Performs an operation or returns a result. |
| Cannot be called like an ordinary instance method. | Called through an object or type, depending on whether it is instance or static. |

## 7.7 Constructor overloading

A class can have multiple constructors as long as their parameter lists differ.

```csharp
class Car
{
    public string Make;
    public string Model;
    public int Year;

    public Car()
    {
        Make = "Unknown";
        Model = "Unknown";
        Year = 0;
    }

    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }
}
```

Usage:

```csharp
Car car1 = new Car();
Car car2 = new Car("Toyota", "Camry", 2025);
```

The compiler selects the constructor whose parameter list matches the supplied arguments.

---

# 8. Array of objects

## 8.1 First understand an array

An **array is a fixed-size collection of elements of the same type.** Its elements are accessed by zero-based indexes.

```csharp
int[] numbers = new int[5];
```

This creates an array with five integer slots. Their indexes are `0` through `4`.

## 8.2 What is an array of objects?

An array of objects is an array whose elements are object references of a particular class type.

For example:

```csharp
Student[] students = new Student[5];
```

This creates an array with five Student-reference slots. It does **not** automatically create five Student objects.

For reference types, the slots are initially `null` until objects are assigned to them.

A simple illustration:

```text
students array
+---------+---------+---------+---------+---------+
| [0]     | [1]     | [2]     | [3]     | [4]     |
| null    | null    | null    | null    | null    |
+---------+---------+---------+---------+---------+
```

## 8.3 Create each object and assign it to an array slot

```csharp
Student[] students = new Student[3];

students[0] = new Student(101, "Amit");
students[1] = new Student(102, "Priya");
students[2] = new Student(103, "Rahul");
```

Now the array slots refer to three different Student objects.

```text
students
+----------------+----------------+----------------+
| [0]            | [1]            | [2]            |
| Student 101    | Student 102    | Student 103    |
| Amit           | Priya          | Rahul          |
+----------------+----------------+----------------+
```

## 8.4 Why do we need an array of objects?

Suppose a supermarket needs to store ten item records. Without an array, you might create ten separate variables:

```csharp
Item item1 = new Item(...);
Item item2 = new Item(...);
Item item3 = new Item(...);
```

This becomes repetitive and inconvenient to process.

An array allows all the item references to be managed through one variable and processed using a loop.

## 8.5 Access an object using an index

```csharp
Console.WriteLine(students[0].Name);
```

This means:

1. `students` refers to the array.
2. `[0]` selects the first array element.
3. `.Name` accesses the Name field of the Student object referenced by that element.

## 8.6 Use a loop to process every object

```csharp
for (int i = 0; i < students.Length; i++)
{
    Console.WriteLine(students[i].Id);
    Console.WriteLine(students[i].Name);
}
```

A `foreach` loop can also be used after every slot has been initialized:

```csharp
foreach (Student student in students)
{
    Console.WriteLine(student.Name);
}
```

## 8.7 Important distinction: array creation versus object creation

```csharp
Student[] students = new Student[5];
```

Creates one array object containing five reference slots.

```csharp
students[0] = new Student();
```

Creates one Student object and stores its reference in slot zero.

These are two separate object-creation operations.

## 8.8 Array properties and limitations

- Indexing starts at zero.
- The last valid index is `Length - 1`.
- `Length` reports the number of array slots.
- The array's size is fixed after it is created.
- Inserting or removing an item from the middle does not automatically resize an array.
- An object array can hold references to objects assignable to its declared element type.

**Scope note:** Lab 2 focuses on arrays of objects. Dynamically resizable collections such as `List<T>` are covered separately in Lab 3. Do not confuse an array of objects with a List.

---

# 9. How to organize the project

Lab 2 contains six independent exercises. For beginners, a clear setup is to keep each practical in its own Console App project, or to keep one solution with separate Console App projects for each exercise. This prevents unrelated `Main()` methods and duplicate class names from becoming confusing.

Example solution organization:

```text
Lab02_Class_Objects_Arrays.sln
│
├── 01_Faculty
│   ├── Program.cs
│   └── Models
│       └── Faculty.cs
│
├── 02_Employee
│   ├── Program.cs
│   └── Models
│       └── Employee.cs
│
├── 03_Cube
│   ├── Program.cs
│   └── Models
│       └── Cube.cs
│
├── 04_Car
│   ├── Program.cs
│   └── Models
│       └── Car.cs
│
├── 05_Inventory
│   ├── Program.cs
│   └── Models
│       └── Item.cs
│
└── 06_Patient
    ├── Program.cs
    └── Models
        └── Patient.cs
```

If your instructor requires one Console App project instead, you can create the required model files in one project and run one exercise at a time. The structure above is a recommendation for clarity, not a requirement from the lab manual.

## Responsibility of each file

| File | Responsibility |
|---|---|
| `Program.cs` | Controls execution, creates objects, calls methods and handles the program flow. |
| `Models/Faculty.cs` | Defines Faculty data and its required member methods. |
| `Models/Employee.cs` | Defines Employee data and its required member methods. |
| Other model files | Define the data and constructor for the entity in that exercise. |

For these beginner programs, do not introduce unnecessary database, repository, service or API layers. Those are different topics in later labs.

---

# 10. Lab 2 practicals explained one by one

This section follows the six practicals in the lab manual. It explains how to think about each requirement before implementation.

## Program 1 – Faculty details

### Requirement

Create a `Faculty` class with:

- `ID`
- `Name`
- `Age`
- `Weight`
- `Height`

Create the member methods:

- `GetFacultyDetails()`
- `DisplayFacultyDetails()`

### Class-design thinking

| Item | Responsibility |
|---|---|
| Faculty class | Represents a faculty member. |
| Fields | Store the faculty's ID, name, age, weight and height. |
| `GetFacultyDetails()` | Accepts the faculty details and assigns them to the current object. |
| `DisplayFacultyDetails()` | Displays the current object's stored details. |

### Example implementation pattern

```csharp
using System;

internal class Faculty
{
    public int Id;
    public string Name = "";
    public int Age;
    public double Weight;
    public double Height;

    public void GetFacultyDetails()
    {
        Console.Write("Enter Faculty ID: ");
        Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Faculty Name: ");
        Name = Console.ReadLine() ?? "";

        Console.Write("Enter Age: ");
        Age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Weight: ");
        Weight = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Height: ");
        Height = Convert.ToDouble(Console.ReadLine());
    }

    public void DisplayFacultyDetails()
    {
        Console.WriteLine($"Faculty ID: {Id}");
        Console.WriteLine($"Faculty Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"Height: {Height}");
    }
}
```

Calling the methods:

```csharp
Faculty faculty = new Faculty();
faculty.GetFacultyDetails();
faculty.DisplayFacultyDetails();
```

### Important teaching point

`GetFacultyDetails()` and `DisplayFacultyDetails()` have different responsibilities. The first accepts and stores data; the second displays the values already stored in the object.

The manual asks for these method names. It does not prescribe whether input must be collected inside the method or passed through parameters. For the beginner console exercise, accepting input inside `GetFacultyDetails()` is a straightforward implementation.

---

## Program 2 – Employee details for five objects

### Requirement

Create an `Employee` class with:

- `Emp_ID`
- `Name`
- `Department`
- `Designation`
- `Salary`

Create five different Employee objects and accept/display their details.

### Main concept

One Employee class can represent all five employees because they share the same structure. Their field values differ because they are different objects.

### Example class design

```csharp
internal class Employee
{
    public int Emp_ID;
    public string Name = "";
    public string Department = "";
    public string Designation = "";
    public decimal Salary;

    public void GetEmpDetails()
    {
        // Accept each employee's information.
    }

    public void DisplayEmpDetails()
    {
        // Display this employee's information.
    }
}
```

`decimal` is a suitable type for salary in many business applications because it is designed for base-10 decimal arithmetic. The manual does not specify the C# type.

### Creating five independent objects

```csharp
Employee emp1 = new Employee();
Employee emp2 = new Employee();
Employee emp3 = new Employee();
Employee emp4 = new Employee();
Employee emp5 = new Employee();
```

Each object has its own instance fields.

### Alternative: create five objects in an array

If you want to manage the five objects together, an array can be used:

```csharp
Employee[] employees = new Employee[5];

for (int i = 0; i < employees.Length; i++)
{
    employees[i] = new Employee();
    employees[i].GetEmpDetails();
}

for (int i = 0; i < employees.Length; i++)
{
    employees[i].DisplayEmpDetails();
}
```

This is an additional design option that combines multiple objects with an object array. The key requirement is still five different employee objects.

### Questions to ask yourself

- Why is one Employee class sufficient?
- Why do the five objects have separate values?
- What is the benefit of using an array when we want to iterate over all five employees?

---

## Program 3 – Volume of a cube using a constructor

### Requirement

Write a program to calculate the volume of a cube using a constructor.

### Understand the mathematics

A cube has equal length, width and height. If its side length is `side`, then:

```text
Volume = side × side × side
```

or:

```text
Volume = side³
```

### Design thinking

1. The Cube class represents one cube.
2. A field stores the side length.
3. The constructor receives and initializes the side length.
4. A method calculates or displays the volume.

### Example implementation

```csharp
using System;

internal class Cube
{
    public double Side;

    public Cube(double side)
    {
        Side = side;
    }

    public double CalculateVolume()
    {
        return Side * Side * Side;
    }
}
```

Usage:

```csharp
Console.Write("Enter cube side: ");
double side = Convert.ToDouble(Console.ReadLine());

Cube cube = new Cube(side);

Console.WriteLine($"Volume: {cube.CalculateVolume()}");
```

### Dry run

For side = 3:

```text
Volume = 3 × 3 × 3
Volume = 27
```

### Why use a constructor?

The side length is supplied when the Cube object is created. The constructor initializes the object's required data so the object is ready for calculation.

---

## Program 4 – Car object using a constructor

### Requirement

Create a Car object with:

- Make
- Model
- Year
- FuelType
- Horsepower

Use a constructor to initialize the object.

### Design thinking

All five values describe one car. A parameterized constructor can receive these five values when a Car object is created.

### Example implementation

```csharp
internal class Car
{
    public string Make;
    public string Model;
    public int Year;
    public string FuelType;
    public int Horsepower;

    public Car(string make, string model, int year,
               string fuelType, int horsepower)
    {
        Make = make;
        Model = model;
        Year = year;
        FuelType = fuelType;
        Horsepower = horsepower;
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Make: {Make}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Year: {Year}");
        Console.WriteLine($"Fuel Type: {FuelType}");
        Console.WriteLine($"Horsepower: {Horsepower}");
    }
}
```

Usage:

```csharp
Car car = new Car("Porsche", "911", 2025, "Petrol", 388);
car.DisplayDetails();
```

### Important point

The constructor's parameter names can be different from the field names. The constructor receives values and assigns them to the new object's fields.

---

## Program 5 – Supermarket inventory using an array of Item objects

### Requirement

Create an `Item` class with:

- `Item_Code`
- `Item_Name`
- `Stock_Quantity`

Use a constructor to initialize objects and an array to store ten different items.

### Design thinking

1. Define one `Item` class.
2. Give the class a constructor that receives the three values.
3. Create an array with ten Item-reference slots.
4. Create ten Item objects and assign them to the ten array indexes.
5. Use a loop to display the items.

### Example implementation

```csharp
using System;

internal class Item
{
    public int Item_Code;
    public string Item_Name;
    public int Stock_Quantity;

    public Item(int itemCode, string itemName, int stockQuantity)
    {
        Item_Code = itemCode;
        Item_Name = itemName;
        Stock_Quantity = stockQuantity;
    }

    public void DisplayItem()
    {
        Console.WriteLine($"Code: {Item_Code}");
        Console.WriteLine($"Name: {Item_Name}");
        Console.WriteLine($"Stock: {Stock_Quantity}");
    }
}
```

Creating and filling the array:

```csharp
Item[] items = new Item[10];

for (int i = 0; i < items.Length; i++)
{
    Console.Write($"Enter item {i + 1} code: ");
    int code = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter item name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter stock quantity: ");
    int quantity = Convert.ToInt32(Console.ReadLine());

    items[i] = new Item(code, name, quantity);
}

Console.WriteLine("\n--- Inventory Report ---");

foreach (Item item in items)
{
    item.DisplayItem();
}
```

### Important: Why must we write `items[i] = new Item(...)`?

`new Item[10]` creates the array, not ten Item objects. Each element initially contains a null reference. The statement `new Item(...)` creates one Item object, and the assignment stores its reference in the selected array slot.

If you try to call `items[i].DisplayItem()` before assigning an Item object to that slot, a `NullReferenceException` can occur.

In a project with nullable reference types enabled, the compiler may issue warnings for an array declared this way before every slot is initialized. Make sure every slot is assigned before dereferencing it, or use nullable annotations and check for null.

---

## Program 6 – Patient records using an array of objects

### Requirement

Create a `Patient` class with:

- `Patient_ID`
- `Name`
- `Age`
- `Disease`

Accept details for multiple patients and display a formatted summary report.




For a first introduction, learn `Convert` and then understand `TryParse` as a safer input-handling approach.

## 11.5 String interpolation

Instead of joining many strings with `+`, use `$` before the string:

```csharp
Console.WriteLine($"ID: {Id}, Name: {Name}");
```

This is called string interpolation.





# 13. Dry-run method

A dry run means tracing the program manually before or after execution. It helps you understand which object is being created, which values are assigned and which method is called.

Example:

```csharp
Student s1 = new Student(101, "Amit");
Student s2 = new Student(102, "Priya");

s1.Display();
s2.Display();
```

Dry-run table:

| Step | Statement | What happens |
|---|---|---|
| 1 | `new Student(101, "Amit")` | First Student object is created and initialized. |
| 2 | `new Student(102, "Priya")` | Second Student object is created and initialized. |
| 3 | `s1.Display()` | First object's details are displayed. |
| 4 | `s2.Display()` | Second object's details are displayed. |

Expected output depends on the implementation of `Display()`, but should represent Amit's record first and Priya's record second.

When dry-running an array of objects, also track the array index and check that the element has been assigned an object before accessing it.

---

# 14. Viva and interview questions with answers

Try to answer each question yourself before reading its answer. The answers are intentionally direct so that beginners can revise them before a practical examination.

## A. Class and object

### Q1. What is a class in C#?

A class is a user-defined reference type that defines the data and behaviour of its objects. It acts as a blueprint for creating objects.

### Q2. What is an object?

An object is an instance of a class. It has its own instance data and can use the class's instance methods.

### Q3. What is the difference between a class and an object?

A class defines the structure; an object is an individual instance created from that structure.

### Q4. Can we create multiple objects from one class?

Yes. One class can be used to create any number of objects, subject to the application's memory and other constraints.

### Q5. Why do we not create 50 Faculty classes for 50 faculty members?

Because all faculty members share the same structure. One Faculty class can create 50 different objects, each with its own field values. This avoids repeating the same class definition.

### Q6. Does creating a class automatically create an object?

No. An object must be created separately, commonly using the `new` keyword.

### Q7. What is the purpose of the `new` keyword?

For a reference type such as a class, `new` creates an object and invokes an appropriate constructor.

### Q8. What is the dot (`.`) operator used for?

It accesses an accessible member through a type or an object, depending on the member and context.

## B. Fields, methods and `this`

### Q9. What is a field?

A field is a variable declared directly in a class or struct, outside methods and constructors. An instance field stores data for an object.

### Q10. What is a local variable?

A local variable is declared inside a method, constructor or block and is limited to its scope.

### Q11. What is a parameter?

A parameter is a variable declared in a method or constructor's parameter list. It receives a value when that method or constructor is called.

### Q12. What is the difference between a field and a parameter?

A field stores data as part of an object (for instance fields). A parameter receives input for a method or constructor call.

### Q13. What is a member method?

A method declared inside a class or struct is a member of that type. It defines an operation that can be performed in the appropriate static or instance context.

### Q14. What is an instance method?

An instance method is a non-static method called through an object. It can access the current object's instance members.

### Q15. What is the `this` keyword?

`this` refers to the current instance of the class or struct. It is commonly used to distinguish an instance field from a parameter with the same name.

### Q16. Why do we write `this.Id = id`?

`this.Id` refers to the current object's field, while `id` refers to the method parameter. The assignment stores the received value in the object's field.

### Q17. How does the same instance method know which object's data to use?

The object reference used to invoke the method identifies the receiver. Inside the method, `this` represents that current object.

## C. Constructors

### Q18. What is a constructor?

A constructor is a special member that runs when an object is created and is commonly used to initialize the object.

### Q19. Does a constructor have a return type?

No. A constructor has the same name as its class and has no return type, not even `void`.

### Q20. When is a constructor called?

It is called as part of creating an object using `new` or another object-creation mechanism.

### Q21. What is a parameterless constructor?

It is a constructor that takes no parameters.

### Q22. What is a parameterized constructor?

It is a constructor that accepts one or more parameters to initialize an object with supplied values.

### Q23. What is constructor overloading?

Defining multiple constructors in the same class with different parameter lists is called constructor overloading.

### Q24. What is the difference between a constructor and a method?

A constructor initializes an object during its creation and has no return type. A method performs an operation and is called explicitly.

### Q25. Does C# provide a constructor automatically?

If a class declares no instance constructors, the compiler provides a parameterless default constructor. If the class declares a constructor, that automatic constructor is not supplied.

### Q26. Can a constructor be overloaded?

Yes. A class can contain multiple constructors with different parameter lists.

## D. Array of objects

### Q27. What is an array?

An array is a fixed-size collection of elements of the same type, accessed using zero-based indexes.

### Q28. What is an array of objects?

It is an array whose elements hold references to objects of a compatible type.

### Q29. Does `new Student[5]` create five Student objects?

No. It creates one array with five reference slots. The Student objects must be created and assigned separately.

### Q30. Why do array indexes start at zero?

C# arrays use zero-based indexing. The first element is at index zero and the last element is at `Length - 1`.

### Q31. What is the `Length` property?

It returns the total number of elements (slots) in an array.

### Q32. What happens if we access an invalid array index?

The program throws an `IndexOutOfRangeException` at runtime.

### Q33. What happens if an object-array element is null and we access its field?

A `NullReferenceException` can occur because no object is referenced by that array element.

### Q34. Why do we use loops with arrays of objects?

Loops allow us to process each array element without writing separate statements for every object.

### Q35. What is the difference between an array of objects and separate object variables?

Separate variables refer to individual objects. An array groups multiple object references under one variable and makes indexed access and iteration convenient.

## E. Static and instance concepts

### Q36. What is a static member?

A static member belongs to the type itself rather than to a particular object instance.

### Q37. What is an instance member?

An instance member belongs to an individual object. Each object has its own instance fields.

### Q38. Can a static method directly access an instance field?

No. A static method has no current object (`this`). It can access an instance field through an object reference that is available to it.

### Q39. Why is the traditional `Main()` method static?

It is the application entry point and can be invoked by the runtime without first creating an instance of the Program class.

### Q40. Does static mean that data is permanently saved?

No. Static describes ownership by the type, not permanent storage. Static data is still held in memory and does not automatically survive application termination.

## F. Scenario-based questions

### Q41. A college has 50 faculty members. Should we create 50 classes?

No. Define one Faculty class and create 50 Faculty objects because the faculty members share the same structure.

### Q42. A question asks for five Employee objects. What should you do?

Define one Employee class, create five different Employee objects, accept or initialize each object's details and display them.

### Q43. A question asks for ten Item objects in an array. What is the sequence?

Define the Item class and constructor, create an Item array with ten slots, create an Item object for each slot, assign each object to its index and iterate through the array to display the items.

### Q44. Why is a constructor useful for Item objects?

It allows the required item information to be supplied when each object is created, so the object can be initialized immediately.

### Q45. Why should we separate input and display methods?

They have different responsibilities. One method accepts and stores information, while the other displays the stored information. Separate responsibilities make the program easier to understand.

---

# 16. Quick revision sheet

| Term | Remember this |
|---|---|
| Class | Blueprint that defines data and behaviour. |
| Object | Instance of a class. |
| Field | Variable declared at class level outside methods/constructors. |
| Local variable | Variable declared inside a method, constructor or block. |
| Parameter | Receives input in a method or constructor call. |
| Method | Defines an operation. |
| Instance method | Method called through an object. |
| `this` | Reference to the current object instance. |
| `new` | Creates an object and invokes its constructor. |
| Constructor | Initializes an object during creation. |
| Parameterless constructor | Constructor with no parameters. |
| Parameterized constructor | Constructor that receives values. |
| Constructor overloading | Multiple constructors with different parameter lists. |
| Array | Fixed-size group of same-type elements. |
| Array of objects | Array containing object references. |
| `Length` | Number of array slots. |
| Index | Zero-based position of an array element. |
| `static` | Member belongs to the type. |
| Instance | Member belongs to an object. |

## The complete Lab 2 learning flow

```text
1. CLASS
   Understand the common structure of an entity.
          |
          v
2. OBJECT
   Create an individual instance from the class.
          |
          v
3. FIELDS AND METHODS
   Store object information and define its operations.
          |
          v
4. MULTIPLE OBJECTS
   Create different objects from the same class.
          |
          v
5. CONSTRUCTOR
   Initialize object information during creation.
          |
          v
6. ARRAY OF OBJECTS
   Store and process multiple object references using indexes and loops.
```

## Final advice for students

Do not memorize the six programs as six unrelated code samples. Understand the progression:

- Faculty teaches how to define a class and its methods.
- Employee teaches how one class can represent multiple objects.
- Cube and Car teach how constructors initialize objects.
- Item and Patient teach how arrays can hold and process multiple objects.

When you understand why each concept is needed, you can solve a new problem even when the class name or field names change.

---

**End of Lab 2 – Class, Objects, Array of Objects & Constructor**
