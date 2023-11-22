<Query Kind="Program">
<Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\MyExtensions.Core3.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	var builder = WebApplication.CreateBuilder();

	// Configure JSON logging to the console.
	builder.Logging.AddJsonConsole();

	var app = builder.Build();

	app.MapGet("/", () => "Hello JSON console!".Dump("JSON console"));
	
	MyExtensions.SendRequestToServer();

	app.Run();
}