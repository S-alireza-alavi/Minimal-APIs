<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>Microsoft.AspNetCore.Mvc</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//Attributes can be used to explicitly declare where parameters are bound from.
	var builder = WebApplication.CreateBuilder();

	// Added as service
	builder.Services.AddSingleton<Service>();

	var app = builder.Build();

	"1. id\n2. 'page' as a query string\n3. Service and\n4. Content-Type from header".Dump("This GET method can take one of these parameters: ");

	app.MapGet("/{id}", ([FromRoute] int id,
					 [FromQuery(Name = "p")] int page,
					 [FromServices] Service service,
					 [FromHeader(Name = "Content-Type")] string contentType)
					 =>
	{ });
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

class Service
{

}

record Person(string Name, int Age);