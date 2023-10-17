<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	string LocalFunction() => "This is local function".Dump("Result");

	app.MapGet("/", LocalFunction);
	
	MyExtensions.ProcessStart();

	app.Run();
}