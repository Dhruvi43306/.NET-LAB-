# Lab 4 – Create Controllers and Action Methods and Implement CRUD APIs

> **Course:** Building RESTful APIs with ASP.NET Core  
> **Practical:** Lab 4  
> **Main topics:** Model classes, navigation through a list of related objects, in-memory data, ASP.NET Core controllers, action methods, routing, HTTP GET, and `404 Not Found`.

This README follows the Lab 4 requirements in the supplied laboratory manual. The manual asks you to:



## 1. What are we building?

We are building a small Student Management REST API.

Imagine the college has five students. Each student has their own details and can have multiple subjects. The API should allow a client, such as Swagger UI, Postman, or a frontend application, to request:

- The complete student list.
- One student's information using their ID.
- The subject list belonging to one particular student.

For this lab, data is stored in application memory. We are **not using a database**.

### Understand the relationship first

```text
Student
│
├── StudentId
├── Name
├── Age
└── Subjects (List<Subject>)
    │
    ├── Subject 1
    │   ├── SubjectId
    │   └── SubjectName
    │
    ├── Subject 2
    │   ├── SubjectId
    │   └── SubjectName
    │
    └── Subject 3
        ├── SubjectId
        └── SubjectName
```

**Important:** One student can have many subjects. Therefore, `Student` contains a collection of `Subject` objects. This is an example of an object containing a collection of related objects.

---

## 2. Important concepts before coding

### 2.1 Model class

A model class describes the shape of the data used by the application.

For this practical:

- `Student` describes one student.
- `Subject` describes one subject.

A model class does not automatically store all student records. It defines the structure of one student object.

### 2.2 Object

An object is an instance of a class. For example, one `Student` object represents one actual student record.

```csharp
Student student = new Student();
```

### 2.3 List of objects

A `List<Student>` can hold multiple Student objects. The list in this lab is used to hold the five preloaded students.

```csharp
List<Student> students = new List<Student>();
```

### 2.4 List inside a class

The Student class has a property whose type is `List<Subject>`:

```csharp
public List<Subject> Subjects { get; set; } = new List<Subject>();
```

This means each Student object can hold multiple Subject objects.

### 2.5 In-memory data

In-memory data is held in application memory while the application is running. It is not automatically saved to a file or database. When the application stops and starts again, the sample data is recreated from the code.

### 2.6 Static list

The manual specifically asks for a **static in-memory `List<Student>`**.

A static field belongs to the type rather than to an individual controller object. ASP.NET Core can create controller instances to handle requests; the static list provides one shared list for those controller instances during the application's process lifetime.

`static` does **not** mean that data is permanently saved. It is still in memory and is lost when the application process stops.

### 2.7 Controller

A controller receives HTTP requests, calls the relevant action method, and returns an HTTP response.

In this lab, `StudentsController` handles student-related GET requests.

### 2.8 Action method

An action method is a public method in a controller that ASP.NET Core can execute for a matching request.

Examples in this lab:

- `GetAllStudents()`
- `GetStudentById(int id)`
- `GetStudentSubjects(int id)`

### 2.9 Routing

Routing maps an incoming URL and HTTP method to a controller action.

For example:

```csharp
[Route("api/[controller]")]
```

When the controller is named `StudentsController`, `[controller]` becomes `students` (the `Controller` suffix is omitted).

---

## 3. Project structure

Create an ASP.NET Core Web API project. A simple structure for this practical is:

```text
StudentManagementAPI/
│
├── Controllers/
│   └── StudentsController.cs
│
├── Models/
│   ├── Student.cs
│   └── Subject.cs
│
└── Program.cs
```

### Responsibility of each file

| File | Responsibility |
|---|---|
| `Student.cs` | Defines the student data and its list of subjects. |
| `Subject.cs` | Defines the subject data. |
| `StudentsController.cs` | Holds the sample in-memory student records and exposes the GET endpoints. |
| `Program.cs` | Registers controllers and maps controller routes. |

Keep this lab simple. It does not require a database, Entity Framework Core, repository, service layer, DTOs, or authentication.

---

## 4. Create the model classes

The field names below are example choices for demonstrating the required relationship. The manual requires a Student class with a list of Subject objects, but it does not prescribe exact property names or fields.

### 4.1 Models/Subject.cs

```csharp
namespace StudentManagementAPI.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = string.Empty;
    }
}
```

**Understand this class:**

- `SubjectId` identifies a subject in this example.
- `SubjectName` stores the subject's name.
- `{ get; set; }` declares a C# property.
- `string.Empty` gives the string property a non-null initial value.

### 4.2 Models/Student.cs

```csharp
using System.Collections.Generic;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
```

**Understand the important line:**

```csharp
public List<Subject> Subjects { get; set; } = new List<Subject>();
```

Break it into parts:

| Part | Meaning |
|---|---|
| `List` | A collection that can store multiple elements. |
| `<Subject>` | The collection stores Subject objects. |
| `Subjects` | The property name. |
| `new List<Subject>()` | Creates an empty list for this Student object. |

Each Student object receives its own `Subjects` list because this is an instance property initialized on each object.

---

## 5. Create StudentsController.cs

The controller below preloads five sample students. Every student has a list of subjects.

```csharp
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // One shared in-memory list containing five sample students.
        private static readonly List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 101,
                Name = "Amit",
                Age = 20,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "C#" },
                    new Subject { SubjectId = 2, SubjectName = "Database" }
                }
            },
            new Student
            {
                Id = 102,
                Name = "Priya",
                Age = 21,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 3, SubjectName = "Java" },
                    new Subject { SubjectId = 4, SubjectName = "Python" }
                }
            },
            new Student
            {
                Id = 103,
                Name = "Rahul",
                Age = 22,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 5, SubjectName = "ASP.NET Core" },
                    new Subject { SubjectId = 6, SubjectName = "C#" }
                }
            },
            new Student
            {
                Id = 104,
                Name = "Neha",
                Age = 20,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 7, SubjectName = "HTML" },
                    new Subject { SubjectId = 8, SubjectName = "CSS" }
                }
            },
            new Student
            {
                Id = 105,
                Name = "Jay",
                Age = 21,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 9, SubjectName = "JavaScript" },
                    new Subject { SubjectId = 10, SubjectName = "React" }
                }
            }
        };

      
```

### Understand the controller step by step

#### A. `[ApiController]`

```csharp
[ApiController]
```

This marks the class as an API controller and enables API-specific behaviours such as automatic handling of model-validation errors when applicable. The three GET actions in this lab do not receive request-body models.

#### B. `[Route("api/[controller]")]`

```csharp
[Route("api/[controller]")]
```

This defines the common route prefix. Since the controller is named `StudentsController`, the route prefix is:

```text
api/students
```

#### C. `ControllerBase`

```csharp
public class StudentsController : ControllerBase
```

`ControllerBase` provides API controller functionality, including methods such as `Ok()` and `NotFound()`. It does not include MVC view support, which is not needed for this Web API practical.

#### D. Why is the list static?

```csharp
private static readonly List<Student> students = new List<Student>
```

- `private`: only the controller class can access the field directly.
- `static`: the list belongs to the controller type and is shared by its instances.
- `readonly`: the field cannot be assigned to a different List after initialization. The List's contents can still be changed.
- `List<Student>`: it stores Student objects.

The five records are initialized when the type is initialized. The list is in memory, not in a database.

#### E. Why use `FirstOrDefault()`?

```csharp
Student? student = students.FirstOrDefault(s => s.Id == id);
```

This searches the list for the first student whose `Id` matches the requested `id`.

- If a matching student exists, the Student object is returned.
- If there is no match, the result is `null`.

The `?` in `Student?` indicates that the variable may hold `null` when nullable reference types are enabled.

#### F. Why check `student == null`?

If no record is found, the API must not try to access properties of a missing student. The Lab 4 requirement explicitly says to return `404 Not Found` when the requested student does not exist.

#### G. Why use `Ok()`?

```csharp
return Ok(student);
```

`Ok()` returns HTTP `200 OK` and includes the supplied object in the response body. The same method is used to return the complete student list or a student's subjects.

---



### What do these lines do?

| Code | Purpose |
|---|---|
| `WebApplication.CreateBuilder(args)` | Creates the application builder. |
| `AddControllers()` | Registers controller-related services. |
| `builder.Build()` | Builds the web application. |
| `MapControllers()` | Enables attribute-routed controller endpoints. |
| `Run()` | Starts the application. |

If your project already has Swagger/OpenAPI or Scalar configuration, keep that existing configuration. The important controller lines are `AddControllers()` and `MapControllers()`.

---
## 11. Interview and viva preparation

### Basic questions

**Q1. What is a model class?**  
A model class defines the structure of data used by an application. In this practical, Student and Subject are model classes.

**Q2. What is a controller in ASP.NET Core Web API?**  
A controller handles incoming HTTP requests and returns HTTP responses.

**Q3. What is an action method?**  
An action method is a controller method that handles a matching HTTP request.

**Q4. What is routing?**  
Routing maps an incoming URL and HTTP method to an endpoint or controller action.

**Q5. What does `[ApiController]` do?**  
It marks a controller as an API controller and enables API-specific behaviours, including automatic responses for model-validation errors when applicable.

**Q6. Why does the controller inherit from `ControllerBase`?**  
`ControllerBase` provides API-oriented features such as `Ok()` and `NotFound()` without requiring view support.

### Student and Subject relationship

**Q7. Why does Student contain `List<Subject>`?**  
Because one student can have multiple subjects. The list allows each Student object to hold multiple Subject objects.

**Q8. What does `List<Subject>` mean?**  
It is a generic list whose elements are Subject objects.

**Q9. Does `new List<Subject>()` create Subject objects?**  
No. It creates an empty list. Subject objects must be created and added to it separately.

**Q10. Can two students have different subjects?**  
Yes. Each Student object has its own Subjects list and can contain different Subject objects.

### Static and in-memory data

**Q11. Why is the student list static in this lab?**  
The lab specifically asks for a static in-memory list. A static field is shared by controller instances rather than creating a separate list for each controller object.

**Q12. Does static mean data is stored permanently?**  
No. Static controls member ownership and sharing. The data remains in application memory and is lost when the application process stops.

**Q13. What is the meaning of `readonly` on the static list field?**  
It prevents the field from being assigned to a different List after initialization. It does not prevent adding or removing elements from the existing list.

**Q14. Why are we not using a database?**  
This practical asks for a static in-memory list so students can focus on models, controllers, action methods, routing, and HTTP responses before database integration is introduced in later labs.

### HTTP and action methods

**Q15. What is HTTP GET used for?**  
GET is used to retrieve a resource or collection of resources from the server.

**Q16. What does `Ok()` return?**  
It returns HTTP `200 OK`, normally with the supplied data in the response body.

**Q17. What does `NotFound()` return?**  
It returns HTTP `404 Not Found`.

**Q18. Why should GetStudentById return 404 if a student does not exist?**  
Because the requested resource cannot be found in the student list.

**Q19. What is `IActionResult`?**  
It is an interface used as an action method return type when an action may return different HTTP result types, such as `Ok()` or `NotFound()`.

**Q20. What does `[HttpGet("{id:int}")]` mean?**  
It maps a GET request with an integer route segment to that action. For example, `/api/students/101` supplies `101` as the `id` parameter.

**Q21. What does `[HttpGet("{id:int}/subjects")]` mean?**  
It maps a GET request such as `/api/students/101/subjects` to the action that returns the subjects for student 101.

**Q22. Why use `FirstOrDefault()`?**  
It returns the first item that matches a condition, or the default value (`null` for a reference type) if no match is found.

**Q23. What is the purpose of `s => s.Id == id`?**  
It is a lambda expression used as a condition. It checks whether the current student's ID matches the requested ID.

**Q24. What is the difference between `First()` and `FirstOrDefault()`?**  
`First()` throws an exception if no matching element exists. `FirstOrDefault()` returns the default value if there is no match.

**Q25. What happens if the application is restarted?**  
The static in-memory list is initialized again from the sample data in the source code. Runtime changes are lost.

### Scenario-based questions

**Q26. A client sends `GET /api/students/101`. Student 101 exists. What should happen?**  
The API returns `200 OK` with that student's details.

**Q27. A client sends `GET /api/students/999`, but ID 999 is not in the list. What should happen?**  
The API returns `404 Not Found`.

**Q28. Student 101 exists and has three subjects. What should `GET /api/students/101/subjects` return?**  
It should return `200 OK` with the three Subject objects belonging to Student 101.

**Q29. Student 999 does not exist. What should `GET /api/students/999/subjects` return?**  
It should return `404 Not Found`, because the student whose subjects were requested does not exist.

**Q30. Why should the API check whether a student exists before accessing `student.Subjects`?**  
If the student was not found, the result is `null`. Checking first prevents an attempt to access a property on a null reference.

---

## 12. Quick revision sheet

| Concept | Remember |
|---|---|
| `Student` | Model representing one student. |
| `Subject` | Model representing one subject. |
| `List<Subject>` | Holds multiple Subject objects for one student. |
| `List<Student>` | Holds multiple Student objects. |
| `static` list | One shared list for controller instances in the running process. |
| In-memory data | Stored in application memory, not permanently persisted. |
| Controller | Receives HTTP requests and produces responses. |
| Action method | Handles a particular request. |
| `[ApiController]` | Marks a class as an API controller. |
| `[Route]` | Defines a route template. |
| `[HttpGet]` | Maps an action to HTTP GET. |
| `Ok()` | Returns `200 OK`. |
| `NotFound()` | Returns `404 Not Found`. |
| `FirstOrDefault()` | Finds a matching element or returns the default value. |

### Lab 4 implementation checklist

- [ ] Create an ASP.NET Core Web API project.
- [ ] Create `Student.cs`.
- [ ] Create `Subject.cs`.
- [ ] Add `List<Subject>` to Student.
- [ ] Preload five students with their subjects.
- [ ] Store them in a static in-memory `List<Student>`.
- [ ] Create `StudentsController`.
- [ ] Implement GET all students.
- [ ] Implement GET student by ID.
- [ ] Return `404 Not Found` when a student does not exist.
- [ ] Implement GET subjects for a student.
- [ ] Register controllers and map controller routes.
- [ ] Test all three GET endpoints in Swagger or Postman.

---

## 13. Final concept summary

The main learning progression in Lab 4 is:

```text
Student and Subject model classes
                ↓
Student contains List<Subject>
                ↓
Create five Student objects with their subjects
                ↓
Store all five in a static List<Student>
                ↓
Create StudentsController
                ↓
Create GET action methods
                ↓
Return 200 OK when data is found
                ↓
Return 404 Not Found when a requested student is missing
```

**The most important thing to understand:** A Student is one object. A Student can contain multiple Subject objects through `List<Subject>`. The static `List<Student>` holds the five sample student records. The controller exposes GET endpoints so a client can retrieve all students, one student, or that student's subjects.

If you can explain this complete flow without memorizing the code, you understand the core concepts of Lab 4.
