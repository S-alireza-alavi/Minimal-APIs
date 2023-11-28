<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
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
	{
		$"Used id as parameter: {id}".Dump("id");
		$"Used 'page' as query string: {page}".Dump("page");
		$"Used Service: {service.GetType().Name}".Dump("service");
		$"Used Content-Type from header: {contentType}".Dump("contentType");
	});

	curl.GET(url: "http://localhost:5000/123?p=1", headers: new Dictionary<string, string> { { "Content-Type", "application/json" } });

	app.Run();
}

class Service
{

}

record Person(string Name, int Age);