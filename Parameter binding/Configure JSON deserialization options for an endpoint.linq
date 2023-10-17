<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();

	var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
	{
		IncludeFields = true,
		WriteIndented = true
	};

	app.MapGet("/", async (HttpContext context) =>
	{
		if (context.Request.HasJsonContentType())
		{
			var todo = await context.Request.ReadFromJsonAsync<Todo>(options);
			if (todo is not null)
			{
				todo.Name = todo.NameField;
			}
			return Results.Ok(todo);
		}
		else
		{
			return Results.BadRequest();
		}
	});

	MyExtensions.ProcessStart();

	app.Run();
}

class Todo
{
	public string? Name { get; set; }
	public string? NameField;
	public bool IsComplete { get; set; }
}