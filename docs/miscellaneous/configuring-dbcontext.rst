Configuring a DbContext
=======================

This article shows patterns for configuring a ``DbContext`` with
``DbContextOptions``. Options are primarily used to select and configure the
data store.

.. contents:: `In this article`
  :local:

AddDbContext
------------

This approach requires setting up and using dependency injection. See `more reading`_
below for information on how to do this.

.. code-block:: csharp
  :caption: Startup code

  services.AddEntityFramework()
      .AddSqlite()
      .AddDbContext<BloggingContext>(options =>
          options.UseSqlite("Filename=./blog.db"));


.. code-block:: csharp
  :caption: Context code

  public class BloggingContext : DbContext
  {
      public DbSet<Blog> Blogs { get; set; }
  }


.. code-block:: csharp
  :caption: Application code (in ASP.NET MVC)

  public MyController(BloggingContext context)

.. code-block:: csharp
  :caption: Application code (using ServiceProvider directly, less common)

  using (var context = serviceProvider.GetService<BloggingContext>())
  {
    // do stuff
  }

Constructor argument
--------------------

This approach can be used with or without dependency injection.

.. code-block:: csharp
  :caption: Context code

  public class BloggingContext : DbContext
  {
      public BloggingContext(DbContextOptions options)
          : base(options)
      { }

      public DbSet<Blog> Blogs { get; set; }
  }


.. code-block:: csharp
  :caption: Application code (without DI)

  var optionsBuilder = new DbContextOptionsBuilder();
  optionsBuilder.UseSqlite("Filename=./blog.db");
  using (var context = new BloggingContext(optionsBuilder.Options))
  {
      // do stuff
  }

.. code-block:: csharp
  :caption: Application code (with DI in ASP.NET MVC)

  public MyController(BloggingContext context)

.. code-block:: csharp
  :caption: Test code

  var optionsBuilder = new DbContextOptionsBuilder();
  optionsBuilder.UseInMemoryDatabase();
  using (var context = new BloggingContext(optionsBuilder.Options))
  {
      // test
  }

.. tip::
  This works if there are additional constructor parameters besides  'options'.
  Those additional parameters will be resolved from the DI container.


OnConfiguring
-------------

.. caution::
  ``OnConfiguring`` occurs last and can overwrite options obtained from DI or
  the constructor. This approach does not lend itself to testing (unless you
  target the full database). See `Combinations`_.

.. code-block:: csharp
  :caption: Context code

  public class BloggingContext : DbContext
  {
      public DbSet<Blog> Blogs { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
          optionsBuilder.UseSqlite("Filename=./blog.db");
      }
  }

Combinations
------------

The three options above can be used in combination. When multiple options are
provided, DbContext uses the following priorities to select options:

1. `OnConfiguring`_ (highest priority)
2. `Constructor argument`_
3. `AddDbContext`_ (lowest priority)

Options or services selected in higher priorities will overwrite options from
lower priorities.

More reading
------------

- Read :doc:`/platforms/aspnetcore/getting-started` for more information on
  using EF with ASP.NET Core.
- Read `Dependency Injection <https://docs.asp.net/en/latest/fundamentals/dependency-injection.html>`_ to
  learn more about using DI.
- Read :doc:`testing` for more information.
- Read :doc:`/internals/services` for more advanced information on how DbContext initializes
  options and services.
