<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference Version="7.0.13">Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference Version="7.0.13">Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Http.HttpResults</Namespace>
  <Namespace>Microsoft.AspNetCore.Routing</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

async Task Main()
{
	var app = WebApplication.Create();

	// Configure services
	var services = new ServiceCollection();
	ConfigureServices(services);
	var serviceProvider = services.BuildServiceProvider();

	//Resolve CRUD from the service provider
	var crud = serviceProvider.GetRequiredService<CRUD>();

	// Seed initial data
	var dbContext = serviceProvider.GetRequiredService<TodoDb>();
	await SeedData(dbContext);

	app.MapGroup("/public/todos")
	.MapTodosApi(crud)
	.WithTags("Public");

	app.MapGroup("/private/todos")
	.MapTodosApi(crud)
	.WithTags("Private")
	.AddEndpointFilterFactory(QueryPrivateTodos)
	.RequireAuthorization();

	EndpointFilterDelegate QueryPrivateTodos(EndpointFilterFactoryContext factoryContext, EndpointFilterDelegate next)
	{
		var dbContextIndex = -1;

		foreach (var argument in factoryContext.MethodInfo.GetParameters())
		{
			if (argument.ParameterType == typeof(TodoDb))
			{
				dbContextIndex = argument.Position;
				break;
			}
		}

		// Skip filter if the method doesn't have a TodoDb parameter.
		if (dbContextIndex < 0)
		{
			return next;
		}

		return async invocationContext =>
		{
			var dbContext = invocationContext.GetArgument<TodoDb>(dbContextIndex);
			dbContext.IsPrivate = true;

			try
			{
				return await next(invocationContext);
			}
			finally
			{
				// This should only be relevant if you're pooling or otherwise reusing the DbContext instance.
				dbContext.IsPrivate = false;
			}
		};
	}

	curl.GET(url: "http://localhost:5000/public/todos/");
	curl.GET(url: "http://localhost:5000/public/todos/1");
	curl.POST(url: "http://localhost:5000/public/todos/",
		  data: "{\"Id\": 3, \"Name\": \"Task 3\", \"IsComplete\": false}");
	curl.PUT(url: "http://localhost:5000/public/todos/1");
	curl.DELETE(url: "http://localhost:5000/public/todos/1");

	app.Run();
}

public static void ConfigureServices(IServiceCollection services)
{
	services.AddDbContext<TodoDb>(options =>
	{
		options.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	});

	services.AddTransient<CRUD>();
}

public static async Task SeedData(TodoDb dbContext)
{
	if (!dbContext.Todos.Any())
	{
		var initialTodos = new List<Todo>
		{
			new Todo{ Id = 1, Name = "Task 1", IsComplete = false},
			new Todo{ Id = 2, Name = "Task 2", IsComplete = true }
		};

		dbContext.Todos.AddRange(initialTodos);
		await dbContext.SaveChangesAsync();
	}
}

public static class EXMethods
{
	public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group, CRUD crud)
	{
		group.MapGet("/", crud.GetAllTodos);
		group.MapGet("/{id}", crud.GetTodo).Dump("GetTodo");
		group.MapPost("/", crud.CreateTodo).Dump("CreateTodo");
		group.MapPut("/{id}", crud.UpdateTodo).Dump("UpdateTodo");
		group.MapDelete("/{id}", crud.DeleteTodo).Dump("DeleteTodo");

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
	private readonly TodoDb _database;

	public CRUD(TodoDb database)
	{
		_database = database ?? throw new ArgumentNullException(nameof(database));
	}

	public async Task<IEnumerable<Todo>> GetAllTodos()
	{
		var todos = await _database.Todos.ToListAsync();

		todos.Dump("All Todos");

		return todos;
	}

	public async Task<Todo> GetTodo(int id)
	{
		var todo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

		(todo != null ? new[] { todo } : Array.Empty<Todo>()).Dump($"Todo details - Id: {id}");

		return todo;
	}

	public async Task<Created<Todo>> CreateTodo(Todo todo)
	{
		await _database.AddAsync(todo);
		await _database.SaveChangesAsync();

		todo.Dump("");

		return TypedResults.Created($"{todo.Id}", todo);
	}

	public async Task<Todo> UpdateTodo(int id, Todo updatedTodo)
	{
		var existingTodo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

		if (existingTodo == null)
		{
			return null;
		}

		existingTodo.Name = updatedTodo.Name;
		existingTodo.IsComplete = updatedTodo.IsComplete;

		await _database.SaveChangesAsync();

		return existingTodo;
	}

	public async Task<bool> DeleteTodo(int id)
	{
		var existingTodo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

		if (existingTodo == null)
		{
			return false;
		}

		_database.Todos.Remove(existingTodo);
		await _database.SaveChangesAsync();

		return true;
	}
}