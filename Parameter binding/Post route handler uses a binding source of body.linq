<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The HTTP methods GET, HEAD, OPTIONS, and DELETE don't implicitly bind from body. To bind from body (as JSON) for these HTTP methods, bind explicitly with [FromBody] or read from the HttpRequest.
	//The following example POST route handler uses a binding source of body (as JSON) for the person parameter:
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();

	app.MapPost("/", (Person person) =>
	{
		$"Received POST request with data:".Dump("Posted Successfully");
		$"Name: {person.Name}".Dump("Person Name");
		$"Age: {person.Age}".Dump("Person Age");
	});

	curl.POST(data: "{\"Name\":\"John\", \"Age\":30}", contentType: "application/json");

	app.Run();
}

record Person(string Name, int Age);