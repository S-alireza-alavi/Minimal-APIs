<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();
	var app = builder.Build();

	app.MapGet("/inline", () => "This is an inline lambda".Dump("Result of inline lambda"));

	var handler = () => "This is a lambda variable";

	app.MapGet("/", handler);
	
	handler().Dump("Result of lambda variable");
	
	MyExtensions.SendRequestToServer();

	app.Run();
}