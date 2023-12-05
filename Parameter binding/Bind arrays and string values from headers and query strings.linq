<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.Extensions.Primitives</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.Services.AddDbContext<TodoDb>();

	var app = builder.Build();

	// Seed data at the start
	var db = app.Services.GetRequiredService<TodoDb>();
	SeedData(db);

	// Bind query string values to a primitive type array.
	// GET  /tags?q=1&q=2&q=3
	app.MapGet("/tags", (int[] q) =>
	$"tag1: {q[0]}, tag2: {q[1]}, tag3: {q[2]}".Dump("/tags"));

	// Bind to a string array.
	// GET /tags2?names=john&names=jack&names=jane
	app.MapGet("/tags2", (string[] names) =>
	$"tag1: {names[0]}, tag2: {names[1]}, tag3: {names[2]}".Dump("/tags2"));

	// Bind to StringValues.
	// GET /tags3?names=john&names=jack&names=jane
	app.MapGet("/tags3", (StringValues names) =>
	$"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}".Dump("/tags3"));

	// GET /todoitems/tags?tags=home&tags=work
	app.MapGet("/todoitems/tags", async ([FromHeader] Tag[] tags, TodoDb dbContext) =>
	{
		var result = await dbContext.Todos
        .Where(t => tags.Select(tag => tag.Name).Contains(t.Tag.Name))
        .ToListAsync();

		return result.Dump("/todoitems/tags");
	});

	// GET /todoitems/query-string-ids?ids=1&ids=3
	app.MapGet("todoitems/query-string-ids", async (int[] ids, TodoDb dbContext) =>
	{
		ids.Dump("ids");
        var result = await dbContext.Todos
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();

        return result.Dump("/todoitems/query-string-ids");
	});

	app.MapGet("/todoitems/header-ids", async ([FromHeader(Name = "X-Todo-Id")] int[] ids, TodoDb dbContext) =>
	{
		if (ids == null || ids.Length == 0)
        {
            // Handle the case where ids is null or empty
            return Results.BadRequest("Ids cannot be null or empty.");
        }

        try
        {
            var result = await dbContext.Todos
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();
				
			result.Dump("header-ids");

            if (result.Any())
            {
                // Return a successful response with the queried data
                return Results.Ok(result);
            }
            else
            {
                // Return a not found response if no matching items are found
                return Results.NotFound();
            }
		}
		catch (Exception ex)
		{
			// Log the exception for debugging purposes
			Console.Error.WriteLine($"Exception: {ex}");

			// Return an appropriate HTTP status code for the client
			return Results.BadRequest("An error occurred while processing the request.");
		}
	});

	// Test GET /tags
	curl.GET(url: "http://localhost:5000/tags?q=1&q=2&q=3");

	// Test GET /tags2
	curl.GET(url: "http://localhost:5000/tags2?names=john&names=jack&names=jane");

	// Test GET /tags3
	curl.GET(url: "http://localhost:5000/tags3?names=john&names=jack&names=jane");

	// Test GET /todoitems/tags
	var tagData = new List<Tag>
	{
		new Tag { Name = "home" },
		new Tag { Name = "work" }
	};
	
	var tagDataJson = System.Text.Json.JsonSerializer.Serialize(tagData);
	curl.GET(url: "http://localhost:5000/todoitems/tags", headers: new Dictionary<string, string> { { "Tags", tagDataJson } });

	// Test GET /todoitems/query-string-ids
	var idsData = new List<int> { 1, 3 };
	var idsQueryParam = string.Join("&", idsData.Select(id => $"ids={id}"));
	curl.GET(url: $"http://localhost:5000/todoitems/query-string-ids?{idsQueryParam}");

	// Test GET /todoitems/header-ids
	curl.GET(url: "http://localhost:5000/todoitems/header-ids", headers: new Dictionary<string, string> { { "X-Todo-Id", "1" } });

	app.Run();
}

private static void SeedData(TodoDb dbContext)
{
	var todosData = new List<Todo>
	{
		new Todo { Id = 1, Name = "Have Breakfast", IsComplete = true, Tag = new Tag { Name = "home" } },
		new Todo { Id = 2, Name = "Have Lunch", IsComplete = true, Tag = new Tag { Name = "work" } },
		new Todo { Id = 3, Name = "Have Supper", IsComplete = true, Tag = new Tag { Name = "home" } },
		new Todo { Id = 4, Name = "Have Snacks", IsComplete = true, Tag = new Tag { Name = "work" } },
		new Todo { Id = 5, Name = "Watch TV", IsComplete = true, Tag = new Tag { Name = "home" } }
	};

	dbContext.Todos.AddRange(todosData);
	dbContext.SaveChanges();
	

	"Seed data has been added.".Dump("SeedData");
}

public class TodoDb : DbContext
{
	public DbSet<Todo> Todos => Set<Todo>();
	
	public TodoDb(DbContextOptions<TodoDb> options) : base(options)
	{
	}

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