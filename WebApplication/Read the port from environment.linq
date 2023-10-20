<Query Kind="Program">
  <Reference Relative="..\MyExtensions.Core3.dll">&lt;MyDocuments&gt;\LINQPad Queries\Minimal APIs quick reference\Extensions.dll</Reference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <IncludeAspNet>true</IncludeAspNet>
</Query>

void Main()
{
	//The following code reads the port from the environment:
	var app = WebApplication.Create();

	var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

	app.MapGet("/", () => "Hello World".Dump("result"));
	
	MyExtensions.ProcessStart();

	app.Run($"http://localhost:{port}".Dump("Port:"));
}