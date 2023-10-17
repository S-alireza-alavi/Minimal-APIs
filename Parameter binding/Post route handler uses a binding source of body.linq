<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	//The HTTP methods GET, HEAD, OPTIONS, and DELETE don't implicitly bind from body. To bind from body (as JSON) for these HTTP methods, bind explicitly with [FromBody] or read from the HttpRequest.
	//The following example POST route handler uses a binding source of body (as JSON) for the person parameter:
	var builder = WebApplication.CreateBuilder();

	var app = builder.Build();

	app.MapPost("/", (Person person) => { "It must be sent with POST method".Dump(); });
	
	MyExtensions.ProcessStart();
	
	app.Run();
}

record Person(string Name, int Age);