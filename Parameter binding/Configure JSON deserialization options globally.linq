<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.Services.ConfigureHttpJsonOptions(options =>
	{
		options.SerializerOptions.IncludeFields = true;
	});

	var app = builder.Build();

	app.MapPost("/", (Todo todo) =>
	{
		if (todo is not null)
		{
			todo.Name = todo.NameField;
		}

		return todo.Dump("result");
	});

	var todoData = new Todo
	{
		NameField = "Walk dog",
		IsComplete = false
	};

	var todoJson = System.Text.Json.JsonSerializer.Serialize(todoData);

	curl.POST(url: "http://localhost:5000/", data: todoJson, contentType: "application/json");

	app.Run();
}

class Todo
{
	public string? Name { get; set; }
	public string? NameField { get; set; }
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
//    "nameField":"Walk dog",
//    "isComplete":false
// }