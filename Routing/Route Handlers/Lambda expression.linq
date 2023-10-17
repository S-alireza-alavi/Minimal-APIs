<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/inline", () => "This is an inline lambda".Dump("Result of inline lambda"));

	var handler = () => "This is a lambda variable";

	app.MapGet("/", handler);
	
	handler().Dump("Result of lambda variable");
	
	MyExtensions.ProcessStart();

	app.Run();
}