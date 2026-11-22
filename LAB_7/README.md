# Lab 7 – Entity Framework Core Database Configuration

## Unique Indexes, Default Values, Check Constraints, and Migrations

---

## 1. Lab Overview

In this lab, we extend the existing **CourseHub** ASP.NET Core application by adding database rules through Entity Framework Core (EF Core).

The purpose of these rules is to protect the database from duplicate records and invalid values, even when data is inserted or updated outside the application's user interface.

This lab covers four tasks:

1. Add unique indexes to selected columns and a composite unique index to `Submissions`.
2. Configure default values for selected properties.
3. Add check constraints to validate `Credits`, `MaxMarks`, and `Status`.
4. Create a migration named `InitialCreate` and apply it to the database.

> **Important:** Use the existing CourseHub project, entity classes, and `CourseHubDbContext`. Do not create a second DbContext or replace your existing relationships. The examples below assume the entity and property names shown in the lab requirement. If your project uses different names, use the actual names from your code.



## 2. Project Files Used in This Lab

The relevant files will usually be arranged like this:

```text
CourseHub/
├── Data/
│   └── CourseHubDbContext.cs
├── Models/
│   ├── Role.cs
│   ├── User.cs
│   ├── Course.cs
│   ├── Assignment.cs
│   └── Submission.cs
├── Migrations/
├── appsettings.json
└── Program.cs
```

Your actual folder names may be different. The main file for this lab is `CourseHubDbContext.cs`, because EF Core model rules are configured in its `OnModelCreating()` method.

---

# Problem 1 – Add Unique Indexes

## 1.1 Problem Statement

Add unique indexes for the following columns:

- `Roles.RoleName`
- `Users.Email`
- `Courses.CourseCode`

Also add a **composite unique index** on:

- `Submissions.AssignmentId`
- `Submissions.StudentId`

## 1.2 What Is a Unique Index?

An index helps the database find records efficiently. A **unique index** also prevents duplicate values in the indexed column or combination of columns.

For example:

- Two roles cannot have the same `RoleName`.
- Two users cannot have the same `Email`.
- Two courses cannot have the same `CourseCode`.
- The same student cannot have two submission records for the same assignment when both `AssignmentId` and `StudentId` are identical.

## 1.3  C# Code

Open the existing `CourseHubDbContext.cs` file. Inside the existing `OnModelCreating(ModelBuilder modelBuilder)` method, add the following configuration. Keep your existing DbSets, relationships, primary keys, and other Lab 6 configuration.

```csharp
using Microsoft.EntityFrameworkCore;

public class CourseHubDbContext : DbContext
{
    public CourseHubDbContext(
        DbContextOptions<CourseHubDbContext> options)
        : base(options)
    {
    }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Unique index on RoleName
        modelBuilder.Entity<Role>()
            .HasIndex(x => x.RoleName)
            .IsUnique();

       
}
```


## 1.4 Line-by-Line Explanation

### A. Import Entity Framework Core

```csharp
using Microsoft.EntityFrameworkCore;
```

**Meaning:** Imports the EF Core namespace.

**Why do we write it?** The `DbContext`, `DbContextOptions`, and `ModelBuilder` classes are provided by Entity Framework Core. This namespace makes those types available in this file.

### B. Declare the DbContext class

```csharp
public class CourseHubDbContext : DbContext
```

**Meaning:** Declares the database context class for the CourseHub application.

**Why do we write it?** `DbContext` is the main EF Core class used to query and save data. `CourseHubDbContext` represents the application's database session and model.

### C. Add the DbContext constructor

```csharp
public CourseHubDbContext(
    DbContextOptions<CourseHubDbContext> options)
    : base(options)
{
}
```

**Meaning:** Receives the database configuration options and passes them to the parent `DbContext` class.

**Why do we write it?** EF Core uses these options to know which database provider and connection configuration should be used. The constructor is also required for the normal dependency-injection setup used by ASP.NET Core.

### D. Override OnModelCreating

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
```

**Meaning:** Overrides the EF Core method that is used to configure the application's database model.

**Why do we write it?** We place Fluent API rules here when we want to configure indexes, defaults, relationships, table names, and constraints.

### E. Call the base implementation

```csharp
base.OnModelCreating(modelBuilder);
```

**Meaning:** Calls the implementation of `OnModelCreating()` from the base class.

**Why do we write it?** It preserves any model configuration provided by the base context. It is a standard practice when overriding this method.

### F. Configure the RoleName unique index

```csharp
modelBuilder.Entity<Role>()
    .HasIndex(x => x.RoleName)
    .IsUnique();
```

Line-by-line meaning:

- `modelBuilder` – gives access to EF Core's model configuration.
- `.Entity<Role>()` – selects the `Role` entity for configuration.
- `.HasIndex(x => x.RoleName)` – creates an index based on the `RoleName` property.
- `.IsUnique()` – marks that index as unique, so duplicate role names are rejected by the database.

**Why do we write it?** A role name such as `Admin` should not be stored multiple times if each role name must identify one role.





```csharp
modelBuilder.Entity<Submission>()
    .HasIndex(x => new { x.AssignmentId, x.StudentId })
    .IsUnique();
```

Line-by-line meaning:

- `.Entity<Submission>()` – selects the `Submission` entity.
- `.HasIndex(...)` – tells EF Core to create an index.
- `new { x.AssignmentId, x.StudentId }` – places both properties into one anonymous object so EF Core creates a single composite index containing both columns.
- `.IsUnique()` – requires the combination of the two values to be unique.

**Why do we write it?** It prevents the same student from having more than one submission record for the same assignment. A different student can still submit the same assignment, and one student can submit a different assignment.

## 1.5 Expected Result

After the migration is applied, SQL Server should enforce these rules:

| Table | Indexed column(s) | Expected rule |
|---|---|---|
| Roles | RoleName | No duplicate role names |
| Users | Email | No duplicate email addresses |
| Courses | CourseCode | No duplicate course codes |
| Submissions | AssignmentId + StudentId | No duplicate assignment/student pair |

---

# Problem 2 – Add Default Values

## 2.1 Problem Statement

Configure the following database default values:

| Property | Default value |
|---|---|
| `IsActive` | `true` |
| `IsDeleted` | `false` |
| `Credits` | `3` |
| `MaxMarks` | `100` |
| `CreatedAt` | `GETUTCDATE()` |
| `Status` | `'Submitted'` |

Apply each configuration to the entity that contains that property in your CourseHub model.

> **Important:** The requirement does not identify the entity for every generic property (`IsActive`, `IsDeleted`, and `CreatedAt`). The examples below show the configuration pattern. Use the actual entity or entities in your project that define those properties. Do not add a property to an unrelated entity just to copy the example.

## 2.2 What Is a Default Value?

A default value is a value that the database supplies when an `INSERT` statement does not provide a value for a column.

For example, if a new course is inserted without a `Credits` value, SQL Server can automatically store `3`, provided the database default constraint has been created and the insert omits that column or requests `DEFAULT`.

A default does not automatically replace every explicit `NULL` or every value supplied by the application.

## 2.3 Complete C# Configuration Code

Add the following statements inside the existing `OnModelCreating()` method. The `IsActive`, `IsDeleted`, and `CreatedAt` examples use `User` as an illustration only; move those statements to the correct entity if those properties belong to another entity in your project.

```csharp
// Default value for IsActive
modelBuilder.Entity<User>()
    .Property(x => x.IsActive)
    .HasDefaultValue(true);

// Default value for IsDeleted
modelBuilder.Entity<User>()
    .Property(x => x.IsDeleted)
    .HasDefaultValue(false);


If `IsActive`, `IsDeleted`, or `CreatedAt` belongs to a different entity, change only the entity type in `.Entity<...>()`. For example, if `IsDeleted` is a property of `Course`, configure it using `modelBuilder.Entity<Course>()`.

If multiple entities contain `CreatedAt`, configure the default for each applicable entity.

## 2.4 Line-by-Line Explanation

### A. Configure IsActive

```csharp
modelBuilder.Entity<User>()
    .Property(x => x.IsActive)
    .HasDefaultValue(true);
```

- `modelBuilder.Entity<User>()` selects the `User` entity.
- `.Property(x => x.IsActive)` selects its `IsActive` property.
- `.HasDefaultValue(true)` configures the database default as Boolean `true`.

**Why do we write it?** A newly inserted user should be active by default when the insert does not provide an `IsActive` value.

### B. Configure IsDeleted

```csharp
modelBuilder.Entity<User>()
    .Property(x => x.IsDeleted)
    .HasDefaultValue(false);
```

- `.Entity<User>()` selects the entity that contains the property.
- `.Property(x => x.IsDeleted)` selects the soft-delete flag.
- `.HasDefaultValue(false)` sets the database default to Boolean `false`.

**Why do we write it?** A new record should normally start as not deleted. The application can later set this property to `true` when it performs a soft delete.



```csharp
modelBuilder.Entity<Assignment>()
    .Property(x => x.MaxMarks)
    .HasDefaultValue(100);
```

- `.Entity<Assignment>()` selects the `Assignment` entity.
- `.Property(x => x.MaxMarks)` selects the maximum-marks property.
- `.HasDefaultValue(100)` sets its database default to `100`.

**Why do we write it?** It gives a new assignment a default maximum mark of 100 when no value is supplied.

### E. Configure CreatedAt using SQL

```csharp
modelBuilder.Entity<Assignment>()
    .Property(x => x.CreatedAt)
    .HasDefaultValueSql("GETUTCDATE()");
```

- `.Entity<Assignment>()` selects the entity that contains `CreatedAt`.
- `.Property(x => x.CreatedAt)` selects the date/time property.
- `.HasDefaultValueSql(...)` tells EF Core that the default is a SQL expression, not a fixed C# value.
- `"GETUTCDATE()"` is a SQL Server function that returns the current UTC date and time.

**Why do we write it?** The database can automatically record the UTC creation time when a row is inserted without a `CreatedAt` value.

**Important:** `GETUTCDATE()` is SQL Server-specific. This example is intended for a SQL Server database.

### F. Configure Status

```csharp
modelBuilder.Entity<Submission>()
    .Property(x => x.Status)
    .HasDefaultValue("Submitted");
```

- `.Entity<Submission>()` selects the `Submission` entity.
- `.Property(x => x.Status)` selects the submission status.
- `.HasDefaultValue("Submitted")` sets the default text value to `Submitted`.

**Why do we write it?** A newly created submission should start with the status `Submitted` if the insert does not provide another status.

## 2.5 Difference Between HasDefaultValue and HasDefaultValueSql

| Method | Purpose | Example |
|---|---|---|
| `HasDefaultValue()` | Configures a fixed value | `.HasDefaultValue(3)` |
| `HasDefaultValueSql()` | Configures a database SQL expression | `.HasDefaultValueSql("GETUTCDATE()")` |

Use `HasDefaultValue()` for values such as `true`, `false`, `3`, `100`, and `"Submitted"`. Use `HasDefaultValueSql()` for the SQL Server function `GETUTCDATE()`.

---

# Problem 3 – Add Check Constraints

## 3.1 Problem Statement

Add the following database check constraints:

1. `Credits BETWEEN 1 AND 6`
2. `MaxMarks > 0`
3. `Status IN ('Submitted', 'Graded', 'Late')`

## 3.2 What Is a Check Constraint?

A check constraint is a database rule that validates a value before SQL Server allows an insert or update.

For example:

- `Credits` must be at least 1 and at most 6.
- `MaxMarks` must be greater than 0.
- `Status` must be one of the three allowed values.

These rules are enforced by the database, not only by the frontend or backend validation.

## 3.3 Complete C# Configuration Code

Add these configurations inside the existing `OnModelCreating()` method.

```csharp
// Check constraint: Credits must be between 1 and 6
modelBuilder.Entity<Course>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Courses_Credits",
        "[Credits] BETWEEN 1 AND 6"));

// Check constraint: MaxMarks must be greater than 0
modelBuilder.Entity<Assignment>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Assignments_MaxMarks",
        "[MaxMarks] > 0"));

// Check constraint: Status must be Submitted, Graded, or Late
modelBuilder.Entity<Submission>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Submissions_Status",
        "[Status] IN ('Submitted', 'Graded', 'Late')"));
```

> If an entity already has a `ToTable(...)` configuration, add the check constraint to that existing table mapping rather than creating a conflicting second table mapping. Keep the actual table names used by your project.

## 3.4 Line-by-Line Explanation

### A. Check Credits

```csharp
modelBuilder.Entity<Course>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Courses_Credits",
        "[Credits] BETWEEN 1 AND 6"));
```

- `.Entity<Course>()` selects the `Course` entity.
- `.ToTable(...)` configures the relational table mapping.
- `table =>` gives access to the table builder so a table-level rule can be configured.
- `.HasCheckConstraint(...)` adds a check constraint to the table.
- `"CK_Courses_Credits"` is the constraint name. The `CK_` prefix is a common naming convention for check constraints.
- `"[Credits] BETWEEN 1 AND 6"` is the SQL condition. It accepts values from 1 through 6, including both limits.

**Why do we write it?** A course must not be stored with 0 credits or more than 6 credits.

### B. Check MaxMarks

```csharp
modelBuilder.Entity<Assignment>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Assignments_MaxMarks",
        "[MaxMarks] > 0"));
```

- `.Entity<Assignment>()` selects the `Assignment` entity.
- `.ToTable(...)` accesses the table configuration.
- `.HasCheckConstraint(...)` creates the check rule.
- `"CK_Assignments_MaxMarks"` is the descriptive constraint name.
- `"[MaxMarks] > 0"` means the maximum marks value must be greater than zero.

**Why do we write it?** An assignment cannot have zero or a negative maximum mark.

### C. Check Status

```csharp
modelBuilder.Entity<Submission>()
    .ToTable(table => table.HasCheckConstraint(
        "CK_Submissions_Status",
        "[Status] IN ('Submitted', 'Graded', 'Late')"));
```

- `.Entity<Submission>()` selects the `Submission` entity.
- `.ToTable(...)` accesses the table configuration.
- `.HasCheckConstraint(...)` creates a check rule.
- `"CK_Submissions_Status"` is the constraint name.
- `"[Status] IN ('Submitted', 'Graded', 'Late')"` allows only the three listed status values.

**Why do we write it?** It prevents misspelled or unsupported submission statuses from being saved in the database.

## 3.5 Expected Result

| Property | Allowed values | Example rejected value |
|---|---|---|
| Credits | 1, 2, 3, 4, 5, 6 | 0 or 7 |
| MaxMarks | Any number greater than 0 | 0 or -10 |
| Status | Submitted, Graded, Late | Pending or Completed |

---

# Problem 4 – Create and Apply the Migration

## 4.1 Problem Statement

Run the following Entity Framework Core Package Manager Console commands to create the migration file and apply the changes to the database:

```powershell
Add-Migration InitialCreate
Update-Database
```

## 4.2 What Is a Migration?

A migration is a versioned record of changes to the EF Core data model. EF Core generates migration code that describes how the database schema should be created or changed.

For this lab, the migration should include the unique indexes, default constraints, and check constraints configured in the previous problems.

## 4.3 Step-by-Step Commands

Run the commands in Visual Studio's **Package Manager Console**.

### Step 1 – Open Package Manager Console

In Visual Studio:

1. Open the CourseHub solution.
2. Select **Tools** from the menu.
3. Select **NuGet Package Manager**.
4. Select **Package Manager Console**.


### Step 2 – Build the project

```powershell
Build
```

**Meaning:** Builds the project and checks for compilation errors.

**Why do we write it?** A migration cannot be generated reliably if the project has code errors. Fix build errors before continuing.

### Step 3 – Create the migration

```powershell
Add-Migration InitialCreate
```

**Meaning:** Asks EF Core tools to compare the current model with the migration snapshot and generate a migration named `InitialCreate`.

**Why do we write it?** The generated migration contains the schema operations required to create or update the database structure represented by the model.

**Expected result:** A new migration file is created in the project's `Migrations` folder. It normally contains `Up()` and `Down()` methods.

- `Up()` contains operations that apply the migration.
- `Down()` contains operations that reverse the migration.

### Step 4 – Review the generated migration

Open the new migration file and check that it includes the expected operations:

- Unique indexes for `RoleName`, `Email`, and `CourseCode`.
- A composite unique index for `AssignmentId` and `StudentId`.
- Default constraints for the configured properties.
- Check constraints for `Credits`, `MaxMarks`, and `Status`.

Do not apply a migration without reviewing unexpected table, column, key, or relationship changes.

### Step 5 – Apply the migration

```powershell
Update-Database
```

**Meaning:** Applies all pending EF Core migrations to the database configured for the selected startup project.

**Why do we write it?** Creating a migration only generates the migration code. `Update-Database` executes the migration against the database so that the database schema is updated.

**Expected result:** The database is updated, and EF Core records the applied migration in its `__EFMigrationsHistory` table.

## 4.4 Important Note About the Name InitialCreate

Use `Add-Migration InitialCreate` when this is the first migration for the database/model.

If your CourseHub project already has migrations from Lab 6, do not create another migration named `InitialCreate`. Migration names must be unique. In that case, create a new migration with a different name, for example:

```powershell
Add-Migration Lab7_IndexesDefaultsAndCheckConstraints
Update-Database
```

Follow your instructor's required migration naming if the lab environment already contains an earlier migration.

## 4.5 .NET CLI Alternative

If you are not using Visual Studio's Package Manager Console, the equivalent .NET CLI commands are:

```bash
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Use either the Package Manager Console commands or the .NET CLI commands. You do not need to run both sets.

---

# 5. Verify the Database

After applying the migration, open SQL Server Management Studio (SSMS) and connect to the database used by CourseHub.

The following queries help confirm that the indexes and constraints were created.

## 5.1 Verify Indexes

```sql
EXEC sp_helpindex 'Roles';
EXEC sp_helpindex 'Users';
EXEC sp_helpindex 'Courses';
EXEC sp_helpindex 'Submissions';
```

**Explanation:**

- `EXEC` runs a SQL Server stored procedure.
- `sp_helpindex` displays index information for the specified table.
- Each table name identifies the table whose indexes you want to inspect.

Check that the required unique indexes are present. The composite index should contain both `AssignmentId` and `StudentId`.

## 5.2 Verify Check Constraints

```sql
SELECT
    name,
    definition
FROM sys.check_constraints
ORDER BY name;
```

**Explanation:**

- `SELECT` chooses the columns to display.
- `name` returns the check constraint's name.
- `definition` returns the SQL condition enforced by the constraint.
- `FROM sys.check_constraints` reads SQL Server's system catalog view containing check constraints.
- `ORDER BY name` sorts the results by constraint name.

Look for:

- `CK_Courses_Credits`
- `CK_Assignments_MaxMarks`
- `CK_Submissions_Status`

## 5.3 Verify Default Constraints

```sql
SELECT
    t.name AS TableName,
    c.name AS ColumnName,
    dc.name AS DefaultConstraintName,
    dc.definition AS DefaultValue
FROM sys.default_constraints AS dc
INNER JOIN sys.columns AS c
    ON dc.parent_object_id = c.object_id
   AND dc.parent_column_id = c.column_id
INNER JOIN sys.tables AS t
    ON t.object_id = dc.parent_object_id
ORDER BY t.name, c.name;
```

**Explanation:**

- `sys.default_constraints` contains SQL Server default-constraint information.
- `sys.columns` contains information about table columns.
- `sys.tables` contains information about database tables.
- The first `INNER JOIN` matches a default constraint to the column it belongs to.
- The second `INNER JOIN` matches that column to its table.
- `AS TableName`, `AS ColumnName`, and the other aliases give the output columns readable names.
- `ORDER BY` sorts the output by table and column.

Check that the configured defaults appear for the properties that exist in your model.

---

# 6. Test the Database Rules

Run tests only against a development or test database. Use valid foreign-key values and provide all other required columns when testing inserts.

## 6.1 Test Unique Indexes

Try to insert two records with the same `RoleName`. The second insert should fail.

Repeat the test with:

- The same `Email` in two user records.
- The same `CourseCode` in two course records.
- The same `AssignmentId` and `StudentId` pair in two submission records.

For the composite index, changing either `AssignmentId` or `StudentId` makes a different pair. The database only rejects a repeated pair.

## 6.2 Test Check Constraints

Try inserting or updating these invalid values:

| Table | Property | Invalid value | Expected result |
|---|---|---:|---|
| Courses | Credits | 0 | Rejected |
| Courses | Credits | 7 | Rejected |
| Assignments | MaxMarks | 0 | Rejected |
| Submissions | Status | `Pending` | Rejected |

Also test valid values such as `Credits = 3`, `MaxMarks = 100`, and `Status = 'Submitted'`. These should satisfy the check constraints.

## 6.3 Test Default Values

Insert a record while omitting a column that has a configured default. Then query the inserted record and confirm that SQL Server supplied the default value.

Remember: a database default normally applies when the insert omits the column or explicitly uses `DEFAULT`. It does not automatically replace an explicitly supplied `NULL`.

---

# 7. Important Concepts for Lab 7

| Concept | Meaning |
|---|---|
| Index | A database structure that helps locate records efficiently. |
| Unique index | An index that also prevents duplicate indexed values. |
| Composite index | An index containing more than one column. |
| Default value | A value the database supplies when an insert omits the column or requests `DEFAULT`. |
| Check constraint | A database rule that validates values during inserts and updates. |
| DbContext | EF Core class that represents a session with the database. |
| ModelBuilder | EF Core API used to configure entities and database rules. |
| Migration | A versioned set of schema changes generated from the EF Core model. |
| `Up()` | Migration method that applies the schema changes. |
| `Down()` | Migration method that reverses the schema changes. |
| `Update-Database` | Package Manager Console command that applies pending migrations. |

---

# 9. Viva Questions and Answers

**1. What is Entity Framework Core?**  
Entity Framework Core is an object-relational mapper (ORM) that allows a .NET application to work with a database through .NET classes and objects.

**2. What is an index?**  
An index is a database structure that helps the database locate records more efficiently.

**3. What is a unique index?**  
A unique index prevents duplicate values in the indexed column or column combination.

**4. Why do we create a unique index on `RoleName`?**  
To ensure that the same role name is not stored more than once.

**5. Why do we create a unique index on `Email`?**  
To prevent two user records from using the same email address.

**6. What is a composite unique index?**  
It is a unique index created using two or more columns together.

**7. Why do we use a composite unique index on `AssignmentId` and `StudentId`?**  
To prevent the same student-assignment combination from being stored more than once.

**8. What does `IsUnique()` do?**  
It configures an index so that duplicate indexed values are rejected by the database.

**9. What is a default value?**  
A default value is supplied by the database when an insert does not provide a value for the configured column.

**10. What is the purpose of `HasDefaultValue()`?**  
It configures a constant default value for a property.

**11. What is the purpose of `HasDefaultValueSql()`?**  
It configures a SQL expression as the database default for a property.

**12. What does `GETUTCDATE()` return?**  
It returns the current UTC date and time in SQL Server.

**13. What is a check constraint?**  
A check constraint is a database rule that allows only values satisfying a specified condition.

**14. Why do we use `Credits BETWEEN 1 AND 6`?**  
To allow only credit values from 1 through 6.

**15. Why do we use `MaxMarks > 0`?**  
To ensure that an assignment's maximum marks are greater than zero.

**16. What does the SQL `IN` operator do in the Status constraint?**  
It checks whether the status matches one of the allowed values.

**17. What is `OnModelCreating()`?**  
It is an EF Core method used to configure entity mappings, indexes, defaults, relationships, and constraints.

**18. What is a migration?**  
A migration records model changes and the database operations required to apply those changes.

**19. What does `Add-Migration InitialCreate` do?**  
It generates a migration named `InitialCreate` from the current EF Core model, provided that migration name has not already been used.

**20. What does `Update-Database` do?**  
It applies pending migrations to the configured database.

---

# 10. Lab 7 Submission Checklist

Before submitting the lab, confirm the following:

- [ ] The existing CourseHub project and `CourseHubDbContext` are used.
- [ ] A unique index is configured on `Role.RoleName`.
- [ ] A unique index is configured on `User.Email`.
- [ ] A unique index is configured on `Course.CourseCode`.
- [ ] A composite unique index is configured on `Submission.AssignmentId` and `Submission.StudentId`.
- [ ] The required default values are configured on the entities that contain those properties.
- [ ] `CreatedAt` uses `HasDefaultValueSql("GETUTCDATE()")` for the applicable entity or entities.
- [ ] The Credits check constraint allows only values from 1 through 6.
- [ ] The MaxMarks check constraint requires a value greater than 0.
- [ ] The Status check constraint allows only `Submitted`, `Graded`, and `Late`.
- [ ] The project builds successfully.
- [ ] The generated migration has been reviewed.
- [ ] The migration has been applied to the intended database.
- [ ] The database indexes, defaults, and check constraints have been verified.
- [ ] Valid and invalid test cases have been checked.

## 11. Conclusion

In Lab 7, we used Entity Framework Core Fluent API to improve database integrity in the CourseHub application.

Unique indexes prevent duplicate role names, email addresses, course codes, and duplicate student-assignment submission pairs. Default values allow the database to supply appropriate values when an insert omits those columns. Check constraints prevent invalid credits, maximum marks, and submission statuses. Finally, EF Core migrations apply these model configurations to the SQL Server database.

These configurations help keep database records consistent and reliable, even when data is added or updated through different parts of an application.

---

**End of Lab 7**
