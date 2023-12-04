<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
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

	app.MapPost("/", async (HttpContext context) =>
	{
		if (context.Request.HasJsonContentType())
		{
			var todo = await context.Request.ReadFromJsonAsync<Todo>(options);
			if (todo is not null)
			{
				todo.Name = todo.NameField;
			}

			return Results.Ok(todo.Dump("Todo"));
		}
		else
		{
			return Results.BadRequest();
		}
	});
	
	curl.POST(url: "http://localhost:5000", data: "{\"nameField\":\"Walk dog\", \"isComplete\":false}");

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