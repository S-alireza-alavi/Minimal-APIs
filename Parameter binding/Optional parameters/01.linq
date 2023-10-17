<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	/*
		Parameters declared in route handlers are treated as required:

		If a request matches the route, the route handler only runs if all required parameters are provided in the request.
		Failure to provide all required parameters results in an error.
	*/

	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/products", (int pageNumber) => $"Requesting page {pageNumber}".Dump("result"));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}