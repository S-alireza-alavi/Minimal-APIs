<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <NuGetReference>Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
	builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
	var app = builder.Build();

	app.MapGet("/todoitems", async (TodoDb db) =>
	{
		var result = await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync();
		result.Dump("Todo items");
		return result;
	});

	//app.MapGet("/todoitems/{id}",
	//					 		async (int Id, TodoDb Db) =>
	//	await Db.Todos.FindAsync(Id)
	//		is Todo todo
	//			? Results.Ok(new TodoItemDTO(todo))
	//			: Results.NotFound());

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

	//	app.MapPost("/todoitems", async (TodoItemDTO Dto, TodoDb Db) =>
	//	{
	//		var todoItem = new Todo
	//		{
	//			IsComplete = Dto.IsComplete,
	//			Name = Dto.Name
	//		};
	//
	//		Db.Todos.Add(todoItem);
	//		await Db.SaveChangesAsync();
	//
	//		return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
	//	});

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

	//	app.MapPut("/todoitems/{id}", async (int Id, TodoItemDTO Dto, TodoDb Db) =>
	//	{
	//		var todo = await Db.Todos.FindAsync(Id);
	//
	//		if (todo is null) return Results.NotFound();
	//
	//		todo.Name = Dto.Name;
	//		todo.IsComplete = Dto.IsComplete;
	//
	//		await Db.SaveChangesAsync();
	//
	//		return Results.NoContent();
	//	});

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

	//	app.MapDelete("/todoitems/{id}", async (int Id, TodoDb Db) =>
	//	{
	//		if (await Db.Todos.FindAsync(Id) is Todo todo)
	//		{
	//			Db.Todos.Remove(todo);
	//			await Db.SaveChangesAsync();
	//			return Results.Ok(new TodoItemDTO(todo));
	//		}
	//
	//		return Results.NotFound();
	//	});

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

	curl.GET();

	app.Run();
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
	public TodoDb(DbContextOptions<TodoDb> options)
		: base(options) { }

	public DbSet<Todo> Todos => Set<Todo>();
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