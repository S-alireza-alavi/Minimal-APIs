<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Http.HttpResults</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <Namespace>Microsoft.AspNetCore.Routing</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

async Task Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.Services.AddDbContext<TodoDb>();
	
	builder.Services.AddScoped<CRUD>();

	var app = builder.Build();

	//Resolve CRUD from the service provider
	var crud = app.Services.GetRequiredService<CRUD>();

	// Seed initial data
	var db = app.Services.GetRequiredService<TodoDb>();
	SeedData(db);

	app.MapGroup("/public/todos")
	.MapTodosApi(crud)
	.WithTags("Public");

	curl.GET(url: "http://localhost:5000/public/todos/");
	curl.GET(url: "http://localhost:5000/public/todos/5");
	curl.POST(url: "http://localhost:5000/public/todos/",
		  data: "{\"Id\": 3, \"Name\": \"Task 3\", \"IsComplete\": false}");
	curl.PUT(url: "http://localhost:5000/public/todos/4",
    	data: "{\"Id\": 4, \"Name\": \"Updated Task 4\", \"IsComplete\": true}");
	curl.DELETE(url: "http://localhost:5000/public/todos/1");

	app.Run();
}

public static void SeedData(TodoDb dbContext)
{
	if (!dbContext.Todos.Any())
	{
		var initialTodos = new List<Todo>
		{
			new Todo{ Id = 1, Name = "Task 1", IsComplete = false},
			new Todo{ Id = 2, Name = "Task 2", IsComplete = true },
			new Todo{ Id = 3, Name = "Task 3", IsComplete = false },
			new Todo{ Id = 4, Name = "Task 4", IsComplete = true },
			new Todo{ Id = 5, Name = "Task 5", IsComplete = false },
		};

		dbContext.Todos.AddRange(initialTodos);
		dbContext.SaveChanges();
		
		"Seed data has been added.".Dump("SeedData");
	}
}

public static class EXMethods
{
	public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group, CRUD crud)
	{
		group.MapGet("/", crud.GetAllTodos);
		group.MapGet("/{id}", crud.GetTodo);
		group.MapPost("/", crud.CreateTodo);
		group.MapPut("/{id}", crud.UpdateTodo);
		group.MapDelete("/{id}", crud.DeleteTodo);

		return group;
	}
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

	public TodoDb(DbContextOptions<TodoDb> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	}
}

public class CRUD
{
	public async Task<IEnumerable<Todo>> GetAllTodos(TodoDb dbContext)
	{
        var todos = await dbContext.Todos.ToListAsync();        
        return todos;
	}

	public async Task<Todo> GetTodo(int id, TodoDb dbContext)
	{
        var todo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
        (todo != null ? new[] { todo } : Array.Empty<Todo>()).Dump($"Todo details - Id: {id}");
        return todo;
	}

	public async Task<IActionResult> CreateTodo(Todo todo, TodoDb dbContext)
	{
		if (await dbContext.Todos.AnyAsync(t => t.Id == todo.Id))
		{
			return new ConflictObjectResult($"Todo with ID {todo.Id} already exists.");
		}

		await dbContext.AddAsync(todo);
		await dbContext.SaveChangesAsync();
		todo.Dump("Created");
		
		return new CreatedResult($"{todo.Id}", todo);
	}

	public async Task<Todo> UpdateTodo(int id, Todo updatedTodo, TodoDb dbContext)
	{
    	var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

    	if (existingTodo == null)
    	{
        	return null;
    	}

    	if (existingTodo.Id == updatedTodo.Id && updatedTodo.Name != null)
    	{
        	existingTodo.Name = updatedTodo.Name;
        	existingTodo.IsComplete = updatedTodo.IsComplete;

        	await dbContext.SaveChangesAsync();
			await dbContext.Todos.ToListAsync().Dump("All Todos After Update");
        	return existingTodo.Dump("Updated todo");
    	}
    	else
    	{
        	return null;
    	}
	}


	public async Task<bool> DeleteTodo(int id, TodoDb dbContext)
	{
        var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

        if (existingTodo == null)
        {
            return false;
        }

        dbContext.Todos.Remove(existingTodo);
        await dbContext.SaveChangesAsync();
		
		await dbContext.Todos.ToListAsync().Dump("All Todos after Deletion");

        return true;
	}
}