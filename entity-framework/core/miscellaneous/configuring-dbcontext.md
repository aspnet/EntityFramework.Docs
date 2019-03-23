---
title: Configuring a DbContext - EF Core
author: rowanmiller
ms.date: 10/27/2016
ms.assetid: d7a22b5a-4c5b-4e3b-9897-4d7320fcd13f
uid: core/miscellaneous/configuring-dbcontext
---
# Configuring a DbContext

This article shows basic patterns for configuring a `DbContext` via a `DbContextOptions` to connect to a database using a specific EF Core provider and optional behaviors.

## Design-time DbContext configuration

EF Core design-time tools such as [migrations](xref:core/managing-schemas/migrations/index) need to be able to discover and create a working instance of a `DbContext` type in order to gather details about the application's entity types and how they map to a database schema. This process can be automatic as long as the tool can easily create the `DbContext` in such a way that it will be configured similarly to how it would be configured at run-time.

While any pattern that provides the necessary configuration information to the `DbContext` can work at run-time, tools that require using a `DbContext` at design-time can only work with a limited number of patterns. These are covered in more detail in the [Design-Time Context Creation](xref:core/miscellaneous/cli/dbcontext-creation) section.

## Configuring DbContextOptions

`DbContext` must have an instance of `DbContextOptions` in order to perform any work. The `DbContextOptions` instance carries configuration information such as:

- The database provider to use, typically selected by invoking a method such as `UseSqlServer` or `UseSqlite`. These extension methods require the corresponding provider package, such as `Microsoft.EntityFrameworkCore.SqlServer` or `Microsoft.EntityFrameworkCore.Sqlite`. The methods are defined in the `Microsoft.EntityFrameworkCore` namespace.
- Any necessary connection string or identifier of the database instance, typically passed as an argument to the provider selection method mentioned above
- Any provider-level optional behavior selectors, typically also chained inside the call to the provider selection method
- Any general EF Core behavior selectors, typically chained after or before the provider selector method

The following example configures the `DbContextOptions` to use the SQL Server provider, a connection contained in the `connectionString` variable, a provider-level command timeout, and an EF Core behavior selector that makes all queries executed in the `DbContext` [no-tracking](xref:core/querying/tracking#no-tracking-queries) by default:

``` csharp
optionsBuilder
    .UseSqlServer(connectionString, providerOptions=>providerOptions.CommandTimeout(60))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

> [!NOTE]  
> Provider selector methods and other behavior selector methods mentioned above are extension methods on `DbContextOptions` or provider-specific option classes. In order to have access to these extension methods you may need to have a namespace (typically `Microsoft.EntityFrameworkCore`) in scope and include additional package dependencies in the project.

The `DbContextOptions` can be supplied to the `DbContext` by overriding the `OnConfiguring` method or externally via a constructor argument.

If both are used, `OnConfiguring` is applied last and can overwrite options supplied to the constructor argument.

### Constructor argument

Context code with constructor:

``` csharp
public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
    { }

    public DbSet<Blog> Blogs { get; set; }
}
```

> [!TIP]  
> The base constructor of DbContext also accepts the non-generic version of `DbContextOptions`, but using the non-generic version is not recommended for applications with multiple context types.

Application code to initialize from constructor argument:

``` csharp
var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
optionsBuilder.UseSqlite("Data Source=blog.db");

using (var context = new BloggingContext(optionsBuilder.Options))
{
  // do stuff
}
```

### OnConfiguring

Context code with `OnConfiguring`:

``` csharp
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blog.db");
    }
}
```

Application code to initialize a `DbContext` that uses `OnConfiguring`:

``` csharp
using (var context = new BloggingContext())
{
  // do stuff
}
```

> [!TIP]
> This approach does not lend itself to testing, unless the tests target the full database.

### Using DbContext with dependency injection

EF Core supports using `DbContext` with a dependency injection container. Your DbContext type can be added to the service container by using the `AddDbContext<TContext>` method.

`AddDbContext<TContext>` will make both your DbContext type, `TContext`, and the corresponding `DbContextOptions<TContext>` available for injection from the service container.

See [more reading](#more-reading) below for additional information on dependency injection.

Adding the `Dbcontext` to dependency injection:

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<BloggingContext>(options => options.UseSqlite("Data Source=blog.db"));
}
```

This requires adding a [constructor argument](#constructor-argument) to your DbContext type that accepts `DbContextOptions<TContext>`.

Context code:

``` csharp
public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
      :base(options)
    { }

    public DbSet<Blog> Blogs { get; set; }
}
```

Application code (in ASP.NET Core):

``` csharp
public class MyController
{
    private readonly BloggingContext _context;

    public MyController(BloggingContext context)
    {
      _context = context;
    }

    ...
}
```

Application code (using ServiceProvider directly, less common):

``` csharp
using (var context = serviceProvider.GetService<BloggingContext>())
{
  // do stuff
}

var options = serviceProvider.GetService<DbContextOptions<BloggingContext>>();
```
## Avoiding DbContext threading issues

Entity Framework Core does not support multiple parallel operations being run on the same `DbContext`. Because of this, it's important to use separate `DbContext` instances for operations that may execute in parallel. Not doing so could result in invalid operation exceptions.

### Avoiding DbContext threading issues with async methods

One code pattern that could lead to parallel operations with a single `DbContext` is the use of asynchronous extension methods. These methods enable Entity Framework Core operations to be performed in a non-blocking way, but if callers do not `await` completion of one operation before starting another, a `DbContext` could encounter issues from running more than one operation in parallel. You should be sure to `await` the completion of any asynchronous operation before beginning another with the same `DbContext`.

### Avoiding DbContext threading issues with dependency injection

Another code pattern that can lead to accidental use of a `DbContext` on multiple parallel threads is retrieving `DbContext` instances from dependency injection since the `AddDbContext<TContext>` method will register the `DbContext` with a [scoped lifetime](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes) by default. Although a scoped lifetime is generally correct for `DbContext` objects, this can lead to threading problems if a service performs work in parallel within a scope.

Be sure to use separate `DbContext` objects for parallel workers. This can be done by creating new `DbContext` instances using `DbContextOptions<TContext>` (which can be retrieved via dependency injection and is safe to use in parallel), or by registering the `DbContext` with a transient lifetime as shown here:

```csharp
// Transient lifetime will provide unique instances for every DbContext retrieval
services.AddDbContext<BloggingContext>(options => options.UseSqlite("Data Source=blog.db"), ServiceLifetime.Transient);
```

Another option is to create a new scope for each worker thread using `IServiceScopeFactory`. Bear in mind that this new scope will apply to all services retrieved by the worker.

## More reading

* Read [Getting Started on ASP.NET Core](../get-started/aspnetcore/index.md) for more information on using EF with ASP.NET Core.
* Read [Dependency Injection](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection) to learn more about using DI.
* Read [Testing](testing/index.md) for more information.
