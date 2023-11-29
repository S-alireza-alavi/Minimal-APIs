<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference Version="7.0.13">Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore</NuGetReference>
  <NuGetReference Version="7.0.13">Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference Version="7.0.13">Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
	builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
	var app = builder.Build();

	// Seed data
	SeedData(app.Services.GetRequiredService<TodoDb>());

	app.MapGet("/todoitems", async (TodoDb db) =>
	{
		var result = await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync();
		result.Dump("Todo items");
		return result;
	});

	app.MapGet("/ap/todoitems/{id}", async ([AsParameters] TodoItemRequest request) =>
	{
		var todo = await request.Db.Todos.FindAsync(request.Id);
		if (todo is Todo)
		{
			var result = Results.Ok(new TodoItemDTO(todo));
			result.Dump("Todo item");
			return result;
		}
		else
		{
			Results.NotFound().Dump("Todo item not found");
			return Results.NotFound();
		}
	});

	app.MapPost("/ap/todoitems", async ([AsParameters] CreateTodoItemRequest request) =>
	{
		var todoItem = new Todo
		{
			IsComplete = request.Dto.IsComplete,
			Name = request.Dto.Name
		};

		request.Db.Todos.Add(todoItem);
		await request.Db.SaveChangesAsync();

		var result = Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
		result.Dump("Created todo item");
		return result;
	});

	app.MapPut("/ap/todoitems/{id}", async ([AsParameters] EditTodoItemRequest request) =>
	{
		var todo = await request.Db.Todos.FindAsync(request.Id);
		if (todo is null)
		{
			Results.NotFound().Dump("Todo item not found");
			return Results.NotFound();
		}
		todo.Name = request.Dto.Name;
		todo.IsComplete = request.Dto.IsComplete;
		await request.Db.SaveChangesAsync();

		Results.NoContent().Dump("Todo item updated");
		return Results.NoContent();
	});

	app.MapDelete("/ap/todoitems/{id}", async ([AsParameters] TodoItemRequest request) =>
	{
		var todo = await request.Db.Todos.FindAsync(request.Id);
		if (todo is Todo)
		{
			request.Db.Todos.Remove(todo);
			await request.Db.SaveChangesAsync();
			var result = Results.Ok(new TodoItemDTO(todo));
			result.Dump("Todo item deleted");
			return result;
		}
		else
		{
			Results.NotFound().Dump("Todo item not found");
			return Results.NotFound();
		}
	});

	curl.GET(url: "http://localhost:5000/todoitems");

	curl.GET(url: "http://localhost:5000/ap/todoitems/2");

	var createTodoItemDto = new CreateTodoItemRequest
	{
		Dto = new TodoItemDTO
		{
			Name = "New Todo Item",
			IsComplete = false
		}
	};

	curl.POST(url: "http://localhost:5000/ap/todoitems", data: System.Text.Json.JsonSerializer.Serialize(createTodoItemDto), contentType: "application/json");

	var updateTodoItemDto = new EditTodoItemRequest
	{
		Id = 1,
		Dto = new TodoItemDTO
		{
			Name = "Updated Todo Item",
			IsComplete = true
		}
	};

	curl.PUT(url: $"http://localhost:5000/ap/todoitems/{updateTodoItemDto.Id}", data: System.Text.Json.JsonSerializer.Serialize(updateTodoItemDto), contentType: "application/json");

	var deleteTodoItemId = 1;

	curl.DELETE(url: $"http://localhost:5000/ap/todoitems/{deleteTodoItemId}");

	app.Run();
}

void SeedData(TodoDb db)
{
	var todo1 = new Todo { Name = "Buy groceries", IsComplete = false };
	var todo2 = new Todo { Name = "Read a book", IsComplete = true };

	db.Todos.AddRange(todo1, todo2);
	db.SaveChanges();

	"Seed data has been added.".Dump("SeedData");
}

public class Todo
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsComplete { get; set; }
	public string? Secret { get; set; }
}

public class TodoItemDTO
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsComplete { get; set; }

	public TodoItemDTO() { }
	public TodoItemDTO(Todo todoItem) =>
	(Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
}

class TodoDb : DbContext
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

struct TodoItemRequest
{
	public int Id { get; set; }
	public TodoDb Db { get; set; }
}

class CreateTodoItemRequest
{
	public TodoItemDTO Dto { get; set; } = default!;
	public TodoDb Db { get; set; } = default!;
}

class EditTodoItemRequest
{
	public int Id { get; set; }
	public TodoItemDTO Dto { get; set; } = default!;
	public TodoDb Db { get; set; } = default!;
}