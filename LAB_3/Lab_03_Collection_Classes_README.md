# Lab 3 – Collection Classes

> **Course:** Building RESTful APIs with ASP.NET Core  
> **Lab topic:** Collection Classes  
> **Purpose of this guide:** Help students understand what collections are, why different collection types exist, how to choose the correct one, how to complete each lab exercise, and how to answer practical-examination and interview questions.

---



## The six practicals in the lab manual

| Program | Practical requirement | Collection |
|---|---|---|
| 1 | Menu-driven Student Records Management: Add, Display, Search, Update and Delete. | `List<Student>` |
| 2 | Shopping Cart: add items, remove items, view cart details and calculate total with discount. | `List<CartItem>` |
| 3 | Student Grade Book: add/update student details, subject-wise marks, percentage and reports. | `Dictionary<int, Student>` |
| 4 | Product Catalog and Inventory: add products, update stock, search and generate bills. | `Dictionary<string, Product>` |
| 5 | Role-Based Permission System: assign roles/permissions, check permissions and merge permissions. | `HashSet<string>` |
| 6 | Social Networking: manage interests and friends, find mutual friends, suggest users based on interests and remove duplicate tags. | `HashSet<T>` |

This README focuses on the collection concepts and the requirements listed in the lab manual. Additional collection types are clearly marked as extra knowledge for interviews.

---

# 2. Why do we need collection classes?

## 2.1 Start with a real problem

Suppose a college has 500 students. Each student has an ID, name and age. The application must:

- Add new students.
- Remove students who leave.
- Search for a student.
- Update student details.
- Display all students.

In the previous lab, you learned how to define a `Student` class and create Student objects. But a Student class describes one student; it does not by itself provide a convenient structure for managing hundreds of Student objects together.

We therefore need a way to group and manage related data.

## 2.2 Three ways to think about storing many objects

### Option 1 – Separate object variables

```csharp
Student student1 = new Student();
Student student2 = new Student();
Student student3 = new Student();
```

This works for a small number of objects, but maintaining hundreds of individually named variables is inconvenient.

### Option 2 – Array of objects

```csharp
Student[] students = new Student[500];
```

An array groups object references and supports index-based access. Its size is fixed after creation. The individual Student objects still need to be created and assigned to the array slots.

### Option 3 – Collection

```csharp
List<Student> students = new List<Student>();
```

A `List<Student>` can grow and shrink as elements are added or removed. It provides operations such as `Add`, `Remove`, `Insert`, `Find` and `Count`.

## 2.3 Actual meaning of a collection

A **collection is an object or data structure used to store and manage a group of elements.** .NET provides collection classes and interfaces that offer ready-made operations for working with groups of data.

Examples:

- `List<Student>` stores a sequence of Student elements.
- `Dictionary<int, Student>` stores Student values associated with integer keys.
- `HashSet<string>` stores distinct strings.

## 2.4 Class versus object versus array versus collection

| Concept | Meaning | Example |
|---|---|---|
| Class | Defines a type and its members. | `Student` |
| Object | One instance of a class. | `new Student()` |
| Array | Fixed-size indexed group of same-type elements. | `Student[]` |
| Collection | Structure with operations for managing a group of elements. | `List<Student>` |

**Important:** A collection does not replace the Student class. The Student class defines one record; the collection manages multiple records.

---

# 3. Collection, collection class and generic type

## 3.1 What is a collection class?

A collection class is a class provided by .NET that implements a particular way of storing and managing elements. Different collections offer different rules and operations.

The most important collections in this lab are:

- `List<T>` – sequence of elements.
- `Dictionary<TKey, TValue>` – key-value pairs.
- `HashSet<T>` – unique elements.

## 3.2 What does the `T` mean?

`T` is a generic type parameter. It represents the type of elements that a generic collection will store.

```csharp
List<Student> students = new List<Student>();
List<int> marks = new List<int>();
List<string> names = new List<string>();
```

| Generic declaration | Element type |
|---|---|
| `List<Student>` | Student objects |
| `List<int>` | Integer values |
| `List<string>` | String values |

For Dictionary, there are two type parameters:

```csharp
Dictionary<int, Student> studentsById =
    new Dictionary<int, Student>();
```

- `int` is the key type.
- `Student` is the value type.

## 3.3 Why use generic collections?

Generic collections provide compile-time type safety. A `List<Student>` is intended to accept Student values, not unrelated types such as an integer or a Faculty object.

```csharp
List<Student> students = new List<Student>();

students.Add(new Student()); // Valid
// students.Add(100);        // Compile-time error
```

This helps catch type mistakes earlier and reduces the need for manual casting when retrieving elements.

## 3.4 The `System.Collections.Generic` namespace

The generic collections used in this lab are available in the `System.Collections.Generic` namespace.

```csharp
using System.Collections.Generic;
```

Modern .NET projects may include common namespaces through implicit global usings, depending on project settings. If your project does not recognize `List<T>`, `Dictionary<TKey, TValue>` or `HashSet<T>`, check that the namespace is imported.

---

# 4. Arrays versus collections

| Feature | Array | `List<T>` |
|---|---|---|
| Size | Fixed when created. | Can grow and shrink. |
| Index access | Yes. | Yes. |
| Number of elements | `Length`. | `Count`. |
| Add an element | No built-in resizing `Add()` operation. | `Add()`. |
| Remove an element | Requires shifting/copying or building another array. | `Remove()` or `RemoveAt()`. |
| Storage | Elements are stored in indexed slots. | List manages an internal array-like storage structure. |
| Best fit | Number of elements is fixed or array-specific behaviour is required. | Number of elements changes and convenient collection operations are needed. |

## Does a List automatically save memory compared with an array?

No. A List does not automatically use less memory. It manages its internal storage and capacity for you. A List may reserve more capacity than its current element count so that it can grow efficiently.

## Can an array grow?

An existing array's length cannot be changed. You can create a larger array, copy existing values/references and use the new array. .NET also provides `Array.Resize()` to perform this operation by creating/replacing the array reference with a resized array.

For frequently changing numbers of elements, `List<T>` is often more convenient.

---

# 5. Generic and non-generic collections

## 5.1 Generic collections

Generic collections specify the element type or key/value types.

Examples:

```csharp
List<Student> students = new List<Student>();
Dictionary<int, Student> studentsById = new Dictionary<int, Student>();
HashSet<string> permissions = new HashSet<string>();
```

Advantages:

- Compile-time type checking.
- Less manual casting when retrieving elements.
- Clearer code because the intended data type is visible.
- Better suitability for modern strongly typed C# code.

## 5.2 Non-generic collections

Older non-generic collections can store values as `object`, so unrelated types can be placed in the same collection.

Examples include:

- `ArrayList`
- `Hashtable`
- Non-generic `Queue`
- Non-generic `Stack`

Example:

```csharp
using System.Collections;

ArrayList values = new ArrayList();
values.Add(10);
values.Add("Hello");
values.Add(true);
```

Because multiple types can be stored, the programmer must take care when retrieving and converting values.

## 5.3 Generic versus non-generic

| Generic | Non-generic |
|---|---|
| Specifies the type. | Commonly stores values as `object`. |
| Provides compile-time type safety. | Allows unrelated types to be mixed. |
| Usually avoids manual casts for retrieval. | May require casting. |
| Examples: `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`. | Examples: `ArrayList`, `Hashtable`. |

**Interview point:** Prefer generic collections for most modern C# application code unless a specific API or requirement calls for a non-generic collection.

---

# 6. List<T>

## 6.1 What is `List<T>`?

`List<T>` is a generic, dynamically resizable collection that stores elements in a sequence and supports index-based access.

It allows duplicate values. It maintains the sequence of elements by index.

Example:

```csharp
List<string> names = new List<string>();
```

## 6.2 Understand the declaration

```csharp
List<Student> students = new List<Student>();
```

| Part | Meaning |
|---|---|
| `List` | Generic collection class. |
| `<Student>` | Element type: Student. |
| `students` | Variable that refers to the List object. |
| `new List<Student>()` | Creates an empty List object. |

Creating the List does not create any Student objects. It creates the collection. You add Student objects separately.

## 6.3 Add elements

```csharp
List<Student> students = new List<Student>();

Student s1 = new Student { Id = 101, Name = "Amit" };
Student s2 = new Student { Id = 102, Name = "Priya" };

students.Add(s1);
students.Add(s2);
```

`Add()` appends an element to the end of the List.

## 6.4 Add an object directly

```csharp
students.Add(new Student { Id = 103, Name = "Rahul" });
```

This creates a Student object and adds its reference to the List.

## 6.5 Display elements using `foreach`

```csharp
foreach (Student student in students)
{
    Console.WriteLine($"{student.Id} - {student.Name}");
}
```

A `foreach` loop visits each element in the collection. The local variable `student` refers to the current element during that iteration.

## 6.6 Access an element by index

```csharp
Student firstStudent = students[0];
Console.WriteLine(firstStudent.Name);
```

List indexes start at zero. Accessing an index outside the valid range throws an `ArgumentOutOfRangeException`.

## 6.7 Important List properties and methods

| Member | Purpose | Example |
|---|---|---|
| `Count` | Number of elements currently in the List. | `students.Count` |
| `Capacity` | Number of elements the List can hold before it needs to grow its internal storage. | `students.Capacity` |
| `Add(item)` | Adds an element at the end. | `students.Add(s1)` |
| `AddRange(items)` | Adds multiple elements. | `students.AddRange(otherStudents)` |
| `Insert(index, item)` | Inserts at an index. | `students.Insert(0, s1)` |
| `Remove(item)` | Removes the first matching element. | `students.Remove(s1)` |
| `RemoveAt(index)` | Removes the element at an index. | `students.RemoveAt(0)` |
| `RemoveAll(condition)` | Removes all elements matching a predicate. | `students.RemoveAll(s => s.Age < 18)` |
| `Contains(item)` | Checks whether an equal element exists. | `students.Contains(s1)` |
| `Find(condition)` | Returns the first matching element or default if none is found. | `students.Find(s => s.Id == 101)` |
| `FindIndex(condition)` | Returns the index of the first matching element, or -1. | `students.FindIndex(s => s.Id == 101)` |
| `Clear()` | Removes all elements. | `students.Clear()` |
| `Sort()` | Sorts elements using the default or supplied comparer. | `numbers.Sort()` |
| `ToArray()` | Creates an array containing the List's elements. | `students.ToArray()` |

## 6.8 Count versus Capacity

This is a common interview question.

- `Count` is the actual number of elements currently stored.
- `Capacity` is the number of elements the List can hold before its internal storage must grow.

Example:

```csharp
List<int> numbers = new List<int>();

Console.WriteLine(numbers.Count);
Console.WriteLine(numbers.Capacity);

numbers.Add(10);
numbers.Add(20);

Console.WriteLine(numbers.Count);
Console.WriteLine(numbers.Capacity);
```

Guaranteed facts:

- Initially, `Count` is zero.
- After adding two elements, `Count` is two.
- `Capacity` is at least `Count`.
- The exact capacity growth strategy is an implementation detail; do not assume one exact capacity value in a viva answer unless it is specified by the runtime/API version.

Adding elements does not allocate a completely new backing array every time. The List uses available capacity and grows its internal storage when necessary.

## 6.9 `Remove()` versus `RemoveAt()`

```csharp
students.Remove(studentObject);
students.RemoveAt(1);
```

- `Remove()` searches for and removes the first matching element.
- `RemoveAt()` removes the element at the specified index.

After removing an element from the middle, later elements shift to fill the gap, and the List's `Count` decreases.

## 6.10 Search using `Find()` and `FindIndex()`

```csharp
Student? foundStudent = students.Find(s => s.Id == 102);

if (foundStudent != null)
{
    Console.WriteLine(foundStudent.Name);
}
else
{
    Console.WriteLine("Student not found.");
}
```

`Find()` returns the first matching element or the default value for the element type if no match is found. For a class reference type, the default is `null`.

Using `FindIndex()`:

```csharp
int index = students.FindIndex(s => s.Id == 102);

if (index != -1)
{
    Console.WriteLine(students[index].Name);
}
```

`-1` indicates that no matching index was found.

## 6.11 Update an element

```csharp
int index = students.FindIndex(s => s.Id == 102);

if (index != -1)
{
    students[index].Name = "Priya Shah";
    students[index].Age = 21;
}
```

The List holds references to Student objects. Accessing `students[index]` gives access to the object at that position, so its fields can be updated.

## 6.12 Does List allow duplicate values?

Yes. A List allows duplicate values.

```csharp
List<string> names = new List<string>();
names.Add("Amit");
names.Add("Priya");
names.Add("Amit");
```

All three entries can exist. A List does not enforce uniqueness.

## 6.13 Full example – Student Records using List

This example demonstrates the first practical's five operations. It uses a simple Student model and an in-memory List; no database is involved.

### Student.cs

```csharp
internal class Student
{
    public int Id;
    public string Name = "";
    public int Age;
}
```

### Program.cs

```csharp
using System;
using System.Collections.Generic;

internal class Program
{
    static List<Student> students = new List<Student>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== STUDENT MANAGEMENT =====");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Search");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    DisplayStudents();
                    break;
                case 3:
                    SearchStudent();
                    break;
                case 4:
                    UpdateStudent();
                    break;
                case 5:
                    DeleteStudent();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        if (students.Exists(s => s.Id == id))
        {
            Console.WriteLine("This ID already exists.");
            return;
        }

        Console.Write("Enter name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter age: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Invalid age.");
            return;
        }

        students.Add(new Student { Id = id, Name = name, Age = age });
        Console.WriteLine("Student added.");
    }

    static void DisplayStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        foreach (Student student in students)
        {
            Console.WriteLine($"{student.Id} | {student.Name} | {student.Age}");
        }
    }

    static void SearchStudent()
    {
        Console.Write("Enter ID to search: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Student? student = students.Find(s => s.Id == id);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine($"{student.Id} | {student.Name} | {student.Age}");
    }

    static void UpdateStudent()
    {
        Console.Write("Enter ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Student? student = students.Find(s => s.Id == id);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.Write("Enter new name: ");
        string newName = Console.ReadLine() ?? "";

        Console.Write("Enter new age: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Invalid age. No changes were saved.");
            return;
        }

        student.Name = newName;
        student.Age = age;
        Console.WriteLine("Student updated.");
    }

    static void DeleteStudent()
    {
        Console.Write("Enter ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        int removed = students.RemoveAll(s => s.Id == id);
        Console.WriteLine(removed > 0 ? "Student deleted." : "Student not found.");
    }
}
```

### Why is the List declared outside Main in this example?

The example uses a static field:

```csharp
static List<Student> students = new List<Student>();
```

All CRUD methods are static and need to work with the same List. A static field can be accessed directly by static methods of the same class.

This is one design option, not a rule that all Lists must be static. Other valid designs include:

1. Declare a local List inside `Main()` and pass it as a parameter to the methods.
2. Put an instance List and instance CRUD methods inside a separate `StudentManager` class, then create one `StudentManager` object in `Main()`.

Static versus instance describes member ownership. It does not change the type or behaviour of `List<T>` itself.

---

# 7. Dictionary<TKey, TValue>

## 7.1 What is a Dictionary?

`Dictionary<TKey, TValue>` is a generic collection that stores values using unique keys.

- `TKey` specifies the key type.
- `TValue` specifies the value type.

Example:

```csharp
Dictionary<int, Student> studentsById =
    new Dictionary<int, Student>();
```

Here:

- The key is an integer Student ID.
- The value is a Student object.

## 7.2 Visualize key-value storage

```text
Key                 Value
101  ------------>  Student { Id = 101, Name = "Amit" }
102  ------------>  Student { Id = 102, Name = "Priya" }
103  ------------>  Student { Id = 103, Name = "Rahul" }
```

The key is used to locate its associated value.

## 7.3 Add a key-value pair

```csharp
Student amit = new Student { Id = 101, Name = "Amit" };
studentsById.Add(101, amit);
```

The `Add()` method throws an `ArgumentException` if the key already exists.

## 7.4 Access an element using its key

```csharp
Student student = studentsById[101];
Console.WriteLine(student.Name);
```

If the key does not exist, using the indexer to read it throws a `KeyNotFoundException`.

## 7.5 Check whether a key exists

```csharp
if (studentsById.ContainsKey(101))
{
    Console.WriteLine("Student ID exists.");
}
```

## 7.6 Use TryGetValue for safer lookup

```csharp
if (studentsById.TryGetValue(101, out Student? student))
{
    Console.WriteLine(student.Name);
}
else
{
    Console.WriteLine("Student not found.");
}
```

`TryGetValue()` checks for a key and retrieves its value without requiring an exception for a missing key.

## 7.7 Add versus indexer assignment

These operations are different:

```csharp
studentsById.Add(101, amit);
```

`Add()` expects that key 101 is not already present. It throws if the key already exists.

```csharp
studentsById[101] = amit;
```

The indexer assignment adds the pair if the key does not exist, or replaces the existing value associated with that key.

Use the one that matches the intended business rule.

## 7.8 Important Dictionary members

| Member | Purpose |
|---|---|
| `Add(key, value)` | Adds a new key-value pair; duplicate key causes an exception. |
| `dictionary[key]` | Reads the value for a key; missing key causes an exception. |
| `dictionary[key] = value` | Adds or replaces a value for a key. |
| `ContainsKey(key)` | Checks whether a key exists. |
| `TryGetValue(key, out value)` | Safely attempts to retrieve a value. |
| `Remove(key)` | Removes the entry associated with a key. |
| `Count` | Number of key-value entries. |
| `Keys` | Collection of keys. |
| `Values` | Collection of values. |
| `Clear()` | Removes every entry. |

## 7.9 Why must Dictionary keys be unique?

A key identifies a particular entry. If two entries used the same key, a lookup by that key would not identify a single unambiguous entry. Therefore, Dictionary enforces key uniqueness.

Values do not need to be unique. Two different keys may refer to equal or even the same value object.

## 7.10 Example – Student Grade Book

The lab manual asks for `Dictionary<int, Student>` where each Student record contains subject-wise marks.

One possible design is:

```csharp
class Student
{
    public int Id;
    public string Name = "";
    public Dictionary<string, double> SubjectMarks = new Dictionary<string, double>();
}
```

The outer Dictionary uses the Student ID as its key. Each Student object contains another Dictionary that maps subject names to marks.

Example setup:

```csharp
Dictionary<int, Student> gradeBook = new Dictionary<int, Student>();

Student amit = new Student
{
    Id = 101,
    Name = "Amit"
};

amit.SubjectMarks.Add("C#", 85);
amit.SubjectMarks.Add("Database", 90);

gradeBook.Add(amit.Id, amit);
```

Calculating percentage depends on the grading rule and maximum marks. For a simple example where each subject is out of 100:

```csharp
double total = 0;

foreach (double marks in amit.SubjectMarks.Values)
{
    total += marks;
}

double percentage = amit.SubjectMarks.Count == 0
    ? 0
    : total / amit.SubjectMarks.Count;
```

The example assumes equal maximum marks for each subject. If subjects have different maximum marks, percentage must be calculated using the appropriate total obtained marks and total maximum marks.

---

# 8. HashSet<T>

## 8.1 What is a HashSet?

`HashSet<T>` is a generic collection that stores distinct elements according to its equality rules. Attempting to add an element that is already present does not add a duplicate.

Example:

```csharp
HashSet<string> permissions = new HashSet<string>();
```

## 8.2 Visualize uniqueness

Input attempts:

```text
Read, Write, Update, Read, Delete, Write
```

Stored distinct values:

```text
Read, Write, Update, Delete
```

The order shown is illustrative. HashSet does not guarantee that enumeration follows insertion order.

## 8.3 Add elements

```csharp
HashSet<string> permissions = new HashSet<string>();

permissions.Add("Read");
permissions.Add("Write");
permissions.Add("Update");
permissions.Add("Read");
```

`Add()` returns `true` if the element was added and `false` if an equal element already existed.

```csharp
bool added = permissions.Add("Read");
Console.WriteLine(added);
```

If `Read` already exists, the result is `false`.

## 8.4 Important HashSet members

| Member | Purpose |
|---|---|
| `Add(item)` | Adds an element if it is not already present. Returns whether it was added. |
| `Remove(item)` | Removes an element. |
| `Contains(item)` | Checks whether an element exists. |
| `Count` | Number of distinct elements. |
| `Clear()` | Removes all elements. |
| `UnionWith(other)` | Adds elements from another collection. |
| `IntersectWith(other)` | Keeps only elements common to both sets. |
| `ExceptWith(other)` | Removes elements that occur in another set. |
| `SymmetricExceptWith(other)` | Keeps elements found in either set, but not both. |
| `IsSubsetOf(other)` | Checks whether every element of this set is in another set. |
| `IsSupersetOf(other)` | Checks whether this set contains every element of another set. |

## 8.5 Set operations with a real example

Suppose:

```csharp
HashSet<string> facultyPermissions =
    new HashSet<string> { "Read", "Write", "Update" };

HashSet<string> adminPermissions =
    new HashSet<string> { "Read", "Write", "Delete" };
```

### Union – all distinct elements from both sets

```csharp
HashSet<string> allPermissions = new HashSet<string>(facultyPermissions);
allPermissions.UnionWith(adminPermissions);
```

Result as a set:

```text
Read, Write, Update, Delete
```

### Intersection – common elements

```csharp
HashSet<string> commonPermissions = new HashSet<string>(facultyPermissions);
commonPermissions.IntersectWith(adminPermissions);
```

Result as a set:

```text
Read, Write
```

### Difference – elements in the first set but not the second

```csharp
HashSet<string> facultyOnly = new HashSet<string>(facultyPermissions);
facultyOnly.ExceptWith(adminPermissions);
```

Result:

```text
Update
```

### Symmetric difference – elements in either set but not both

```csharp
HashSet<string> differentPermissions = new HashSet<string>(facultyPermissions);
differentPermissions.SymmetricExceptWith(adminPermissions);
```

Result:

```text
Update, Delete
```

## 8.6 How does HashSet know whether an element is a duplicate?

HashSet uses equality comparison. For built-in types such as strings and integers, the default equality behaviour compares values according to the type's equality implementation.

For a custom class such as `Student`, the default equality behaviour is generally based on object identity unless the class supplies value-based equality (for example, by overriding `Equals()` and `GetHashCode()` or by using an appropriate equality comparer).

This means two separately created Student objects with the same ID are not automatically guaranteed to be considered equal by a HashSet. If uniqueness should be based on Student ID, define that equality rule or provide an `IEqualityComparer<Student>`.

## 8.7 HashSet versus List

| List<T> | HashSet<T> |
|---|---|
| Allows duplicates. | Prevents equal duplicate elements. |
| Supports index-based access. | Does not provide index-based access. |
| Maintains a sequence by index. | Does not guarantee insertion order. |
| Useful for ordered records and repeated values. | Useful for membership checks and unique values. |

---

# 9. Other useful collection types

The following collections are useful background knowledge for exams and interviews. They are not the three main collection types used in the six practicals of this lab.

## 9.1 Queue<T>

A Queue follows **FIFO (First In, First Out)**. The first item added is normally the first item removed.

Typical operations:

- `Enqueue()` – add an item at the back.
- `Dequeue()` – remove and return the item at the front.
- `Peek()` – view the front item without removing it.

Example use: a line of customers waiting for service.

## 9.2 Stack<T>

A Stack follows **LIFO (Last In, First Out)**. The most recently added item is normally the first item removed.

Typical operations:

- `Push()` – add an item to the top.
- `Pop()` – remove and return the top item.
- `Peek()` – view the top item without removing it.

Example use: undo operations or browser-history-like backtracking patterns.

## 9.3 LinkedList<T>

A LinkedList is a sequence of nodes. Each node refers to neighbouring node(s), depending on the linked-list type. It supports insertion/removal around known nodes, but does not provide direct index access like a List.

## 9.4 SortedSet<T>

A SortedSet stores unique elements and keeps them sorted according to a comparer.

## 9.5 SortedDictionary<TKey, TValue>

A key-value collection that maintains entries in key-sorted order.

## 9.6 ConcurrentDictionary<TKey, TValue>

A dictionary designed to support concurrent access and updates from multiple threads.

**Interview advice:** First become confident with List, Dictionary and HashSet because those are the collections required by this lab. Learn the others as additional knowledge.

---

# 10. Choosing the correct collection

Do not select a collection only because you know its syntax. Read the requirement and identify how the data must be organized.

| Requirement | Suitable collection | Reason |
|---|---|---|
| Store student records in a sequence. | `List<Student>` | Ordered sequence, indexed access, convenient add/remove. |
| Store shopping cart items. | `List<CartItem>` | Cart contains a sequence of item records and quantities. |
| Find a student using unique Student ID. | `Dictionary<int, Student>` | Student ID can be the key. |
| Find product using product code. | `Dictionary<string, Product>` | Product code can identify the product. |
| Store unique role permissions. | `HashSet<string>` | Duplicate permission strings should not be stored. |
| Store unique interests. | `HashSet<string>` | Duplicate interests should be ignored. |
| Find common friends. | `HashSet<T>` | Intersection returns common set elements. |

## Questions to ask before selecting a collection

1. Do I need to preserve a sequence and access elements by index?
2. Do I need to retrieve a value using a unique key?
3. Must duplicate values be prevented?
4. Will the number of elements change?
5. Do I need set operations such as union and intersection?
6. What equality rule defines two values as the same?

---

# 11. Static, instance, local collections and method parameters

A common beginner confusion is thinking that a List must be static. That is not true. A collection can be a local variable, an instance field or a static field. These choices describe where the variable belongs and how it can be accessed; they do not change what `List<T>` is.

## 11.1 Local List inside Main

```csharp
static void Main()
{
    List<Student> students = new List<Student>();

    students.Add(new Student());
    Console.WriteLine(students.Count);
}
```

This works because the List is created and used inside the same method. Other methods cannot directly access this local variable by its name.

## 11.2 Pass the List to another method

```csharp
static void Main()
{
    List<Student> students = new List<Student>();

    AddStudent(students);
    DisplayStudents(students);
}

static void AddStudent(List<Student> students)
{
    students.Add(new Student());
}

static void DisplayStudents(List<Student> students)
{
    Console.WriteLine(students.Count);
}
```

The same List reference is passed to each method. The methods operate on the same underlying List object.

Passing the reference does not make a second copy of the List.

## 11.3 Static field shared by static methods

```csharp
class Program
{
    static List<Student> students = new List<Student>();

    static void AddStudent()
    {
        students.Add(new Student());
    }

    static void DisplayStudents()
    {
        Console.WriteLine(students.Count);
    }
}
```

The static List belongs to the Program type and can be accessed directly by its static methods.

## 11.4 Instance field inside a manager object

```csharp
class StudentManager
{
    private List<Student> students = new List<Student>();

    public void AddStudent()
    {
        students.Add(new Student());
    }

    public void DisplayStudents()
    {
        Console.WriteLine(students.Count);
    }
}
```

Use it from the entry point:

```csharp
static void Main()
{
    StudentManager manager = new StudentManager();
    manager.AddStudent();
    manager.DisplayStudents();
}
```

The StudentManager object owns its instance List and methods.

## 11.5 What if two manager objects are created?

```csharp
StudentManager manager1 = new StudentManager();
StudentManager manager2 = new StudentManager();
```

Each manager object receives its own instance List because `students` is a non-static instance field.

## 11.6 Summary

| Where the List is declared | How another method uses it |
|---|---|
| Local variable in `Main()` | Pass it as a parameter or keep operations inside Main. |
| Static field | Static methods of the class can access it directly. |
| Instance field | Instance methods of the same object can access it; create/use an object from static Main. |

**Important:** The lab requires learning collection classes. It does not require every collection to be static. Keep the storage choice separate from the collection's purpose.

---

# 12. Understand the six lab practicals

This section follows the six exercises in the lab manual and explains the design decisions before implementation.

## Program 1 – Student Records using List<Student>

### Requirement

Build a menu-driven console application with Add, Display, Search, Update and Delete operations.

### Suggested Student fields

The manual does not specify the exact Student fields for this exercise. A simple teaching example can use:

- Id
- Name
- Age

### Suggested project structure

```text
01_StudentManagement
│
├── Program.cs
└── Models
    └── Student.cs
```

### Design responsibilities

| File/member | Responsibility |
|---|---|
| `Student.cs` | Defines one Student record. |
| `List<Student> students` | Stores multiple Student objects. |
| `AddStudent()` | Accepts information, creates a Student object and adds it to the List. |
| `DisplayStudents()` | Iterates through the List and displays all records. |
| `SearchStudent()` | Finds a record by a chosen identifier such as ID. |
| `UpdateStudent()` | Finds a record and changes its fields. |
| `DeleteStudent()` | Removes a matching record. |

### Logical flow

```text
Start
  |
Create an empty List<Student>
  |
Display menu
  |
Read user's choice
  |
  +-- Add ------> Create Student object -> Add to List
  +-- Display --> Iterate through List -> Show records
  +-- Search ---> Find matching record -> Show result
  +-- Update ---> Find record -> Change fields
  +-- Delete ---> Find record -> Remove record
  +-- Exit -----> End
```

### Important design decisions

- Decide which field uniquely identifies a Student, such as ID.
- Check for an existing ID before adding if duplicate IDs are not allowed by the application.
- Handle the empty List case before displaying or searching.
- Check whether the search result exists before updating or deleting.
- Keep the same List instance available to every operation.

The unique-ID rule is a sensible application design decision for this example; the manual does not explicitly specify the Student fields or the uniqueness rule.

---

## Program 2 – Shopping Cart using List<CartItem>

### Requirement

Use `List<CartItem>` to support:

- Adding items.
- Removing items.
- Viewing cart details.
- Calculating the total amount with discount.

### Think about the CartItem class

A CartItem represents one product line in the cart. A reasonable design might include:

| Field | Possible type | Purpose |
|---|---|---|
| ProductId | int | Identifies the product. |
| ProductName | string | Product description. |
| UnitPrice | decimal | Price of one unit. |
| Quantity | int | Number of units. |

The manual does not prescribe these exact fields; they are an example design.

### Important calculation

```text
Line total = Unit price × Quantity
Cart subtotal = Sum of all line totals
Discount amount = Subtotal × Discount percentage / 100
Final total = Subtotal - Discount amount
```

Use `decimal` for prices and monetary calculations in this example.

### Example calculation

| Product | Unit price | Quantity | Line total |
|---|---:|---:|---:|
| Notebook | 50.00 | 2 | 100.00 |
| Pen | 10.00 | 3 | 30.00 |
| **Subtotal** | | | **130.00** |

If the discount is 10%:

```text
Discount = 130 × 10 / 100 = 13
Final total = 130 - 13 = 117
```

### Useful List operations

- `Add()` to add a CartItem.
- `Find()` or `FindIndex()` to locate a cart item.
- `Remove()` or `RemoveAt()` to remove a cart item.
- `foreach` to display cart lines and calculate totals.

### A design question

If the customer adds the same product again, should the program create another cart line or increase the existing line's quantity? Both policies are possible. Choose one and explain it. The manual only requires adding and removing items; it does not prescribe this business rule.

---

## Program 3 – Student Grade Book using Dictionary<int, Student>

### Requirement

Use `Dictionary<int, Student>` where each Student record contains subject-wise marks. Support adding/updating details, calculating percentage and displaying reports.

### Suggested data structure

The outer Dictionary can use Student ID as the key and a Student object as the value. Each Student object can contain a second Dictionary for subject marks.

```csharp
class Student
{
    public int Id;
    public string Name = "";
    public Dictionary<string, double> SubjectMarks = new Dictionary<string, double>();
}
```

Example:

```text
Student ID (key) -> Student object (value)

101 -> Amit
      C#       -> 85
      Database -> 90

102 -> Priya
      C#       -> 92
      Database -> 88
```

### Logical operations

1. Add a Student using the Student ID as a key.
2. Add subject marks to that Student's subject-mark Dictionary.
3. Update a subject's mark using its subject key.
4. Calculate total or percentage using the defined maximum-mark rule.
5. Display each student's subject-wise report.

### Percentage calculation

If every subject has the same maximum mark:

```text
Percentage = (Total marks obtained / Total maximum marks) × 100
```

If the marks are out of 100 for each subject, the average of the subject marks is equivalent to the percentage. If maximum marks differ, use total obtained divided by total possible marks.

### Important Dictionary point

Before adding a Student using `Add(id, student)`, check whether that ID already exists if duplicate IDs are not allowed. Otherwise, `Add()` throws an exception for a duplicate key.

---

## Program 4 – Product Catalog and Inventory using Dictionary<string, Product>

### Requirement

Use `Dictionary<string, Product>` for:

- Adding products.
- Updating stock.
- Searching products.
- Generating bills.

### Suggested Product fields

| Field | Possible type |
|---|---|
| ProductCode | string |
| ProductName | string |
| UnitPrice | decimal |
| StockQuantity | int |

The manual specifies the Dictionary key as `string` but does not prescribe all Product fields or exact property names. These are suggested example fields.

### Why string as the key?

A product code may contain letters and digits, such as `PEN101` or `BOOK205`. A string key can represent these codes.

### Example structure

```text
Product code (key) -> Product object (value)

PEN101  -> Blue Pen, Price 10, Stock 100
BOOK205 -> C# Book, Price 450, Stock 20
```

### Main operations

**Add product:** Validate that the product code does not already exist, create the Product object and add it to the Dictionary.

**Update stock:** Find the product by code and increase/decrease the stock quantity according to the operation.

**Search:** Use `TryGetValue(productCode, out product)` to find the product.

**Generate bill:** For each purchased line, calculate `unit price × quantity`, add the line total to the bill subtotal and display the total.

### Important business checks

- Do not sell more units than the available stock.
- Do not accept a negative quantity.
- Do not accept an invalid or negative price.
- Do not add a duplicate product code unless the requirement is to update the existing product.

These are practical validation recommendations; the manual lists the inventory features but does not prescribe every validation rule.

---

## Program 5 – Role-Based Permission System using HashSet<string>

### Requirement

Use `HashSet<string>` to manage user permissions. Support:

- Role assignment.
- Permission checking.
- Merging permissions.

### Understand the problem

A role can have multiple permissions. For example:

```text
Faculty permissions: Read, Upload, Evaluate
Admin permissions:   Read, Upload, Evaluate, Delete, ManageUsers
```

A permission should not be duplicated inside the same permission set. HashSet is suitable because it stores distinct values.

### Suggested role-to-permission structure

The practical specifically asks to use HashSet<string> for permissions. A Dictionary can optionally map role names to their permission sets if you need to manage multiple roles:

```csharp
Dictionary<string, HashSet<string>> rolePermissions =
    new Dictionary<string, HashSet<string>>();
```

This is an additional organizing structure; the permission collection for each role remains a HashSet<string>.

### Assign a permission

```csharp
HashSet<string> facultyPermissions = new HashSet<string>();

facultyPermissions.Add("Read");
facultyPermissions.Add("Upload");
facultyPermissions.Add("Evaluate");
```

### Check a permission

```csharp
bool canEvaluate = facultyPermissions.Contains("Evaluate");
```

### Merge two permission sets

```csharp
HashSet<string> allPermissions = new HashSet<string>(facultyPermissions);
allPermissions.UnionWith(adminPermissions);
```

The result contains all distinct permissions from both sets.

### Important distinction

A role is a label or category such as Admin or Faculty. A permission is an allowed action such as Read, Update or Delete. Do not confuse a role with an individual permission.

---

## Program 6 – Social Networking using HashSet<T>

### Requirement

Use HashSet<T> to manage user interests and friends. Implement:

- Mutual friends finding.
- Interest-based suggestions.
- Duplicate tag removal.

### A. Manage friends

A user's friend collection can be represented by a HashSet of friend IDs, usernames or another suitable identifier.

Example:

```csharp
HashSet<int> amitFriends = new HashSet<int> { 102, 103, 104 };
HashSet<int> priyaFriends = new HashSet<int> { 103, 104, 105 };
```

### B. Find mutual friends

Mutual friends are present in both friend sets. Use intersection:

```csharp
HashSet<int> mutualFriends = new HashSet<int>(amitFriends);
mutualFriends.IntersectWith(priyaFriends);
```

Result:

```text
103, 104
```

### C. Interest-based suggestions

Suppose users have interest sets:

```csharp
HashSet<string> amitInterests = new HashSet<string>
{
    "Cricket", "Music", "Coding"
};

HashSet<string> rahulInterests = new HashSet<string>
{
    "Music", "Coding", "Travel"
};
```

Their common interests can be found using intersection:

```csharp
HashSet<string> commonInterests = new HashSet<string>(amitInterests);
commonInterests.IntersectWith(rahulInterests);
```

A real suggestion rule might recommend users who share at least one interest. That recommendation rule must be designed by the application; HashSet provides the set operations that help implement it.

### D. Remove duplicate tags

```csharp
List<string> tags = new List<string>
{
    "CSharp", "DotNet", "CSharp", "WebAPI", "DotNet"
};

HashSet<string> uniqueTags = new HashSet<string>(tags);
```

The HashSet contains only distinct tag values. If the output must be returned as a List:

```csharp
List<string> result = uniqueTags.ToList();
```

This example uses LINQ's `ToList()` extension method, so add `using System.Linq;` if implicit/global usings do not already include it.

**Note:** HashSet uniqueness depends on the equality comparer. For strings, case-sensitive comparison is the usual default, so `"CSharp"` and `"csharp"` are different values unless a case-insensitive comparer is supplied.

---

# 13. Common errors and important details

## Mistake 1 – Thinking the List declaration creates Student objects

```csharp
List<Student> students = new List<Student>();
```

This creates an empty List, not a Student object. You must create and add Student objects.

## Mistake 2 – Confusing the class with the variable name

```csharp
List<Student> students = new List<Student>();
```

- `Student` is the element type.
- `students` is the variable that refers to the List object.

## Mistake 3 – Forgetting the generic type

```csharp
List students = new List(); // Non-generic ArrayList-like usage is not the generic List<T> syntax
```

For the generic List, specify a type:

```csharp
List<Student> students = new List<Student>();
```

## Mistake 4 – Accessing an empty List by index

```csharp
List<Student> students = new List<Student>();
Console.WriteLine(students[0].Name); // Error at runtime: no element at index 0
```

Check `Count` first or use a safe search operation.

## Mistake 5 – Using Length on List

```csharp
students.Length // Incorrect for List<T>
```

Use:

```csharp
students.Count
```

For an array, use `Length`.

## Mistake 6 – Confusing Count and Capacity

`Count` is the number of existing elements. `Capacity` is the number of elements the List can hold before growing its internal storage.

## Mistake 7 – Using RemoveAt with an invalid index

Check that the index is between zero and `Count - 1` before using `RemoveAt()`.

## Mistake 8 – Adding a duplicate Dictionary key

```csharp
dictionary.Add(101, student1);
dictionary.Add(101, student2); // Throws ArgumentException
```

Check with `ContainsKey()` or use `TryAdd()` when appropriate.

## Mistake 9 – Reading a missing Dictionary key using the indexer

```csharp
Student student = studentsById[999];
```

If 999 does not exist, a `KeyNotFoundException` is thrown. Use `TryGetValue()` when the key might be missing.

## Mistake 10 – Assuming Dictionary values must be unique

Only keys must be unique. Multiple keys can map to equal or identical values.

## Mistake 11 – Assuming HashSet keeps insertion order

HashSet does not guarantee insertion-order enumeration. Do not depend on the order in which elements are displayed.

## Mistake 12 – Assuming duplicate class objects are detected by their fields

For custom reference types, HashSet and List comparison operations use the type's equality implementation or a supplied comparer. Two separate Student objects with the same field values are not automatically equal unless equality is defined accordingly.

## Mistake 13 – Thinking every List must be static

A List can be a local variable, instance field or static field. The correct choice depends on which methods need to access it and how the application is designed.

## Mistake 14 – Assuming in-memory collections permanently save data

List, Dictionary and HashSet store data in memory. When the application ends, their in-memory contents are lost unless the program saves the data somewhere else (for example, a file or database). Database persistence is not part of these six collection exercises.

---

# 14. Performance and internal behaviour

This section is extra interview preparation. These are typical time-complexity expectations for common operations; actual performance can depend on the element type, comparer, data distribution and runtime implementation.

| Collection | Operation | Typical complexity |
|---|---|---|
| `List<T>` | Access by index | O(1) |
| `List<T>` | Search by value/condition | O(n) |
| `List<T>` | Add at end | Amortized O(1) |
| `List<T>` | Insert/remove near beginning or middle | O(n) because elements may need shifting |
| `Dictionary<TKey,TValue>` | Lookup by key | Average O(1) |
| `Dictionary<TKey,TValue>` | Add/remove by key | Average O(1) |
| `HashSet<T>` | Contains/Add/Remove | Average O(1) |

`O(1)` means the operation is expected to take roughly constant time as the number of elements grows. `O(n)` means the work can grow in proportion to the number of elements.

## 14.1 How does List grow?

A List maintains internal storage with a capacity. When the current capacity is insufficient, it allocates larger storage and copies its existing elements. It does not need to resize on every Add call.

Do not rely on a particular capacity growth factor as a permanent API guarantee.

## 14.2 How do Dictionary and HashSet use hashing?

Dictionary and HashSet use hash codes and equality comparisons to organize and locate values. A hash code helps determine where to look, while equality comparison confirms whether two keys/elements are equal.

A hash code is not guaranteed to be unique. Different values may have the same hash code; the collection handles collisions by checking equality as part of its lookup logic.

## 14.3 Why are stable keys important?

A Dictionary key should not change in a way that changes its hash code or equality behaviour while it is stored in the Dictionary. Immutable key types such as `int` and `string` are common choices because their values do not change after creation.

---

# 15. Practical testing checklist

## Program 1 – Student Records

- [ ] Create the List.
- [ ] Add multiple students.
- [ ] Display all students.
- [ ] Search for an existing student.
- [ ] Search for a missing student.
- [ ] Update an existing student.
- [ ] Delete an existing student.
- [ ] Try deleting a missing student.
- [ ] Test the empty List case.
- [ ] Check duplicate-ID handling if implemented.

## Program 2 – Shopping Cart

- [ ] Add one item.
- [ ] Add more than one item.
- [ ] Display cart details.
- [ ] Remove an existing item.
- [ ] Try removing a missing item.
- [ ] Calculate subtotal.
- [ ] Calculate discount amount.
- [ ] Verify final total.
- [ ] Test quantity and price validation.

## Program 3 – Student Grade Book

- [ ] Add students using unique IDs.
- [ ] Add multiple subjects for a student.
- [ ] Update subject marks.
- [ ] Calculate percentage correctly.
- [ ] Display one student's report.
- [ ] Display reports for all students.
- [ ] Handle a missing student ID.
- [ ] Handle a student with no subject marks.

## Program 4 – Product Catalog

- [ ] Add products with unique codes.
- [ ] Check duplicate product code.
- [ ] Search for an existing product.
- [ ] Search for a missing product.
- [ ] Update stock quantity.
- [ ] Prevent invalid stock changes if applicable.
- [ ] Generate a bill for multiple products.
- [ ] Verify subtotal and final total.

## Program 5 – Role-Based Permissions

- [ ] Create a permission HashSet for a role.
- [ ] Add permissions.
- [ ] Try adding a duplicate permission.
- [ ] Check an existing permission.
- [ ] Check a missing permission.
- [ ] Merge two permission sets.
- [ ] Display the merged distinct permissions.

## Program 6 – Social Networking

- [ ] Create friend sets for two users.
- [ ] Find mutual friends using intersection.
- [ ] Create interest sets.
- [ ] Identify common interests.
- [ ] Apply a clear interest-based recommendation rule.
- [ ] Remove duplicate tags.
- [ ] Verify that set operations produce the expected distinct elements.

---

# 16. Viva and interview questions with answers

Try to answer a question yourself before reading its answer.

## A. Collection fundamentals

### Q1. What is a collection?

A collection is a structure used to store and manage a group of elements or objects.

### Q2. What is a collection class in .NET?

A collection class is a .NET class that provides storage and operations for managing groups of values or objects.

### Q3. Why do we need collection classes?

They provide convenient, reusable operations for storing, searching, adding, removing and organizing multiple elements. They are especially helpful when the number of elements changes or when data must follow a particular organization rule.

### Q4. What is the difference between a class and a collection?

A class defines the structure of one kind of object. A collection manages a group of elements or objects.

### Q5. What is the difference between an array and a List?

An array has a fixed length. `List<T>` is dynamically resizable and provides methods such as Add, Remove and Insert.

### Q6. Does List always use less memory than an array?

No. A List manages internal capacity and may reserve unused space to support growth. It is more convenient for changing sizes, but it is not automatically more memory-efficient.

### Q7. What is a generic collection?

A generic collection specifies the type or types of values it can store, such as `List<Student>` or `Dictionary<int, Student>`.

### Q8. What is the benefit of generic collections?

They provide compile-time type safety and reduce the need for manual casts when values are retrieved.

### Q9. What is a non-generic collection?

A non-generic collection commonly stores values as `object`, allowing different data types but requiring more care with conversions and runtime type checking.

### Q10. Which namespace contains List, Dictionary and HashSet?

`System.Collections.Generic`.

## B. List<T>

### Q11. What is List<T>?

It is a generic, dynamically resizable collection that stores elements in a sequence, supports indexing and allows duplicate values.

### Q12. What does T represent in List<T>?

`T` is a generic type parameter representing the element type stored in the List.

### Q13. Does creating a List<Student> create Student objects?

No. It creates an empty List. Student objects must be created and added separately.

### Q14. What is the difference between Count and Capacity?

Count is the current number of elements. Capacity is the number of elements the List can hold before it must grow its internal storage.

### Q15. What is the difference between Add and Insert?

Add appends an element to the end. Insert places an element at a specified index.

### Q16. What is the difference between Remove and RemoveAt?

Remove deletes the first matching element. RemoveAt deletes the element at a specified index.

### Q17. Does List allow duplicates?

Yes. A List allows duplicate values.

### Q18. What does Find() return if no matching element exists?

It returns the default value for the element type. For a class reference type, that is normally null.

### Q19. What does FindIndex() return if no match is found?

It returns -1.

### Q20. What happens when we access an invalid List index?

An `ArgumentOutOfRangeException` is thrown.

## C. Dictionary<TKey, TValue>

### Q21. What is Dictionary<TKey, TValue>?

It is a generic collection that stores values associated with unique keys.

### Q22. What do TKey and TValue represent?

TKey is the key type and TValue is the value type.

### Q23. Why must Dictionary keys be unique?

A key identifies one entry. Duplicate keys would make lookup by key ambiguous, so the Dictionary prevents duplicate keys.

### Q24. Can Dictionary values be duplicated?

Yes. Different keys can map to equal or even the same value object.

### Q25. What happens when Add() is called with an existing key?

It throws an `ArgumentException` because that key already exists.

### Q26. What happens when we read a missing key using the Dictionary indexer?

A `KeyNotFoundException` is thrown.

### Q27. How can we safely retrieve a value if a key might not exist?

Use `TryGetValue()` and check its Boolean result.

### Q28. What is the difference between Add(key, value) and dictionary[key] = value?

Add throws when the key already exists. Indexer assignment adds the entry if missing or replaces its value if the key exists.

### Q29. Are Dictionary entries stored in insertion order as a guaranteed contract?

Do not rely on Dictionary enumeration order as an insertion-order guarantee. If ordering is required, choose a collection designed for that requirement or sort the data when displaying it.

## D. HashSet<T>

### Q30. What is HashSet<T>?

It is a generic collection that stores distinct elements according to its equality comparer.

### Q31. Does HashSet allow duplicate elements?

No equal duplicate elements are added. `Add()` returns false if an equal element already exists.

### Q32. Does HashSet support index-based access?

No. HashSet is not an indexed sequence like List.

### Q33. Does HashSet guarantee insertion order?

No. Do not depend on enumeration following insertion order.

### Q34. What is UnionWith()?

It adds the distinct elements from another collection to the current set.

### Q35. What is IntersectWith()?

It keeps only the elements that are also in the other collection.

### Q36. What is ExceptWith()?

It removes elements that are also present in the other collection.

### Q37. What is SymmetricExceptWith()?

It keeps elements found in either set but not in both.

### Q38. How does HashSet identify duplicates for a custom class?

It uses equality comparison and hash codes. Unless the class defines value-based equality or a comparer is supplied, reference-type objects commonly use identity-based equality by default.

## E. Static, instance and storage

### Q39. Does every List need to be static?

No. A List can be a local variable, an instance field or a static field.

### Q40. Why might a List be declared as a static field in a simple console example?

If all CRUD methods are static and must directly access one shared List, a static field is a simple way to make that collection accessible to those methods.

### Q41. Can Main use a local List and pass it to other methods?

Yes. Pass the same List reference as a parameter to each method that needs it.

### Q42. If two manager objects each have a non-static List field, do they share the same List?

No. Each manager object has its own instance List field.

### Q43. Does an in-memory List save records permanently?

No. Its data is in memory and is lost when the application ends unless the program saves it to persistent storage.

## F. Scenario-based questions

### Q44. A college manages students and needs easy add/remove operations. Which collection would you select?

`List<Student>` is a suitable choice when an ordered collection with convenient resizing and index access is required.

### Q45. A system needs to retrieve a student using a unique ID. Which collection would you select?

`Dictionary<int, Student>` is suitable because the Student ID can be the key.

### Q46. A role must not contain the same permission twice. Which collection would you select?

`HashSet<string>` is suitable because it stores distinct permission values.

### Q47. How would you find mutual friends?

Represent each user's friends as a set and use intersection (`IntersectWith`) to find common friend identifiers.

### Q48. How would you remove duplicate tags from a List?

Create a HashSet from the List. The HashSet stores distinct values. Convert back to a List if a List result is required.

### Q49. Can we store a Student object directly in a Dictionary?

Yes. For example, `Dictionary<int, Student>` stores Student objects as values and an integer key for each entry.

### Q50. If two Student objects have the same ID, will HashSet<Student> automatically treat them as duplicates?

Not necessarily. It depends on the Student equality implementation or the comparer supplied to the HashSet. Define equality by ID if that is the intended uniqueness rule.

---

# 17. Quick revision sheet

| Term | Remember this |
|---|---|
| Collection | Structure for managing a group of elements. |
| Generic collection | Collection with specified element/key/value types. |
| `List<T>` | Resizable sequence; supports indexes and duplicates. |
| `Dictionary<TKey,TValue>` | Unique keys mapped to values. |
| `HashSet<T>` | Distinct elements; no index-based access. |
| `Count` | Actual number of elements/entries. |
| `Capacity` | List's current storage capacity. |
| `Add()` in List | Appends an element. |
| `Remove()` in List | Removes the first matching element. |
| `RemoveAt()` in List | Removes by index. |
| `Find()` | Finds the first matching element. |
| `TryGetValue()` | Safely retrieves a Dictionary value by key. |
| `ContainsKey()` | Checks whether a Dictionary key exists. |
| `UnionWith()` | Set union. |
| `IntersectWith()` | Set intersection. |
| `ExceptWith()` | Set difference. |
| FIFO | First In, First Out – Queue. |
| LIFO | Last In, First Out – Stack. |
| Static field | Belongs to the type. |
| Instance field | Belongs to an object. |

## The complete Lab 3 learning flow

```text
Understand why multiple records need to be managed
                    |
                    v
            COLLECTION BASICS
                    |
          +---------+---------+
          |         |         |
          v         v         v
        List    Dictionary  HashSet
          |         |         |
          v         v         v
      Sequence   Key-Value  Unique values
          |         |         |
          v         v         v
     Student/Cart Gradebook/  Permissions/
                  Inventory  Social network
```

## Final advice for students

Do not memorize collection method names without understanding the requirement.

First ask:

- Do I need an ordered sequence? Consider `List<T>`.
- Do I need to retrieve a value using a unique key? Consider `Dictionary<TKey, TValue>`.
- Do I need to prevent equal duplicate values and perform set operations? Consider `HashSet<T>`.

The central idea of Lab 3 is **choosing the correct way to manage data**, not simply learning three syntax patterns.

---

**End of Lab 3 – Collection Classes**
