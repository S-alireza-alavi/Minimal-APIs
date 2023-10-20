<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The preceding nullable and default value applies to all sources:
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapPost("/products", (Product? product) => { });

	app.Run();
}

class Product { }
//The preceding code calls the method with a null product if no request body is sent.
//NOTE: If invalid data is provided and the parameter is nullable, the route handler is not run.