<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();

	builder.Services.ConfigureHttpJsonOptions(options =>
	{
		options.SerializerOptions.WriteIndented = true;
		options.SerializerOptions.IncludeFields = true;
	});

	var app = builder.Build();

	app.MapPost("/", (Todo todo) =>
	{
		if (todo is not null)
		{
			todo.Name = todo.NameField;
		}
		return todo;
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