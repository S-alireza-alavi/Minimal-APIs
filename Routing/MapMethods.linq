<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal-APIs\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/", () => "This is a GET".Dump("GET method"));
	app.MapPost("/", () => "This is a POST".Dump("POST method"));
	app.MapPut("/", () => "This is a PUT".Dump("PUT method"));
	app.MapDelete("/", () => "This is a DELETE".Dump("DELETE method"));

	app.MapMethods("/options-or-head", new[] { "OPTIONS", "HEAD" },
	() => "This is an options or head request".Dump("Map Methods"));
	
	curl.GET();
	curl.POST();
	curl.PUT();
	curl.DELETE();
	
	app.Run();
}