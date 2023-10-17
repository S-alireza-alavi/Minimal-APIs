<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();

	app.MapGet("/hello", () => new { Message = "Hello World".Dump("JSON type return value") });
	
	MyExtensions.ProcessStart();
	
	app.Run();
}