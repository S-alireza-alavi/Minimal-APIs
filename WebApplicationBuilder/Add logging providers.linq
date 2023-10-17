<Query Kind="Program">
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

using static MyExtensions;

void Main()
{
	var builder = WebApplication.CreateBuilder();

	// Configure JSON logging to the console.
	builder.Logging.AddJsonConsole();

	var app = builder.Build();

	app.MapGet("/", () => "Hello JSON console!".Dump("JSON console"));
	
	MyExtensions.ProcessStart();

	app.Run();
}