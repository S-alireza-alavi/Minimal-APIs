<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.AspNetCore.Routing</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.AspNetCore.Http.HttpResults</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var app = WebApplication.Create();

	//app.MapGet("/public/todos")
	//.MapToD

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
	
	curl.GET();
	
	app.Run();
}

public static class EXMethods
{
	public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group)
	{
		group.MapGet("/", CRUD.GetAllTodos).Dump("GetAllTodos");
		group.MapGet("/{id}", CRUD.GetTodo).Dump("GetTodo");
		group.MapPost("/", CRUD.CreateTodo).Dump("CreateTodo");
		group.MapPut("/{id}", CRUD.UpdateTodo).Dump("UpdateTodo");
		group.MapDelete("/{id}", CRUD.DeleteTodo).Dump("DeleteTodo");

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

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
	}
}

public static class CRUD
{
	private static readonly TodoDb _database = new TodoDb();
	public static async Task<IEnumerable<Todo>> GetAllTodos()
	{
		return await _database.Todos.ToListAsync();
	}

	public static async Task<Todo> GetTodo(int id)
	{
		return await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
	}

	public static async Task<Created<Todo>> CreateTodo(Todo todo)
	{
		await _database.AddAsync(todo);
		await _database.SaveChangesAsync();

		return TypedResults.Created($"{todo.Id}", todo);
	}

	public static async Task<Todo> UpdateTodo(int id, Todo updatedTodo)
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

	public static async Task<bool> DeleteTodo(int id)
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