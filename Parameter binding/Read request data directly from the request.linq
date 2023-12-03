<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The parameters in the preceding examples are all bound from request data automatically.
	//To demonstrate the convenience that parameter binding provides,
	//the following route handlers show how to read request data directly from the request:
	var app = WebApplication.Create();

	"1. id\n2. 'page' as a query string\n3. Custome Header with this format: 'X-CUSTOM-HEADER'".Dump("This GET method can take one of these parameters: ");

	app.MapGet("/{id}", (HttpRequest request) =>
	{
		var id = request.RouteValues["id"].Dump("Used id as parameter");
		var page = request.Query["page"].Dump("Used 'page' as query string");
		var customeHeader = request.Headers["X-CUSTOM-HEADER"].Dump("Used custome header");

		// ...
	});

	app.MapPost("/", async (HttpRequest request) =>
	{
		var person = await request.ReadFromJsonAsync<Person>();
		person.Dump("Received Person:");
	});

	curl.POST(
		data: "{\"Id\":123, \"Name\":\"John\"}",
		contentType: "application/json",
		headers: new Dictionary<string, string> { { "X-CUSTOM-HEADER", "header-value" } }
		);

	curl.GET(url: "http://localhost:5000/123?page=1", headers: new Dictionary<string, string> { { "X-CUSTOM-HEADER", "header-value" } });

	app.Run();
}

class Person
{
	public int Id { get; set; }
	public string Name { get; set; }
}