<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	using var db = new TodoDbContext();

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

	app.MapGet("/todos/{id:int}", (int id) => db.Todos.Find(id).Dump("Searched by Id"));
	app.MapGet("/todos/{text}", (string text) => db.Todos.Where(t => t.Name.Contains(text)).ToList().Dump("Searched by Name"));
	app.MapGet("/posts/{slug:regex(^[a-z0-9_-]+$)}", (string slug) => $"Post {slug}".Dump("Searched by Regex"));

	MyExtensions.SendRequestToServer();

	app.Run();
}

public class Todo
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsComplete { get; set; }
}

public class TodoDbContext : DbContext
{
	public DbSet<Todo> Todos => Set<Todo>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	}
}