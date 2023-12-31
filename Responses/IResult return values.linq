<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	builder.Services.AddDbContext<TodoDb>();
	var app = builder.Build();

	using var db = new TodoDb();

	db.Database.EnsureCreated();

	if (!db.Todos.Any())
	{
		db.Todos.AddRange(
			new Todo { Name = "Task 1", IsComplete = false },
			new Todo { Name = "Task 2", IsComplete = true },
			new Todo { Name = "Task 3", IsComplete = false }
		);
		
		db.SaveChanges();
	}
	
	var todos = db.Todos.Dump("Todos before FindAsync");

	app.MapGet("/hello",() => Results.Ok(new { Message = "Hello World".Dump("/hello") }));

	//The following example uses the built-in result types to customize the response:
	app.MapGet("/api/todoitems/{id}", async (int id, TodoDb db) =>
		 await db.Todos.FindAsync(id)
		 is Todo todo
		 ? Results.Ok(todo).Dump("Found")
		 : Results.NotFound().Dump("Not found"));
   
   curl.GET(url: "http://localhost:5000/hello");
   
   curl.GET(url: "http://localhost:5000/api/todoitems/1");
   
   app.Run();
}

public class Todo
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsComplete { get; set; }
}

public class TodoDb : DbContext
{
	public DbSet<Todo> Todos => Set<Todo>();

	public bool IsPrivate { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	}
}