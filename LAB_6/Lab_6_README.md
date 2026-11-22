# Lab 6 -- Entity Framework Core: Entities, Primary Keys, Foreign Keys and Relationships

## 1. Lab Objective

In this lab, we use Entity Framework Core (EF Core) to describe the
database structure of the CourseHub application in C#.

We will work with five entities:

-   `Role`
-   `User`
-   `Course`
-   `Assignment`
-   `Submission`

The main purpose is to understand how C# classes are connected to
database tables, how primary keys identify records, how foreign keys
connect tables, and how EF Core is configured to use SQL Server.

**Important:** This README explains the current Lab 6 implementation. To
make the explanation easy to follow, the `Role` and `User` relationship
is used as the main example for explaining a primary key, foreign key,
navigation properties, and relationship configuration. The same ideas
are applied to the other entities in the project.

------------------------------------------------------------------------

## 2. NuGet Packages Required

A NuGet package is a reusable library that adds functionality to a .NET
project. EF Core is provided through several packages. Each package has
a separate responsibility.

### 2.1 `Microsoft.EntityFrameworkCore`

**Purpose:** Provides the main EF Core functionality.

This package contains important types used in the lab:

  -----------------------------------------------------------------------
  Type                                Purpose
  ----------------------------------- -----------------------------------
  `DbContext`                         Base class for the application's
                                      database context.

  `DbSet<TEntity>`                    Represents the set of entities that
                                      can be queried and saved.

  `ModelBuilder`                      Used to configure entity mappings
                                      and relationships.

  `DeleteBehavior`                    Defines what happens when a
                                      principal record is deleted.
  -----------------------------------------------------------------------

**Where is it used in our code?**

``` csharp
public class CourseHubDbContext : DbContext
{
    // DbContext implementation
}
```

Here, `CourseHubDbContext` inherits from `DbContext`, which is provided
by EF Core.

We also use `DbSet<Role>`, `DbSet<User>`, and the other `DbSet`
properties to expose the entities to EF Core.

### 2.2 `Microsoft.EntityFrameworkCore.SqlServer`

**Purpose:** Adds the SQL Server database provider for EF Core.

EF Core itself is not tied to one database. A provider tells EF Core how
to communicate with a particular database system. This package allows EF
Core to work with Microsoft SQL Server.

**Where is it used in our code?**

``` csharp
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
);
```

`UseSqlServer()` is provided by the SQL Server provider package. It
tells EF Core that this application uses SQL Server.

Without the SQL Server provider, the project cannot use `UseSqlServer()`
to configure this database provider.

### 2.3 `Microsoft.EntityFrameworkCore.Tools`

**Purpose:** Provides EF Core tools for Visual Studio's Package Manager
Console.

It is used for commands such as:

-   `Add-Migration` -- creates a migration based on changes to the EF
    Core model.
-   `Update-Database` -- applies pending migrations to the database.
-   `Remove-Migration` -- removes the last migration if it has not been
    applied.

Example Package Manager Console commands:

``` powershell
Add-Migration InitialCreate
Update-Database
```

These are development-time commands. The package does not create entity
classes or configure relationships automatically in our C# code.

### 2.4 `Microsoft.EntityFrameworkCore.Design` -- when is it needed?

This package provides design-time services used by EF Core tooling.
Install it when you use the .NET CLI migration commands, such as
`dotnet ef migrations add`.

``` powershell
Install-Package Microsoft.EntityFrameworkCore.Design
```

For the Visual Studio Package Manager Console workflow,
`Microsoft.EntityFrameworkCore.Tools` is the main tools package. If the
project uses the `dotnet ef` CLI, install the Design package as well.

### 2.5 Install the packages

Open **Tools → NuGet Package Manager → Package Manager Console** in
Visual Studio and run:

``` powershell
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```

If using `dotnet ef`, also run:

``` powershell
Install-Package Microsoft.EntityFrameworkCore.Design
```

Use compatible versions of the EF Core packages. Keep the EF Core
package versions aligned with one another and with the project's target
framework.

------------------------------------------------------------------------

## 3. Project Structure

The relevant files for this lab are organised as follows:

``` text
WebApplication3
│
├── Data
│   └── CourseHubDbContext.cs
│
├── Models
│   ├── Role.cs
│   ├── User.cs
│   ├── Course.cs
│   ├── Assignment.cs
│   └── Submission.cs
│
├── appsettings.json
└── Program.cs
```

  -----------------------------------------------------------------------
  File/folder                         Responsibility
  ----------------------------------- -----------------------------------
  `Models`                            Contains the entity classes that
                                      represent database tables.

  `Role.cs`, `User.cs`, etc.          Define the columns and
                                      relationships for each entity.

  `CourseHubDbContext.cs`             Connects the entity model to EF
                                      Core and configures keys and
                                      relationships.

  `appsettings.json`                  Stores the SQL Server connection
                                      string.

  `Program.cs`                        Registers the DbContext and
                                      configures the SQL Server provider.
  -----------------------------------------------------------------------

------------------------------------------------------------------------

## 4. Entity Classes -- What Does an Entity Represent?

An **entity** is a C# class that EF Core maps to a database table.

For example, the `Role` class represents the `Roles` table. Its
properties represent the data stored in that table.

The five entities in this lab represent:

  -----------------------------------------------------------------------
  Entity                              Database purpose
  ----------------------------------- -----------------------------------
  `Role`                              Stores application roles such as
                                      Admin, Faculty, and Student.

  `User`                              Stores the users of the
                                      application.

  `Course`                            Stores the courses offered in the
                                      application.

  `Assignment`                        Stores assignments belonging to
                                      courses.

  `Submission`                        Stores students' assignment
                                      submissions.
  -----------------------------------------------------------------------

### 4.1 Scalar properties

A scalar property stores an individual value.

Examples from the current model:

``` csharp
public int UserId { get; set; }
public string FullName { get; set; } = string.Empty;
public string Email { get; set; } = string.Empty;
public bool IsActive { get; set; }
```

These properties store values such as an ID, name, email address, or
active status.

### 4.2 Navigation properties

A navigation property represents a relationship with another entity.

For example, in the `User` entity:

``` csharp
public Role Role { get; set; } = null!;
```

This property represents the `Role` object associated with the user. It
does not store the role name directly. It allows the application to
navigate from a user to the related role.

A collection navigation property represents multiple related records:

``` csharp
public ICollection<User> Users { get; set; } = new List<User>();
```

This property represents the collection of users that belong to a role.

**Remember:**

-   `RoleId` is an FK value stored in the `User` table.
-   `Role` is a navigation property representing the related `Role`
    object.
-   `Role.Users` is a collection navigation representing multiple users.

------------------------------------------------------------------------

## 5. Primary Key -- Explanation Using the Role Table

A **Primary Key (PK)** uniquely identifies each record in a database
table.

Consider this example `Roles` table:

    RoleId (PK) RoleName
  ------------- ----------
              1 Admin
              2 Faculty
              3 Student

Here, `RoleId` is the primary key because each role record has a unique
ID.

### 5.1 Role entity code

The relevant part of the current `Role` entity is:

``` csharp
public class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}
```

Explanation:

-   `RoleId` stores the identifier for each role.
-   `RoleName` stores the role's name.
-   `Users` is a collection navigation property. It represents all users
    assigned to this role.

### 5.2 Configure the primary key in DbContext

In the current implementation, the primary key is configured manually
using Fluent API inside `OnModelCreating()`:

``` csharp
modelBuilder.Entity<Role>()
    .HasKey(r => r.RoleId);
```

Understand the statement one part at a time:

1.  `modelBuilder` is used to configure the EF Core model.
2.  `Entity<Role>()` selects the `Role` entity for configuration.
3.  `HasKey()` tells EF Core which property is the primary key.
4.  `r => r.RoleId` selects the `RoleId` property.

**Why are we using `HasKey()`?**

The current lab implementation configures primary keys in the DbContext
instead of adding a `[Key]` attribute to each entity. This keeps
primary-key configuration in one place.

The same approach is used for the other entities:

``` csharp
modelBuilder.Entity<User>().HasKey(u => u.UserId);
modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
modelBuilder.Entity<Assignment>().HasKey(a => a.AssignmentId);
modelBuilder.Entity<Submission>().HasKey(s => s.SubmissionId);
```

------------------------------------------------------------------------

## 6. Foreign Key -- Explanation Using the User Table

A **Foreign Key (FK)** is a column or group of columns in one table that
refers to a key in another table.

The `User` table contains `RoleId`. This column refers to `RoleId` in
the `Roles` table.

Consider these records:

**Roles table**

    RoleId (PK) RoleName
  ------------- ----------
              1 Admin
              2 Faculty
              3 Student

**Users table**

    UserId (PK) FullName       RoleId (FK)
  ------------- ------------ -------------
            101 Asha Patel               3
            102 Ravi Shah                2
            103 Neha Joshi               3

How do we read this data?

-   Asha has `RoleId = 3`, so her role is Student.
-   Ravi has `RoleId = 2`, so his role is Faculty.
-   Neha has `RoleId = 3`, so her role is Student.
-   Both Asha and Neha refer to the same role record.

Therefore, one role can be assigned to many users. This is a
**one-to-many relationship**.

### 6.1 User entity code

The relevant part of the current `User` entity is:

``` csharp
[ForeignKey(nameof(Role))]
public int RoleId { get; set; }

public Role Role { get; set; } = null!;
```

Explanation:

**`public int RoleId { get; set; }`**

This is the FK property. It stores the ID of the role assigned to the
user.

For example, if Asha is a student, her `RoleId` value is `3`.

**`[ForeignKey(nameof(Role))]`**

This is a Data Annotation attribute from:

``` csharp
using System.ComponentModel.DataAnnotations.Schema;
```

It tells EF Core that `RoleId` is the foreign key for the navigation
property named `Role`.

**`public Role Role { get; set; }`**

This is the reference navigation property. It represents the related
role object.

### 6.2 Why do we need both `RoleId` and `Role`?

They serve different purposes.

  -----------------------------------------------------------------------
  Property                            What it gives us
  ----------------------------------- -----------------------------------
  `RoleId`                            The foreign-key value, for example
                                      `3`.

  `Role`                              The related role object, for
                                      example the role whose name is
                                      `Student`.
  -----------------------------------------------------------------------

The attribute affects EF Core's relationship mapping. It does not change
how the C# properties are accessed.

Also, because `RoleId` follows EF Core's conventional naming pattern for
the `Role` navigation, EF Core can usually discover this relationship
without the attribute. The attribute is included in this implementation
to make the mapping explicit.

------------------------------------------------------------------------

## 7. Navigation Properties on Both Sides

A relationship can be represented from both entities.

In our example, the `User` entity contains:

``` csharp
public Role Role { get; set; } = null!;
```

The `Role` entity contains:

``` csharp
public ICollection<User> Users { get; set; } = new List<User>();
```

These properties describe the two sides of the same relationship:

-   From a user, we can navigate to one role.
-   From a role, we can navigate to many users.

The relationship can be visualised as:

``` text
Role
----------------
RoleId (PK)
RoleName
      |
      | one role
      |
      | many users
      v
User
----------------
UserId (PK)
FullName
RoleId (FK)
```

The `RoleId` column in `User` is what stores the database relationship.
The navigation properties provide the object-oriented representation in
C#.

------------------------------------------------------------------------

## 8. Configure the Relationship in CourseHubDbContext

The current `CourseHubDbContext` contains this relationship
configuration:

``` csharp
modelBuilder.Entity<User>()
    .HasOne(u => u.Role)
    .WithMany(r => r.Users)
    .OnDelete(DeleteBehavior.NoAction);
```

This code configures the relationship between `User` and `Role`.

### Explanation of each method

**1. `Entity<User>()`**

Selects the `User` entity whose relationship is being configured.

**2. `HasOne(u => u.Role)`**

Indicates that each user has one related role through the `Role`
navigation property.

**3. `WithMany(r => r.Users)`**

Indicates that one role can be related to many users through the `Users`
collection.

**4. `OnDelete(DeleteBehavior.NoAction)`**

Specifies the delete behavior for this relationship.

### Why is `.HasForeignKey()` not written here?

The current entity class already uses:

``` csharp
[ForeignKey(nameof(Role))]
public int RoleId { get; set; }
```

This attribute identifies `RoleId` as the FK for the `Role` navigation
property. Therefore, the relationship configuration in this
implementation does not need to repeat the FK property using
`.HasForeignKey()`.

`HasOne()` and `WithMany()` pair the navigation properties, while
`OnDelete()` configures delete behavior.

------------------------------------------------------------------------

## 9. Understanding DeleteBehavior.NoAction

The current DbContext uses `DeleteBehavior.NoAction` for the configured
relationships.

Suppose the `Student` role is referenced by Asha and Neha in the `Users`
table.

If we try to physically delete the Student role while those users still
refer to it, SQL Server will prevent the deletion because dependent rows
still reference that role.

This is useful when we do not want deleting one principal record to
automatically delete its dependent records.

### Soft delete versus physical delete

The entities contain properties such as `IsDeleted`.

-   **Soft delete:** Update `IsDeleted` to `true`. The database row
    remains.
-   **Physical delete:** Remove the row from the database.

`DeleteBehavior.NoAction` concerns physical deletion and FK constraints.
It does not automatically change `IsDeleted`.

------------------------------------------------------------------------

## 10. Required and Optional Foreign Keys

In the current code, the FK property's C# type helps EF Core determine
whether the relationship is required or optional.

  Property                    C# type   Meaning
  --------------------------- --------- -------------
  `User.RoleId`               `int`     Required FK
  `Course.FacultyId`          `int`     Required FK
  `Course.CreatedBy`          `int?`    Optional FK
  `Course.UpdatedBy`          `int?`    Optional FK
  `Assignment.CourseId`       `int`     Required FK
  `Assignment.CreatedBy`      `int`     Required FK
  `Assignment.UpdatedBy`      `int?`    Optional FK
  `Submission.AssignmentId`   `int`     Required FK
  `Submission.StudentId`      `int`     Required FK
  `Submission.GradedBy`       `int?`    Optional FK

-   `int` cannot hold `null`, so a value must be supplied.
-   `int?` can hold `null`, so the relationship can be left unassigned.

For example, a submission may exist before a faculty member grades it.
Therefore, `GradedBy` is nullable.

------------------------------------------------------------------------



For example, in `User`:

``` csharp

public ICollection<Course> FacultyCourses { get; set; }
    = new List<Course>();
```

And in `Course`:

``` csharp
public User Faculty { get; set; } = null!;
```

This tells EF Core that `User.FacultyCourses` is the inverse navigation
of `Course.Faculty`.

The same idea is used for the CreatedBy and UpdatedBy relationships, and
for the different user relationships in assignments and submissions.

**Why is it important?** Without clear mapping, EF Core may not know
which navigation properties belong to which relationship when the same
entity types are connected more than once.

------------------------------------------------------------------------

## 12. Understanding CourseHubDbContext

`CourseHubDbContext` is the central EF Core class for the CourseHub
database model.

It inherits from:

``` csharp
public class CourseHubDbContext : DbContext
```

### 12.1 Constructor

The current constructor is:

``` csharp
public CourseHubDbContext(
    DbContextOptions<CourseHubDbContext> options)
    : base(options)
{
}
```

-   `DbContextOptions<CourseHubDbContext>` contains the configuration
    for this context.
-   `: base(options)` passes that configuration to the parent
    `DbContext` class.
-   The options are supplied by ASP.NET Core dependency injection after
    the context is registered.

### 12.2 DbSet properties

The context exposes the five entity sets:

``` csharp
public DbSet<Role> Roles { get; set; }
public DbSet<User> Users { get; set; }
public DbSet<Course> Courses { get; set; }
public DbSet<Assignment> Assignments { get; set; }
public DbSet<Submission> Submissions { get; set; }
```

A `DbSet<TEntity>` gives EF Core an entry point for querying and saving
records of that entity type.

For example, `context.Users` is used to query or add user records.

### 12.3 OnModelCreating

EF Core calls `OnModelCreating()` when it builds the model.

In the current implementation, this method is used to:

-   Configure the five primary keys using `HasKey()`.
-   Configure relationships using `HasOne()` and `WithMany()`.
-   Configure delete behavior using `OnDelete(DeleteBehavior.NoAction)`.

This keeps the key and relationship configuration in the DbContext.

------------------------------------------------------------------------

## 13. SQL Server Connection String

The connection string is stored in `appsettings.json`:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CourseHubDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Explanation:

  -----------------------------------------------------------------------
  Part                                Meaning
  ----------------------------------- -----------------------------------
  `ConnectionStrings`                 Configuration section that stores
                                      connection strings.

  `DefaultConnection`                 Name used to retrieve this
                                      connection string in C#.

  `Server=.`                          Uses the local/default SQL Server
                                      instance.

  `Database=CourseHubDB`              Connects to the database named
                                      `CourseHubDB`.

  `Trusted_Connection=True`           Uses Windows authentication.

  `TrustServerCertificate=True`       Trusts the SQL Server certificate;
                                      commonly used for local
                                      development.
  -----------------------------------------------------------------------

The server name and authentication method must match the SQL Server
installation. Do not use the example connection string unchanged if your
SQL Server instance is configured differently.

------------------------------------------------------------------------

## 14. Register DbContext in Program.cs

The current implementation registers the context using:

``` csharp
builder.Services.AddDbContext<CourseHubDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
```

Explanation:

**`builder.Services.AddDbContext<CourseHubDbContext>()`**

Registers `CourseHubDbContext` with ASP.NET Core's dependency injection
container. A controller or service can then request the context through
its constructor.

**`options.UseSqlServer(...)`**

Selects SQL Server as the EF Core database provider.

**`GetConnectionString("DefaultConnection")`**

Reads the connection string with the name `DefaultConnection` from the
configuration.

### Why do we register DbContext?

Instead of manually creating a database context wherever it is needed,
ASP.NET Core creates and provides the configured context through
dependency injection.

For example, a controller can receive it through its constructor:

``` csharp
public class UsersController : ControllerBase
{
    private readonly CourseHubDbContext _context;

    public UsersController(CourseHubDbContext context)
    {
        _context = context;
    }
}
```

This is an example of constructor dependency injection. The controller
receives the configured context rather than creating it itself.

------------------------------------------------------------------------

## 15. How the Current Lab 6 Implementation Fits Together

Use the Role--User relationship to remember the complete process:

1.  Create the `Role` and `User` entity classes.
2.  Add `RoleId` to `User` to store the FK value.
3.  Add the `Role` navigation property to `User`.
4.  Add the `Users` collection navigation property to `Role`.
5.  Use `[ForeignKey]` to explicitly map `RoleId` to the `Role`
    navigation.
6.  Configure `RoleId` as the primary key of `Role` using `HasKey()`.
7.  Configure the one-to-many relationship using `HasOne()` and
    `WithMany()`.
8.  Configure the delete behavior using
    `OnDelete(DeleteBehavior.NoAction)`.
9.  Register `CourseHubDbContext` with SQL Server in `Program.cs`.

The same pattern is used for the other relationships in CourseHub.

------------------------------------------------------------------------

## 16. Common Viva Questions

**Q1. What is Entity Framework Core?**\
EF Core is an Object-Relational Mapper (ORM). It allows a .NET
application to work with database records through C# classes and
objects.

**Q2. What is an entity?**\
An entity is a C# class that EF Core maps to a database table.

**Q3. What is a primary key?**\
A primary key uniquely identifies each record in a table.

**Q4. What is a foreign key?**\
A foreign key refers to a key in another table and establishes a
relationship between the tables.

**Q5. What is the difference between `RoleId` and `Role` in the User
entity?**\
`RoleId` stores the FK value. `Role` is the navigation property
representing the related role object.

**Q6. Why do we use `[ForeignKey]`?**\
It explicitly tells EF Core which property is the FK for a navigation
property.

**Q7. What does `HasKey()` do?**\
It configures the primary-key property for an entity.

**Q8. What does `HasOne()` mean?**\
It configures one reference navigation on the entity being configured.

**Q9. What does `WithMany()` mean?**\
It configures the collection navigation on the other side of the
relationship.

**Q10. Why do we use `[InverseProperty]`?**\
It identifies which navigation properties are the inverse sides of a
relationship when multiple relationships exist between the same entity
types.

**Q11. What does `DeleteBehavior.NoAction` do?**\
It prevents cascading deletion. If dependent rows still reference a
principal row, the database prevents that principal row from being
physically deleted.

**Q12. What is the difference between `int` and `int?` for an FK?**\
`int` is non-nullable and represents a required FK. `int?` can be null
and represents an optional FK.

**Q13. What is the purpose of `DbContext`?**\
It coordinates EF Core database operations and holds the entity and
relationship configuration.

**Q14. What is the purpose of `DbSet`?**\
It provides an entry point for querying and saving records of an entity
type.

**Q15. Why do we use `AddDbContext()`?**\
It registers the context with dependency injection so the application
can receive the configured context when needed.

**Q16. Why do we use `UseSqlServer()`?**\
It tells EF Core to use SQL Server as the database provider.

------------------------------------------------------------------------


## 18. Lab Scope

This lab is about entity classes, navigation properties, primary keys,
foreign keys, relationships, delete behavior, DbContext, and SQL Server
registration.

Do not add unique indexes, default-value constraints, or check
constraints as part of this lab unless they are explicitly required.
Those are separate database configuration topics.
