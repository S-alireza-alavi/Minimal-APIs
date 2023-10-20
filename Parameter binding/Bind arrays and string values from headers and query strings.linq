<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Primitives</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();
	
	var db = new TodoDb();
	db.Database.EnsureCreated();

	// Use a tool like Postman to pass the following data to the '/todoitems/batch' endpoint:
	/*
		[
			{
				"id": 1,
        		"name": "Have Breakfast",
        		"isComplete": true,
        		"tag": {
					"name": "home"

				}
			},
    		{
				"id": 2,
        		"name": "Have Lunch",
        		"isComplete": true,
        		"tag": {
					"name": "work"

				}
			},
    		{
				"id": 3,
        		"name": "Have Supper",
        		"isComplete": true,
        		"tag": {
					"name": "home"

				}
			},
    		{
				"id": 4,
        		"name": "Have Snacks",
        		"isComplete": true,
        		"tag": {
					"name": "N/A"

				}
			}
		]
*/

	// Bind query string values to a primitive type array.
	// GET  /tags?q=1&q=2&q=3
	app.MapGet("/tags", (int[] q) =>
	$"tag1: {q[0]}, tag2: {q[1]}, tag3: {q[2]}");

	// Bind to a string array.
	// GET /tags2?names=john&names=jack&names=jane
	app.MapGet("/tags2", (string[] names) =>
	$"tag1: {names[0]}, tag2: {names[1]}, tag3: {names[2]}");

	// Bind to StringValues.
	// GET /tags3?names=john&names=jack&names=jane
	app.MapGet("tags3", (StringValues names) =>
	$"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}");

	// GET /todoitems/tags?tags=home&tags=work
	app.MapGet("/todoitems/tags", async (Tag[] tags) =>
	{
		return await db.Todos
			.Where(t => tags.Select(i => i.Name).Contains(t.Tag.Name))
			.ToListAsync().Dump();
	});

	// GET /todoitems/query-string-ids?ids=1&ids=3
	app.MapGet("todoitems/query-string-ids", async (int[] ids) =>
	{
		return await db.Todos
		.Where(t => ids.Contains(t.Id))
		.ToListAsync().Dump();
	});

	// To test the preceding code, add the following endpoint to populate the database with Todo items:
	// POST /todoitems/batch
	app.MapPost("/todoitems/batch", async (Todo[] todos) =>
	{
		await db.Todos.AddRangeAsync(todos);
		await db.SaveChangesAsync();

		return Results.Ok(todos);
	});

	// GET /todoitems/header-ids
	// The keys of the headers should all be X-Todo-Id with different values
	app.MapGet("/todoitems/header-ids", async ([FromHeader(Name = "X-Todo-Id")] int[] ids) =>
	{
		return await db.Todos
			.Where(t => ids.Contains(t.Id))
			.ToListAsync().Dump();
	});

	MyExtensions.ProcessStart();

	app.Run();
}

public class TodoDb : DbContext
{
	public DbSet<Todo> Todos => Set<Todo>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	}
}

public class Todo
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsComplete { get; set; }

	// This is an owned entity. 
	public Tag Tag { get; set; } = new();
}

[Owned]
public class Tag
{
	public string? Name { get; set; } = "n/a";

	public static bool TryParse(string? name, out Tag tag)
	{
		if (name is null)
		{
			tag = default!;
			return false;
		}

		tag = new Tag { Name = name };
		return true;
	}
}