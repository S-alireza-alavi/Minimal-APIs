<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();
	
	app.MapGet("/hello", () => "Hello World".Dump("String type return value"));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}