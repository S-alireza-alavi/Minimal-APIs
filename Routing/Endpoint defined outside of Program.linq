<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Minimal APIs don't have to be located in Program.cs.
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();

	TodoEndpoints.Map(app);
	
	MyExtensions.SendRequestToServer();

	app.Run();
}

public static class TodoEndpoints
{
	public static void Map(WebApplication app)
	{
		app.MapGet("/", async context =>
		{
			// Get all todo items
			await context.Response.WriteAsJsonAsync(new { Message = "All todo items".Dump("Todos list") });
		});

		app.MapGet("/{id}", async context =>
		{
			// Get one todo item
			await context.Response.WriteAsJsonAsync(new { Message = "One todo item".Dump("Todo") });
		});
	}
}