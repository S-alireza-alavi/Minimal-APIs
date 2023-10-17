<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.AspNetCore.Http</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var app = WebApplication.Create();

	app.MapGet("/hello",() => Results.Json(new { Message = "Hello World" }.Dump("JSON type Hello World!")));
	
	MyExtensions.ProcessStart();
	
	app.Run();
}