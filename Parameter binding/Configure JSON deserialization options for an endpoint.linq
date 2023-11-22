<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

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

	MyExtensions.SendRequestToServer();

	app.Run();
}

class Todo
{
	public string? Name { get; set; }
	public string? NameField;
	public bool IsComplete { get; set; }
}
// If the request body contains the following JSON:
//
// {"nameField":"Walk dog", "isComplete":false}
//
// The endpoint returns the following JSON:
//
// {
//    "name":"Walk dog",
//    "isComplete":false
// }