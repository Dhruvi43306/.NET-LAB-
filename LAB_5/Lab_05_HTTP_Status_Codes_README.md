# Lab 5 – Working with HTTP Status Codes

> **Course:** Building RESTful APIs with ASP.NET Core  
> **Lab topic:** Working with HTTP Status Codes  
> **Technology:** C#, ASP.NET Core Web API  
> **Data storage for this practical:** In-memory `List<Student>` (no database required)

---



The main objective is **not simply to return a response**. It is to return a response that accurately explains the result of the request to the client.

## 2. What Is an HTTP Status Code?

An HTTP status code is a three-digit number sent by a server in an HTTP response. It tells the client the result of the request.

For example, a frontend or Postman sends:

```http
GET /api/students/101
```

The API checks whether Student ID `101` exists.

- If the student exists, the API returns `200 OK` and the student data.
- If the student does not exist, the API returns `404 Not Found`.

The status code helps the client understand what happened without guessing from the response message.

### Request–response flow

```text
Client (Browser / Postman / Frontend)
                 |
                 | HTTP Request
                 v
       ASP.NET Core Controller
                 |
                 | Validate input / perform operation
                 v
          HTTP Response
          - Status code
          - Optional response body
          - Optional headers
                 |
                 v
               Client
```

## 3. Understand the Three Status Code Groups Used in This Lab

HTTP status codes are grouped by their first digit:

| Range | Category | General meaning |
|---|---|---|
| `2xx` | Success | The request was successfully processed. |
| `4xx` | Client error | The request cannot be fulfilled because of something about the request or its target. |

This lab uses success responses (`200`, `201`, `204`) and client-error responses (`400`, `404`, `409`).

---

## 4. The Six Required Status Codes

### 4.1 `200 OK` – Successful request

**Meaning:** The request was successfully processed.

Use it for successful GET requests that return data.

Examples:

- Fetch all students.
- Fetch one existing student by ID.
- Fetch the subjects of an existing student.

Controller response:

```csharp
return Ok(students);
```

For one student:

```csharp
return Ok(student);
```

**Important:** If a GET request asks for one particular student and that student does not exist, return `404 Not Found`, not `200 OK` with an error message.

### 4.2 `201 Created` – A new resource was created

**Meaning:** The server successfully created a new resource.

Use it after a successful POST request that adds a new student.

Simple response:

```csharp
return StatusCode(StatusCodes.Status201Created, student);
```

This returns status code `201` and includes the created student in the response body.

A more RESTful option is `CreatedAtAction()`. It returns `201 Created` and also provides a `Location` header that points to the GET endpoint for the newly created student:

```csharp
return CreatedAtAction(
    nameof(GetStudentById),
    new { id = student.Id },
    student
);
```

For this lab, understand the status code first. Both approaches can return `201 Created`; `CreatedAtAction()` additionally supplies the resource location.

### 4.3 `204 No Content` – Successful request with no response body

**Meaning:** The request was successfully processed, and the server has no response content to send.

The lab requires it for successful PUT and DELETE operations.

After a successful update:

```csharp
return NoContent();
```

After a successful deletion:

```csharp
return NoContent();
```

**Important:** A `204` response must not contain a response body. Do not return `NoContent()` and then try to attach a message or student object.

### 4.4 `400 Bad Request` – Invalid input

**Meaning:** The client sent input that the API cannot accept.

Student examples:

- Student ID is zero or negative.
- Student name is empty.
- Age is zero or negative, according to the validation rule chosen for this practical.

Controller response:

```csharp
return BadRequest("Please enter valid student details.");
```

A `400` response means the request data is invalid. It does not mean that the student record is missing.

### 4.5 `404 Not Found` – Requested student does not exist

**Meaning:** The requested resource could not be found.

Example: the client requests Student ID `999`, but there is no student with that ID.

Controller response:

```csharp
return NotFound("Student not found.");
```

Use `404` when the requested student is missing. Do not use `400` just because the ID does not match an existing record; the request can be correctly formed while the resource is absent.

### 4.6 `409 Conflict` – Duplicate record

**Meaning:** The request conflicts with the current state of the data.

Student example: Student ID `101` already exists, and the client tries to create another student with ID `101`.

Controller response:

```csharp
return Conflict("Student ID already exists.");
```

A duplicate ID is a conflict with an existing record, so the lab requires `409 Conflict`.

---

## 5. HTTP Methods and Their Successful Status Codes

| HTTP method | Typical Student API operation | Successful response required by this lab |
|---|---|---:|
| GET | Get all students | `200 OK` |
| GET | Get a student by ID | `200 OK` when found |
| GET | Get a student's subjects | `200 OK` when student is found |
| POST | Create a student | `201 Created` |
| PUT | Update a student | `204 No Content` |
| DELETE | Delete a student | `204 No Content` |

The success code is not the only possible response. An endpoint can return a different status code when its operation cannot be completed.

## 6. Understand the ASP.NET Core Response Methods

In a controller that inherits from `ControllerBase`, ASP.NET Core provides helper methods for common HTTP responses.

| ASP.NET Core method | HTTP status | What it does |
|---|---:|---|
| `Ok(data)` | 200 | Returns success with a response body. |
| `StatusCode(201, data)` | 201 | Returns the specified status code and response body. |
| `CreatedAtAction(...)` | 201 | Returns created response and a Location header for another action. |
| `NoContent()` | 204 | Returns success without a response body. |
| `BadRequest(message)` | 400 | Returns a bad-request response. |
| `NotFound(message)` | 404 | Returns a not-found response. |
| `Conflict(message)` | 409 | Returns a conflict response. |

### What is `IActionResult`?

`IActionResult` is a return type that allows a controller action to return different kinds of HTTP responses.

For example, the same GET action can return either `Ok(student)` or `NotFound(...)`:


## 7. Practical Preparation – Student API Model

The earlier Student Management API practical uses a `Student` class and an in-memory collection. The following simplified model is enough to understand status-code handling.

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
```

An in-memory collection can be declared in the controller:

```csharp
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
            new Subject { SubjectId = 1, SubjectName = "Java" },
            new Subject { SubjectId = 2, SubjectName = "Python" }
        }
    },

    new Student
    {
        Id = 103,
        Name = "Rahul",
        Age = 22
    },

    new Student
    {
        Id = 104,
        Name = "Neha",
        Age = 20
    },

    new Student
    {
        Id = 105,
        Name = "Jay",
        Age = 21
    }
};
```

This is only for practising API behaviour. It is not permanent storage. The records reset when the application restarts.

---



Execution order:

1. Validate the submitted student details.
2. If the details are invalid, return `400`.
3. Check whether the ID already exists.
4. If the ID is a duplicate, return `409`.
5. Add the student to the List.
6. Return `201` with the created student.

This example uses `StatusCode(201, student)` as a simple beginner-friendly response. In a REST API where the GET-by-ID action is available, `CreatedAtAction()` is a useful alternative because it also returns the URL of the newly created resource.


## 11. Important Differences for Exams and Interviews

### 11.1 `400 Bad Request` vs `404 Not Found`

| `400 Bad Request` | `404 Not Found` |
|---|---|
| Request input is invalid. | Requested resource does not exist. |
| Example: empty student name. | Example: Student ID 999 is absent. |

### 11.2 `404 Not Found` vs `409 Conflict`

| `404 Not Found` | `409 Conflict` |
|---|---|
| The requested resource cannot be found. | The requested operation conflicts with existing data. |
| Example: GET Student ID 999. | Example: POST a student with an ID already in use. |

### 11.3 `200 OK` vs `201 Created`

| `200 OK` | `201 Created` |
|---|---|
| Request completed successfully. | A new resource was created successfully. |
| Common example: GET student. | Common example: POST new student. |

### 11.4 `200 OK` vs `204 No Content`

| `200 OK` | `204 No Content` |
|---|---|
| Successful response; may include a body. | Successful response; no body is returned. |
| Common example: GET student. | Common example: PUT or DELETE in this lab. |

---

## 12. Viva Questions and Answers

### Basic Questions

**Q1. What is an HTTP status code?**  
An HTTP status code is a three-digit response code that communicates the outcome of an HTTP request.

**Q2. Why do we use HTTP status codes in Web APIs?**  
They allow clients to understand whether a request succeeded or why it could not be completed.

**Q3. What does 200 OK mean?**  
The request was successfully processed. In this lab, it is used for successful GET requests.

**Q4. What does 201 Created mean?**  
The server successfully created a new resource.

**Q5. What does 204 No Content mean?**  
The request succeeded, but the response has no body.

**Q6. What does 400 Bad Request mean?**  
The request contains invalid input or cannot be processed because of a client-side request problem.

**Q7. What does 404 Not Found mean?**  
The requested resource does not exist at the requested location.

**Q8. What does 409 Conflict mean?**  
The request conflicts with the current state of the resource, such as trying to create a duplicate Student ID.

### ASP.NET Core Questions

**Q9. What is `IActionResult`?**  
It is an ASP.NET Core return type that allows a controller action to return different HTTP action results.

**Q10. What does `Ok()` return?**  
It creates an HTTP 200 response, normally with the supplied data in the response body.

**Q11. What does `BadRequest()` return?**  
It creates an HTTP 400 response.

**Q12. What does `NotFound()` return?**  
It creates an HTTP 404 response.

**Q13. What does `Conflict()` return?**  
It creates an HTTP 409 response.

**Q14. What does `NoContent()` return?**  
It creates an HTTP 204 response with no response body.

**Q15. What is the purpose of `CreatedAtAction()`?**  
It creates an HTTP 201 response and can generate a Location header pointing to another controller action, usually the GET endpoint for the new resource.

**Q16. Can one action method return different status codes?**  
Yes. For example, a GET-by-ID action can return `200 OK` if the student exists or `404 Not Found` if the student does not exist.

### Scenario-Based Questions

**Q17. A user requests all students and the List is empty. Which status code can the API return?**  
`200 OK` with an empty collection. The collection endpoint exists and the request succeeded.

**Q18. A user requests Student ID 500, but it does not exist. Which code should be returned?**  
`404 Not Found`.

**Q19. A user submits a student with an empty name. Which code should be returned under the manual validation rule?**  
`400 Bad Request`.

**Q20. A user submits a Student ID that already exists. Which code should be returned?**  
`409 Conflict`.

**Q21. A new student is created successfully. Which code should be returned?**  
`201 Created`.

**Q22. A student is updated successfully, and the API does not need to return the updated object. Which code is required by this lab?**  
`204 No Content`.

**Q23. A student is deleted successfully. Which code is required by this lab?**  
`204 No Content`.

**Q24. Can a GET endpoint return 404?**  
Yes. A GET endpoint that retrieves a particular student should return 404 when that student does not exist.

**Q25. Why should we check for duplicate IDs before adding a student?**  
To prevent duplicate student records and return the required `409 Conflict` response.

**Q26. Why should a 204 response not contain a message?**  
Because `204 No Content` indicates that the response has no body.

**Q27. Is 404 the correct response if a student exists but has no subjects?**  
No. If the student exists, the subjects endpoint can return `200 OK` with an empty list.

**Q28. What is the difference between a status code and a response message?**  
The status code communicates the protocol-level outcome. A response message provides human-readable detail. A message does not replace the correct status code.

**Q29. Does `students.Add(student)` return HTTP 201?**  
No. `List<T>.Add()` only adds an item to the List and returns `void`. The controller must separately return an HTTP response such as `StatusCode(201, student)`.

**Q30. Does returning 201 automatically save the student permanently?**  
No. The status code only communicates the HTTP result. If the application uses an in-memory List, the data is lost when the application stops.

---

## 13. Quick Revision Table

Use this table before a practical exam or viva.

| Situation | Status | ASP.NET Core response helper |
|---|---:|---|
| GET succeeds | 200 | `Ok(data)` |
| POST creates student | 201 | `StatusCode(201, student)` or `CreatedAtAction(...)` |
| PUT succeeds | 204 | `NoContent()` |
| DELETE succeeds | 204 | `NoContent()` |
| Invalid input | 400 | `BadRequest(message)` |
| Student not found | 404 | `NotFound(message)` |
| Duplicate student | 409 | `Conflict(message)` |

### Easy way to remember

```text
200 – Successfully processed
201 – New resource created
204 – Successfully processed, no response body
400 – Invalid request data
404 – Requested student not found
409 – Request conflicts with existing data
```

---

## 14. Practical Completion Checklist

Use this checklist after implementing the Student Management API.

- [ ] GET all students returns `200 OK`.
- [ ] GET an existing student returns `200 OK`.
- [ ] GET a missing student returns `404 Not Found`.
- [ ] GET subjects for an existing student returns `200 OK`.
- [ ] POST valid new student returns `201 Created`.
- [ ] POST invalid student details returns `400 Bad Request`.
- [ ] POST duplicate Student ID returns `409 Conflict`.
- [ ] PUT an existing student returns `204 No Content`.
- [ ] PUT invalid student details returns `400 Bad Request`.
- [ ] PUT a missing student returns `404 Not Found`.
- [ ] DELETE an existing student returns `204 No Content`.
- [ ] DELETE a missing student returns `404 Not Found`.
- [ ] Successful 204 responses contain no response body.
- [ ] The correct status code is visible in Postman or Swagger.

---

## 15. Final Concept to Remember

Do not memorize status codes only by HTTP method. Understand the outcome of each operation.

For example, POST usually creates a resource and returns `201 Created`, but if the input is invalid, it should return `400 Bad Request`; if the student ID already exists, it should return `409 Conflict`.

The purpose of this lab is to make your API responses meaningful, consistent and understandable to the client.

> **A good API does not only perform an operation correctly. It also communicates the result of that operation using the correct HTTP status code.**
